using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Kinect;

namespace KinectGestureLibrary
{
    public class KinectCursorManager
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

        public static void Create(Window window, KinectSensor sensor, FrameworkElement cursor)
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
                _lastElementOver.RaiseEvent(new RoutedEventArgs(KinectInput.KinectCursorActivatedEvent));
            };
            _isSkeletonTrackingActivated = true;
        }

        private void SetSkeletonTrackingDeactivated()
        {
            if (_lastElementOver != null && _isSkeletonTrackingActivated == true)
            {
                _lastElementOver.RaiseEvent(new RoutedEventArgs(KinectInput.KinectCursorDeactivatedEvent));
            };
            _isSkeletonTrackingActivated = false;
        }

        

        private void HandleCursorEvents(Point point, double z, Joint joint)
        {

            UIElement element = GetElementAtScreenPoint(point, _window);
            if (element != null)
            {
                element.RaiseEvent(new KinectCursorEventArgs(KinectInput.KinectCursorMoveEvent, point, z) { Cursor = _cursorAdorner });
                if (element != _lastElementOver)
                {
                    if (_lastElementOver != null)
                    {
                        _lastElementOver.RaiseEvent(new KinectCursorEventArgs(KinectInput.KinectCursorLeaveEvent, point, z) { Cursor = _cursorAdorner });
                    }

                    element.RaiseEvent(new KinectCursorEventArgs(KinectInput.KinectCursorEnterEvent, point, z) { Cursor = _cursorAdorner });
                }
            }

            _lastElementOver = element;
        }

        //KinectCursorManager Helper Methods
        private static UIElement GetElementAtScreenPoint(Point point, Window window)
        {
            if (!window.IsVisible)
            {
                return null;
            }
            Point windowPoint = window.PointFromScreen(point);

            IInputElement element = window.InputHitTest(windowPoint);
            if (element is UIElement)
            {
                return (UIElement)element;
            }
            else
                return null;
        }

        private static Skeleton GetPrimarySkeleton(IEnumerable<Skeleton> skeletons)
        {
            Skeleton primarySkeleton = null;
            foreach (Skeleton skeleton in skeletons)
            {
                if (skeleton.TrackingState != SkeletonTrackingState.Tracked)
                {
                    continue;
                }
                if (primarySkeleton == null)
                {
                    primarySkeleton = skeleton;
                }
                else if (primarySkeleton.Position.Z > skeleton.Position.Z)
                {
                    primarySkeleton = skeleton;
                }

            }
            return primarySkeleton;
        }

        private static Joint? GetPrimaryHand(Skeleton skeleton)
        {
            Joint leftHand = skeleton.Joints[JointType.HandLeft];
            Joint rightHand = skeleton.Joints[JointType.HandRight];

            if (rightHand.TrackingState == JointTrackingState.Tracked)
            {
                if (leftHand.TrackingState != JointTrackingState.Tracked)
                {
                    return rightHand;
                }
                else if (leftHand.Position.Z > rightHand.Position.Z)
                {
                    return rightHand;
                }
                else
                {
                    return leftHand;
                }
            }

            if (leftHand.TrackingState == JointTrackingState.Tracked)
            {
                return leftHand;
            }
            else
                return null;
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


        private void UpdateCursor(Joint hand)
        {
            var point = _kinectSensor.MapSkeletonPointToDepth(hand.Position, _kinectSensor.DepthStream.Format);

            float x = point.X;
            float y = point.Y;
            float z = point.Depth;

            x = (float)(x * _window.ActualWidth / _kinectSensor.DepthStream.FrameWidth);
            y = (float)(y * _window.ActualHeight / _kinectSensor.DepthStream.FrameHeight);

            Point cursorPoint = new Point(x, y);
            HandleCursorEvents(cursorPoint, z, hand);
            _cursorAdorner.UpdateCursor(cursorPoint);
        }

        //Swipe***********************************************************************************************
        public struct GesturePoint
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
            public DateTime T { get; set; }

            public override bool Equals(object obj)
            {
                var o = (GesturePoint)obj;
                return (X == o.X) && (Y == o.Y) && (Z == o.Z) && (T == o.T);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        //P211 Swipe Gesture Detection fields
        private List<GesturePoint> _gesturePoints = new List<GesturePoint>();
        private bool _gesturePointTrackingEnabled;
        private double _swipeLenght, _swipeDeviation;
        private int _swipeTime;
        public event KinectCursorEventHandler swipeDetected;
        public event KinectCursorEventHandler SwipeOutOfBoundsDetected;

        private double _xOutOfBoundsLenght;
        private static double _initialSwipeX;

        public void GesturePointTrackingInitialize(double swipeLenght, double swipeDeviation, int swipeTime, double xOutOfBounds)
        {
            _swipeLenght = swipeLenght;
            _swipeDeviation = swipeDeviation;
            _swipeTime = swipeTime;
            _xOutOfBoundsLenght = xOutOfBounds;
        }

        public void GesturePointTrackingStart()
        {
            if (_swipeLenght + _swipeDeviation + _swipeTime == 0)
            {
                throw (new InvalidOperationException("Swipe detection not initialized."));

            }
            _gesturePointTrackingEnabled = true;
        }

        public void GesturePointTrackingStop()
        {
            _xOutOfBoundsLenght = 0;
            _gesturePointTrackingEnabled = false;
            _gesturePoints.Clear();
        }

        public bool GesturePointTrackingEnabled
        {
            get { return _gesturePointTrackingEnabled; }
        }

        public void ResetGesturePoint(GesturePoint point)
        {
            bool startRemoving = false;
            for (int i = _gesturePoints.Count; i >= 0; i--)
            {
                if (startRemoving)
                {
                    _gesturePoints.RemoveAt(i);
                }
                else if (_gesturePoints[i].Equals(point))
                {
                    startRemoving = true;
                }
            }
        }

        private void ResetGesturePoint(int point)
        {
            if (point < 1)
            {
                return;
            }
            for (int i = point - 1; i >= 0; i--)
            {
                _gesturePoints.RemoveAt(i);
            }
        }

        //Heart of swipe Detection
        private void HandleGestureTracking(float x, float y, float z)
        {
            if (!_gesturePointTrackingEnabled)
            {
                return;
            }

            if (_xOutOfBoundsLenght != 0 && _initialSwipeX == 0)
            {
                _initialSwipeX = x;
            }

            GesturePoint newPoint = new GesturePoint() { X = x, Y = y, Z = z, T = DateTime.Now };
            _gesturePoints.Add(newPoint);

            GesturePoint startPoint = _gesturePoints[0];
            var point = new Point(x, y);

            //check for deviation
            if (Math.Abs(newPoint.Y - startPoint.Y) > _swipeDeviation)
            {
                if (SwipeOutOfBoundsDetected != null)
                {
                    SwipeOutOfBoundsDetected(this, new KinectCursorEventArgs(point) { Z = z, Cursor = _cursorAdorner });
                }
                ResetGesturePoint(_gesturePoints.Count);
                return;
            }

            //check time
            if ((newPoint.T - startPoint.T).Milliseconds > _swipeTime)
            {
                _gesturePoints.RemoveAt(0);
                startPoint = _gesturePoints[0];
            }

            //check to see if distance has been achieved swipe left
            if ((_swipeLenght < 0 && newPoint.X - startPoint.X < _swipeLenght) || (_swipeLenght > 0 && newPoint.X - startPoint.X > _swipeLenght))
            {
                _gesturePoints.Clear();
                if (swipeDetected != null)
                {
                    swipeDetected(this, new KinectCursorEventArgs(point) { Z = z, Cursor = _cursorAdorner });
                }
                return;
            }
            if (_xOutOfBoundsLenght != 0 && ((_xOutOfBoundsLenght < 0 && newPoint.X - _initialSwipeX < _xOutOfBoundsLenght)
                || (_xOutOfBoundsLenght > 0 && newPoint.X - _initialSwipeX > _xOutOfBoundsLenght)))
            {
                SwipeOutOfBoundsDetected(this, new KinectCursorEventArgs(point) { Z = z, Cursor = _cursorAdorner });
            }
        }
    }
}
