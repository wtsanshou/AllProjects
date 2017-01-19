

namespace _20120606WaveGesture
{
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Kinect;
    using System;

    



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Width of output drawing
        /// </summary>
        private const float RenderWidth = 640.0f;

        /// <summary>
        /// Height of our output drawing
        /// </summary>
        private const float RenderHeight = 480.0f;

        /// <summary>
        /// Thickness of drawn joint lines
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// Thickness of body center ellipse
        /// </summary>
        private const double BodyCenterThickness = 10;

        /// <summary>
        /// Thickness of clip edge rectangles
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// Brush used to draw skeleton center point
        /// </summary>
        private readonly Brush centerPointBrush = Brushes.Blue;

        /// <summary>
        /// Brush used for drawing joints that are currently tracked
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// Brush used for drawing joints that are currently inferred
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// Pen used for drawing bones that are currently tracked
        /// </summary>
        private readonly Pen trackedBonePen = new Pen(Brushes.Green, 6);

        /// <summary>
        /// Pen used for drawing bones that are currently inferred
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensor sensor;

        /// <summary>
        /// Drawing group for skeleton rendering output
        /// </summary>
        private DrawingGroup drawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += WindowLoaded;
            this.Closing += WindowClosing;
        }

        /// <summary>
        /// Draws indicators to show which edges are clipping skeleton data
        /// </summary>
        /// <param name="skeleton">skeleton to draw clipping information for</param>
        /// <param name="drawingContext">drawing context to draw to</param>


        /// <summary>
        /// Execute startup tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this.imageSource = new DrawingImage(this.drawingGroup);

            // Display the drawing using our image control
            Image.Source = this.imageSource;

            // Look through all sensors and start the first connected one.
            // This requires that a Kinect is connected at the time of app startup.
            // To make your app robust against plug/unplug, 
            // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }
            }

            if (null != this.sensor)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this.sensor.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this.sensor.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }

        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this.sensor)
            {
                this.sensor.Stop();
            }
        }

        /// <summary>
        /// Event handler for Kinect sensor's SkeletonFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);

                    //Skeleton skeleton = GetPrimarySkeleton(skeletons);

                    
                        //WaveGesture wave = new WaveGesture();
                        Update(skeletons, 550);
                   
                }
            }

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

                if (skeletons.Length != 0)
                {
                    foreach (Skeleton skel in skeletons)
                    {


                        if (skel.TrackingState == SkeletonTrackingState.Tracked)
                        {
                            this.DrawBonesAndJoints(skel, dc);
                        }
                        else if (skel.TrackingState == SkeletonTrackingState.PositionOnly)
                        {
                            dc.DrawEllipse(
                            this.centerPointBrush,
                            null,
                            this.SkeletonPointToScreen(skel.Position),
                            BodyCenterThickness,
                            BodyCenterThickness);
                        }
                    }
                }

                // prevent drawing outside of our render area
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, RenderWidth, RenderHeight));
            }
        }


        /**************************************************************************************************************/

       // public class WaveGesture
        //{
            private const float WAVE_THRESHOLD = 0.1f;
            private const int WAVE_MOVEMENT_TIMEOUT = 5000;
            private const int REOUIRED_ITERATIONS = 4;

            private const int LEFT_HAND = 0;
            private const int RIGHT_HAND = 1;

            private int waveCount = 0;

            private WaveGestureTracker[,] _PlayerWaveTracker = new WaveGestureTracker[6, 2];

            public event EventHandler GestureDetected;

            public void Update(Skeleton[] skeletons, long frameTimestamp)
            {
                if (skeletons != null)
                {
                    Skeleton skeleton;

                    for (int i = 0; i < skeletons.Length; i++)
                    {
                        skeleton = skeletons[i];
                        if (skeleton.TrackingState != SkeletonTrackingState.NotTracked)
                        {
                            TrackWave(skeleton, true, ref this._PlayerWaveTracker[i, LEFT_HAND], frameTimestamp);
                            TrackWave(skeleton, false, ref this._PlayerWaveTracker[i, RIGHT_HAND], frameTimestamp);
                        }
                        else
                        {
                            this._PlayerWaveTracker[i, LEFT_HAND].Reset();
                            this._PlayerWaveTracker[i, RIGHT_HAND].Reset();
                        }
                    }

                }
            }

            private void TrackWave(Skeleton skeleton, bool isLeft, ref WaveGestureTracker tracker, long timestamp)
            {
                JointType handJointId = (isLeft) ? JointType.HandLeft : JointType.HandRight;
                JointType elbowJointId = (isLeft) ? JointType.ElbowLeft : JointType.ElbowRight;
                Joint hand = skeleton.Joints[handJointId];
                Joint elbow = skeleton.Joints[elbowJointId];

                if (hand.TrackingState != JointTrackingState.NotTracked && elbow.TrackingState != JointTrackingState.NotTracked)
                {
                    if (tracker.State == WaveGestureState.InProgress && tracker.Timestamp + WAVE_MOVEMENT_TIMEOUT < timestamp)
                    {
                        tracker.UpdateState(WaveGestureState.Failure, timestamp);
                    }
                    else if (hand.Position.Y > elbow.Position.Y)
                    {
                        if (hand.Position.X <= elbow.Position.X - WAVE_THRESHOLD)
                        {
                            tracker.UpdatePosition(WavePosition.Left, timestamp);
                        }
                        else if (hand.Position.X >= elbow.Position.X + WAVE_THRESHOLD)
                        {
                            tracker.UpdatePosition(WavePosition.Right, timestamp);
                        }
                        else
                        {
                            tracker.UpdatePosition(WavePosition.Neutral, timestamp);
                        }

                        if (tracker.State != WaveGestureState.Success && tracker.IterationCount == REOUIRED_ITERATIONS)
                        {
                            tracker.UpdateState(WaveGestureState.Success, timestamp);
                            //Do what you want in this place!!!!!
                            waveCount++;
                            WaveName.Text = "Wave Count: "+waveCount;

                        }
                    }
                    else
                    {
                        if (tracker.State == WaveGestureState.InProgress)
                        {
                            tracker.UpdateState(WaveGestureState.Failure, timestamp);
                        }
                        else
                        {
                            tracker.Reset();
                        }
                    }
                }
                else
                {
                    tracker.Reset();
                }
            }
       // }

        private enum WavePosition
        { 
            None =0,
            Left = 1,
            Right = 2,
            Neutral = 3
        }

        private enum WaveGestureState
        {
            None = 0,
            Success = 1,
            Failure = 2,
            InProgress = 3


        }

        private struct WaveGestureTracker
        {
            public int IterationCount;
            public WaveGestureState State;
            public long Timestamp;
            public WavePosition StartPosition;
            public WavePosition CurrentPosition;

            public void Reset()
            {
                IterationCount = 0;
                State = WaveGestureState.None;
                Timestamp = 0;
                StartPosition = WavePosition.None;
                CurrentPosition = WavePosition.None;
            }

            public void UpdateState(WaveGestureState state, long timestamp)
            {
                State = state;
                Timestamp = timestamp;
            }

            public void UpdatePosition(WavePosition position, long timestamp)
            {
                if (CurrentPosition != position)
                {
                    if (position == WavePosition.Left || position == WavePosition.Right)
                    {
                        if (State != WaveGestureState.InProgress)
                        {
                            State = WaveGestureState.InProgress;
                            IterationCount = 0;
                            StartPosition = position;
                        }
                        IterationCount++;
                    }
                    CurrentPosition = position;
                    Timestamp = timestamp;
                }
            }
        }
        
        

            


            
       
      
        /**************************************************************************************************************/
        private Point getJointPoint(Joint joint)
        {
            DepthImagePoint point = this.sensor.MapSkeletonPointToDepth(joint.Position, DepthImageFormat.Resolution640x480Fps30);

            point.X = (int)(point.X * (LayoutRoot.ActualWidth / this.sensor.DepthStream.FrameWidth));
            point.Y = (int)(point.Y * (LayoutRoot.ActualHeight / this.sensor.DepthStream.FrameHeight));

            return new Point((double)point.X, (double)point.Y);
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
        /// <summary>
        /// Draws a skeleton's bones and joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        private void DrawBonesAndJoints(Skeleton skeleton, DrawingContext drawingContext)
        {
            // Render Torso
            this.DrawBone(skeleton, drawingContext, JointType.Head, JointType.ShoulderCenter);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.ShoulderRight);
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderCenter, JointType.Spine);
            this.DrawBone(skeleton, drawingContext, JointType.Spine, JointType.HipCenter);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipLeft);
            this.DrawBone(skeleton, drawingContext, JointType.HipCenter, JointType.HipRight);

            // Left Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderLeft, JointType.ElbowLeft);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowLeft, JointType.WristLeft);
            this.DrawBone(skeleton, drawingContext, JointType.WristLeft, JointType.HandLeft);

            // Right Arm
            this.DrawBone(skeleton, drawingContext, JointType.ShoulderRight, JointType.ElbowRight);
            this.DrawBone(skeleton, drawingContext, JointType.ElbowRight, JointType.WristRight);
            this.DrawBone(skeleton, drawingContext, JointType.WristRight, JointType.HandRight);

            // Left Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipLeft, JointType.KneeLeft);
            this.DrawBone(skeleton, drawingContext, JointType.KneeLeft, JointType.AnkleLeft);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleLeft, JointType.FootLeft);

            // Right Leg
            this.DrawBone(skeleton, drawingContext, JointType.HipRight, JointType.KneeRight);
            this.DrawBone(skeleton, drawingContext, JointType.KneeRight, JointType.AnkleRight);
            this.DrawBone(skeleton, drawingContext, JointType.AnkleRight, JointType.FootRight);

            // Render Joints
            foreach (Joint joint in skeleton.Joints)
            {
                Brush drawBrush = null;

                if (joint.TrackingState == JointTrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (joint.TrackingState == JointTrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), JointThickness, JointThickness);
                }
            }
        }

        /// <summary>
        /// Maps a SkeletonPoint to lie within our render space and converts to Point
        /// </summary>
        /// <param name="skelpoint">point to map</param>
        /// <returns>mapped point</returns>
        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.MapSkeletonPointToDepth(
                                                                             skelpoint,
                                                                             DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }

        /// <summary>
        /// Draws a bone line between two joints
        /// </summary>
        /// <param name="skeleton">skeleton to draw bones from</param>
        /// <param name="drawingContext">drawing context to draw to</param>
        /// <param name="jointType0">joint to start drawing from</param>
        /// <param name="jointType1">joint to end drawing at</param>
        private void DrawBone(Skeleton skeleton, DrawingContext drawingContext, JointType jointType0, JointType jointType1)
        {
            Joint joint0 = skeleton.Joints[jointType0];
            Joint joint1 = skeleton.Joints[jointType1];

            // If we can't find either of these joints, exit
            if (joint0.TrackingState == JointTrackingState.NotTracked ||
                joint1.TrackingState == JointTrackingState.NotTracked)
            {
                return;
            }

            // Don't draw if both points are inferred
            if (joint0.TrackingState == JointTrackingState.Inferred &&
                joint1.TrackingState == JointTrackingState.Inferred)
            {
                return;
            }

            // We assume all drawn bones are inferred unless BOTH joints are tracked
            Pen drawPen = this.inferredBonePen;
            if (joint0.TrackingState == JointTrackingState.Tracked && joint1.TrackingState == JointTrackingState.Tracked)
            {
                drawPen = this.trackedBonePen;
            }

            drawingContext.DrawLine(drawPen, this.SkeletonPointToScreen(joint0.Position), this.SkeletonPointToScreen(joint1.Position));
        }
    }
}
