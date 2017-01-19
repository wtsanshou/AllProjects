using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows;

namespace _20120607HandTracking.Manage
{
    class KinectCursorManager
    {
        private KinectSensor _kinectSensor;
        private CursorAdorner _cursorAdorner;
        private readonly Window _window;
        private UIElement _lastElementOver;
        private bool _isSkeletonTrackingActivated;
        private static bool _isInitialized;
        private static KinectCursorManager _instance;

        public static void Create(Window window)
        {
            if (!_isInitialized)
            {
                _instance = new KinectCursorManager(window);
                _isInitialized = true;
            }
        }

        public static void Create(Window window, FrameworkElement cursor)
        {
            if (!_isInitialized)
            {
                _instance = new KinectCursorManager(window, cursor);
                _isInitialized = true;
            }
        }

        public static void Create(Window window, KinectSensor sensor)
        {
            if (!_isInitialized)
            {
                _instance = new KinectCursorManager(window, sensor);
                _isInitialized = true;
            }
        }

        public static void Create(Window window, KinectSensor sensor,FrameworkElement cursor)
        {
            if (!_isInitialized)
            {
                _instance = new KinectCursorManager(window, sensor, cursor);
                _isInitialized = true;
            }
        }

        public static KinectCursorManager Instance
        {
            get { return _instance; }
        }

        private KinectCursorManager(Window window)
            : this(window, KinectSensor.KinectSensors[0])
        { }

        private KinectCursorManager(Window window, FrameworkElement cursor)
            : this(window, KinectSensor.KinectSensors[0], cursor)
        { }

        private KinectCursorManager(Window window, KinectSensor sensor)
            : this(window, sensor, null)
        { }

        private KinectCursorManager(Window window, KinectSensor sensor, FrameworkElement cursor)
           
        {
            this._window = window;

            if (KinectSensor.KinectSensors.Count > 0)
            {
                _window.Unloaded += delegate
                {
                    if (this._kinectSensor.SkeletonStream.IsEnabled)
                    {
                        this._kinectSensor.SkeletonStream.Disable();
                    }
                    _kinectSensor.Stop();
                };

                _window.Loaded += delegate
                {
                    if (cursor == null)
                    {
                        _cursorAdorner = new CursorAdorner((FrameworkElement)window.Content);
                    }
                    else
                    {
                        _cursorAdorner = new CursorAdorner((FrameworkElement)window.Content, cursor);
                    }

                    this._kinectSensor = sensor;

                    this._kinectSensor.SkeletonFrameReady += SkeletonFrameReady;
                    this._kinectSensor.SkeletonStream.Enable(new TransformSmoothParameters());
                    this._kinectSensor.Start();
                };
            }
        }


        //KinectCursorManager Event Management
        private void SetSkeletonTrackingActivated()
        {
            if (_lastElementOver != null && _isSkeletonTrackingActivated == false)
            {
                _lastElementOver.RaiseEvent(new RoutedEventArgs(KinectInput.KinectCursorEnterEvent));
            };
            _isSkeletonTrackingActivated = true;
        }

        private void SetSkeletonTrackingDeactivated()
        {
            if (_lastElementOver != null && _isSkeletonTrackingActivated == true)
            {
                _lastElementOver.RaiseEvent(new RoutedEventArgs(KinectInput.KinectCursorLeaveEvent));
            };
            _isSkeletonTrackingActivated = false;
        }

        private void HandleCursorEvents(Point point, double z)
        {
            UIElement element = GetElementAtScreenPoint(point, _window);
            if (element != null)
            {
                element.RaiseEvent(new RoutedEventArgs(KinectInput.KinectCursorMoveEvent, point, z) { Cursor = _cursorAdorner});

                if (element != _lastElementOver)
                {
                    if (_lastElementOver != null)
                    {
                        _lastElementOver.RaiseEvent(new RoutedEventArgs(KinectInput.KinectCursorLeaveEvent, point, z) { Cursor = _cursorAdorner });
                    }

                    element.RaiseEvent(new RoutedEventArgs(KinectInput.KinectCursorEnterEvent, point, z) { Cursor = _cursorAdorner });
                }
            }
            _lastElementOver = element;
        }

        

        //p198 Translating Kinect Data into WPF Data
        private void SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame == null || frame.SkeletonArrayLength == 0)
                {
                    return;
                }

                Skeleton[] skeletons = new Skeleton[frame.SkeletonArrayLength];
                frame.CopySkeletonDataTo(skeletons);
                Skeleton skeleton = GetPrimarySkeleton(skeletons);

                if (skeleton == null)
                {
                    SetSkeletonTrackingDeactivated();
                }
                else
                {
                    Joint? primaryHand = GetPrimaryHand(skeleton);
                    if (primaryHand.HasValue)
                    {
                        UpdateCursor(primaryHand.Value);
                    }
                    else
                    {
                        SetSkeletonTrackingDeactivated();
                    }
                }
            }
        }

        private static Joint GetPrimaryHand(Skeleton skeleton)
        {
            Joint primaryHand = new Joint();
            if (skeleton != null)
            {
                primaryHand = skeleton.Joints[JointType.HandLeft];
                Joint rightHand = skeleton.Joints[JointType.HandRight];

                if (rightHand.TrackingState != JointTrackingState.NotTracked)
                {
                    if (primaryHand.TrackingState == JointTrackingState.NotTracked)
                    {
                        primaryHand = rightHand;
                    }
                    else
                    {
                        if (primaryHand.Position.Z > rightHand.Position.Z)
                        {
                            primaryHand = rightHand;
                        }
                    }
                }
            }
            return primaryHand;
        }

        private static Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
        {
            Skeleton skeleton = null;
            if (skeletons != null)
            {
                for (int i = 0; i < skeletons.Length; i++)
                {
                    if (skeletons[i].TrackingState == SkeletonTrackingState.Tracked)
                    {
                        if (skeleton == null)
                        {
                            skeleton = skeletons[i];
                        }
                        else
                        {
                            if (skeleton.Position.Z > skeletons[i].Position.Z)
                            {
                                skeleton = skeletons[i];
                            }
                        }
                    }
                }
            }
            return skeleton;
        }

        private void UpdateCursor(Joint hand)
        {
            var point = _kinectSensor.MapSkeletonPointToDepth(hand.Position, _kinectSensor.DepthStream.Format);

            float x = point.X;
            float y = point.Y;
            float z = point.Depth;

            x = (float)(x * _window.ActualWidth / _kinectSensor.DepthStream.FrameWidth);
            y = (float)(y * _window.ActualHeight / _kinectSensor.DepthStream.FrameHeight);

            Point cursorPoint = new Point(x, y);
            HandleCursorEvents(cursorPoint, z);
            _cursorAdorner.UpdateCursor(cursorPoint);
        }
    }
}
