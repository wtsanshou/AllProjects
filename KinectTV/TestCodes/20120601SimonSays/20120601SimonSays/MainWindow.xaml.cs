

namespace _20120601SimonSays
{
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using Microsoft.Kinect;
    using System.Windows.Controls;
    using System.Windows.Shapes;
    using System;
    using System.Windows.Media.Animation;




    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const double _FeetPerMeters = 3.2808399;

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


        private Skeleton[] _FrameSkeletons;
        private GamePhase _currentPhase;
        private int _CurrentLevel;
        private int _InstructionPosition;
        private UIElement[] _InstructionSequence;
        private Random rnd = new Random();

        public enum GamePhase
        { 
            GameOver =0,
            SimonInstructing = 1,
            PlayerPerforming = 2
        }

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this._currentPhase = GamePhase.GameOver;
            this._CurrentLevel = 0;

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

            //should detect two persons!!!!
            Skeleton[] skeletons = new Skeleton[0];
            

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);

                    Skeleton skeleton = GetPrimarySkeleton(skeletons);

                    if (skeleton == null)
                    {
                        ChangePhase(GamePhase.GameOver);
                    }
                    else
                    {
                        if(this._currentPhase == GamePhase.SimonInstructing)
                        {
                            LeftHandElement.Visibility = Visibility.Collapsed;
                            RightHandElement.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            TrackHand(skeleton.Joints[JointType.HandLeft], LeftHandElement);
                            TrackHand(skeleton.Joints[JointType.HandRight], RightHandElement);

                            switch (this._currentPhase)
                            { 
                                case GamePhase.GameOver:
                                    ProcessGameOver(skeleton);
                                    break;
                                case GamePhase.PlayerPerforming:
                                    ProcessPlayerPerforming(skeleton);
                                    break;
                            }
                        }
                    }
                    
                }
            }

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                // Draw a transparent background to set the render size
                dc.DrawRectangle(Brushes.White, null, new Rect(0.0, 0.0, RenderWidth, RenderHeight));

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

        private void TrackHand(Joint hand, Image handImage)
        {
            if (hand.TrackingState == JointTrackingState.NotTracked)
            {
                //handImage.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                handImage.Visibility = System.Windows.Visibility.Visible;
                DepthImagePoint point = this.sensor.MapSkeletonPointToDepth(hand.Position, DepthImageFormat.Resolution640x480Fps30);
                point.X = (int)((point.X * LayoutRoot.ActualWidth / this.sensor.DepthStream.FrameWidth) - (handImage.ActualWidth / 2.0));
                point.Y = (int)((point.Y * LayoutRoot.ActualHeight / this.sensor.DepthStream.FrameHeight) - (handImage.ActualHeight / 2.0));

                Canvas.SetLeft(handImage, point.X);
                Canvas.SetTop(handImage, point.Y);
            }
        }

        private void ProcessGameOver(Skeleton skeleton)
        {
            if (GetHitTarget(skeleton.Joints[JointType.HandLeft], LeftHandStartElement) != null ||
                GetHitTarget(skeleton.Joints[JointType.HandRight], RightHandStartElement) != null)
            {
                ChangePhase(GamePhase.SimonInstructing);
            }
        }

        private IInputElement GetHitTarget(Joint joint, UIElement target)
        {
            Point targetPoint = GetJointPoint(joint);
            targetPoint = LayoutRoot.TranslatePoint(targetPoint, target);
            return target.InputHitTest(targetPoint);
        }

        private void ChangePhase(GamePhase newPhase)
        {
            if (newPhase != this._currentPhase)
            {
                this._currentPhase = newPhase;
                switch (this._currentPhase)
                { 
                    case GamePhase.GameOver:
                        this._CurrentLevel = 0;
                        RedBlock.Opacity = 0.2;
                        BlueBlock.Opacity = 0.2;
                        GreenBlock.Opacity = 0.2;
                        YellowBlock.Opacity = 0.2;

                        GameStateElement.Text = "Game Over!";
                        ControlCanvas.Visibility = System.Windows.Visibility.Visible;
                        GameInstructionsElement.Text = "Place hands over the targets to start a new game.";
                        break;
                    case GamePhase.SimonInstructing:
                        this._CurrentLevel++;
                        GameStateElement.Text = string.Format("Level {0}", this._CurrentLevel);
                        ControlCanvas.Visibility = System.Windows.Visibility.Collapsed;
                        GameInstructionsElement.Text = "Watch for Simon's instructions";
                        GenerateInstructions();
                        DisplayInstructions();
                        break;
                    case GamePhase.PlayerPerforming:
                        this._InstructionPosition = 0;
                        GameInstructionsElement.Text = "Repeat Simon's instructions";
                        break;
                }
            }
        }

        private IInputElement _LeftHandLastTarget;
        private IInputElement _RightHandLastTarget;

        private void ProcessPlayerPerforming(Skeleton skeleton)
        {
            IInputElement leftTarget;
            IInputElement rightTarget;
            UIElement correctTarget;

            correctTarget = this._InstructionSequence[this._InstructionPosition];
            leftTarget = GetHitTarget(skeleton.Joints[JointType.HandLeft], GameCanvas);
            rightTarget = GetHitTarget(skeleton.Joints[JointType.HandRight], GameCanvas);

            if ((leftTarget != this._LeftHandLastTarget) || (rightTarget != this._RightHandLastTarget))
            {
                if (leftTarget != null && rightTarget != null)
                {
                    ChangePhase(GamePhase.GameOver);
                }

                else if ((_LeftHandLastTarget == correctTarget && _RightHandLastTarget == null) || (_RightHandLastTarget == correctTarget && _LeftHandLastTarget == null))
                {
                    this._InstructionPosition++;
                    if (this._InstructionPosition >= this._InstructionSequence.Length)
                    {
                        ChangePhase(GamePhase.SimonInstructing);
                    }
                }
                else if (leftTarget != null || rightTarget != null)
                { }
                else
                {
                    ChangePhase(GamePhase.GameOver);
                }
                if (leftTarget != this._LeftHandLastTarget)
                {
                    AnimateHandLeave(this._LeftHandLastTarget);
                    AnimateHandEnter(leftTarget);
                    this._LeftHandLastTarget = leftTarget;
                }
                if (rightTarget != this._RightHandLastTarget)
                {
                    AnimateHandLeave(this._RightHandLastTarget);
                    AnimateHandEnter(rightTarget);
                    this._RightHandLastTarget = rightTarget;
                }
            }
        }

        private void AnimateHandEnter(IInputElement handkElement)
        {
            //handkElement.ToString();
        }

        private void AnimateHandLeave(IInputElement handkElement)
        {
            //handkElement.ToString();
        }

        private void GenerateInstructions()
        {
            this._InstructionSequence = new UIElement[this._CurrentLevel];

            for (int i = 0; i < this._CurrentLevel; i++)
            {
                switch (rnd.Next(1, 4))
                { 
                    case 1:
                        this._InstructionSequence[i] = RedBlock;
                        break;
                    case 2:
                        this._InstructionSequence[i] = BlueBlock;
                        break;
                    case 3:
                        this._InstructionSequence[i] = GreenBlock;
                        break;
                    case 4:
                        this._InstructionSequence[i] = YellowBlock;
                        break;
                }
            }
        }

        private void DisplayInstructions()
        {
            Storyboard instructionsSequence = new Storyboard();
            DoubleAnimationUsingKeyFrames animation;

            for (int i = 0; i < this._InstructionSequence.Length; i++)
            {
                animation = new DoubleAnimationUsingKeyFrames();
                animation.FillBehavior = FillBehavior.Stop;
                animation.BeginTime = TimeSpan.FromMilliseconds(i * 500);
                Storyboard.SetTarget(animation, this._InstructionSequence[i]);
                Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
                instructionsSequence.Children.Add(animation);

                animation.KeyFrames.Add(new EasingDoubleKeyFrame(0.3, KeyTime.FromTimeSpan(TimeSpan.Zero)));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(500))));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1000))));
                animation.KeyFrames.Add(new EasingDoubleKeyFrame(0.3, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(1300))));
            }

            instructionsSequence.Completed += (s, e) =>
                {
                    ChangePhase(GamePhase.PlayerPerforming);
                };
            instructionsSequence.Begin(LayoutRoot);
        }


        

        private Point GetJointPoint(Joint joint)
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

