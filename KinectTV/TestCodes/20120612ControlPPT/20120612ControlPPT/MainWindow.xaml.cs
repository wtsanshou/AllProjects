using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Kinect;
using System.IO;

namespace _20120612ControlPPT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Instantiate the Kinect runtime. Required to initialize the device.
        //IMPORTANT NOTE: You can pass the device ID here, in case more than one Kinect device is connected.
        //Runtime runtime = new Runtime();
        KinectSensor _kinectDevice;
        //Skeleton[] _FrameSkeletons;

        bool isCirclesVisible = true;

        bool isForwardGestureActive = false;
        bool isBackGestureActive = false;
        SolidColorBrush activeBrush = new SolidColorBrush(Colors.Green);
        SolidColorBrush inactiveBrush = new SolidColorBrush(Colors.Red);

        private const string RecognizerId = "SR_MS_en-US_Kinect_10.0";
        bool shouldContinue = true;

        public MainWindow()
        {
            InitializeComponent();

            //Runtime initialization is handled when the window is opened. When the window
            //is closed, the runtime MUST be unitialized.
            this.Loaded += new RoutedEventHandler(WindowLoaded);
            //this.Closed += new EventHandler(WindowClosing);
            //Handle the content obtained from the video camera, once received.

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C)
            {
                ToggleCircles();
            }
            else if (e.Key == Key.S)
            {
                shouldContinue = false;
            }
        }



        private void SensorSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            Skeleton[] skeletons = new Skeleton[0];
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);

                    Skeleton skeleton = GetPrimarySkeleton(skeletons);
                    if (skeleton != null)
                    {

                        Joint head = new Joint();
                        Joint rightHand = new Joint();
                        Joint leftHand = new Joint();

                        head = skeleton.Joints[JointType.Head];
                        rightHand = skeleton.Joints[JointType.HandRight];
                        leftHand = skeleton.Joints[JointType.HandLeft];
                        SetEllipsePosition(ellipseHead, head, false);
                        SetEllipsePosition(ellipseLeftHand, leftHand, isBackGestureActive);
                        SetEllipsePosition(ellipseRightHand, rightHand, isForwardGestureActive);

                        ProcessForwardBackGesture(head, rightHand, leftHand);
                        //DateTime startMarker = DateTime.Now;
                        //this._WaveGesture.Update(this._FrameSkeletons, frame.Timestamp);

                    }
                }
            }
        }

        private Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
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



        private void ProcessForwardBackGesture(Joint head, Joint rightHand, Joint leftHand)
        {
            if (rightHand.Position.X > head.Position.X + 0.45)
            {
                if (!isBackGestureActive && !isForwardGestureActive)
                {
                    isForwardGestureActive = true;
                    System.Windows.Forms.SendKeys.SendWait("{Right}");
                }
            }
            else
            {
                isForwardGestureActive = false;
            }

            if (leftHand.Position.X < head.Position.X - 0.45)
            {
                if (!isBackGestureActive && !isForwardGestureActive)
                {
                    isBackGestureActive = true;
                    System.Windows.Forms.SendKeys.SendWait("{Left}");
                }
            }
            else
            {
                isBackGestureActive = false;
            }
        }

        //This method is used to position the ellipses on the canvas
        //according to correct movements of the tracked joints.
        private void SetEllipsePosition(Ellipse ellipse, Joint joint, bool isHighlighted)
        {
            float x, y;
            DepthImagePoint point = this._kinectDevice.MapSkeletonPointToDepth(joint.Position, DepthImageFormat.Resolution640x480Fps30);

            x = (int)((point.X * LayoutRoot.ActualWidth / this._kinectDevice.DepthStream.FrameWidth));
            y = (int)((point.Y * LayoutRoot.ActualHeight / this._kinectDevice.DepthStream.FrameHeight));


            if (isHighlighted)
            {
                ellipse.Width = 60;
                ellipse.Height = 60;
                ellipse.Fill = activeBrush;
            }
            else
            {
                ellipse.Width = 20;
                ellipse.Height = 20;
                ellipse.Fill = inactiveBrush;
            }

            Canvas.SetLeft(ellipse, x * 640 - ellipse.ActualWidth / 2);
            Canvas.SetTop(ellipse, y * 480 - ellipse.ActualHeight / 2);
        }

        void ToggleCircles()
        {
            if (isCirclesVisible)
                HideCircles();
            else
                ShowCircles();
        }

        void HideCircles()
        {
            isCirclesVisible = false;
            ellipseHead.Visibility = System.Windows.Visibility.Collapsed;
            ellipseLeftHand.Visibility = System.Windows.Visibility.Collapsed;
            ellipseRightHand.Visibility = System.Windows.Visibility.Collapsed;
        }

        void ShowCircles()
        {
            isCirclesVisible = true;
            ellipseHead.Visibility = System.Windows.Visibility.Visible;
            ellipseLeftHand.Visibility = System.Windows.Visibility.Visible;
            ellipseRightHand.Visibility = System.Windows.Visibility.Visible;
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (null != this._kinectDevice)
            {
                this._kinectDevice.Stop();
            }
        }
        private DrawingGroup drawingGroup;

        /// <summary>
        /// Drawing image that we will display
        /// </summary>
        private DrawingImage imageSource;
        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this.imageSource = new DrawingImage(this.drawingGroup);

            // Display the drawing using our image control
            videoImage.Source = this.imageSource;

            // Look through all sensors and start the first connected one.
            // This requires that a Kinect is connected at the time of app startup.
            // To make your app robust against plug/unplug, 
            // it is recommended to use KinectSensorChooser provided in Microsoft.Kinect.Toolkit
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this._kinectDevice = potentialSensor;
                    break;
                }
            }

            if (null != this._kinectDevice)
            {
                // Turn on the skeleton stream to receive skeleton frames
                this._kinectDevice.SkeletonStream.Enable();

                // Add an event handler to be called whenever there is new color frame data
                this._kinectDevice.SkeletonFrameReady += this.SensorSkeletonFrameReady;

                // Start the sensor!
                try
                {
                    this._kinectDevice.Start();
                }
                catch (IOException)
                {
                    this._kinectDevice = null;
                }
            }

        }
        /*
                private void StartSpeechRecognition()
                {
                    var t = new Thread(new ThreadStart(RecognizeAudio));
                    t.SetApartmentState(ApartmentState.MTA);
                    t.Start();
                }

                private void RecognizeAudio()
                {
                    using (var source = new KinectAudioSource())
                    {
                        source.FeatureMode = true;
                        source.AutomaticGainControl = false; //Important to turn this off for speech recognition
                        source.SystemMode = SystemMode.OptibeamArrayOnly; //No AEC for this sample
                        source.MicArrayMode = MicArrayMode.MicArrayAdaptiveBeam;
                
                        RecognizerInfo ri = SpeechRecognitionEngine.InstalledRecognizers().Where(r => r.Id == RecognizerId).FirstOrDefault();

                        if (ri == null)
                        {
                            Console.WriteLine("Could not find speech recognizer: {0}. Please refer to the sample requirements.", RecognizerId);
                            return;
                        }

                        Console.WriteLine("Using: {0}", ri.Name);

                        using (var sre = new SpeechRecognitionEngine(ri.Id))
                        {
                            var phrases = new Choices();
                            phrases.Add("computer show window");
                            phrases.Add("computer hide window");
                            phrases.Add("computer show circles");
                            phrases.Add("computer hide circles");

                            var gb = new GrammarBuilder();
                            //Specify the culture to match the recognizer in case we are running in a different culture.                                 
                            gb.Culture = ri.Culture;
                            gb.Append(phrases);

                            // Create the actual Grammar instance, and then load it into the speech recognizer.
                            var g = new Grammar(gb);

                            sre.LoadGrammar(g);
                            sre.SpeechRecognized += SreSpeechRecognized;
                            sre.SpeechHypothesized += SreSpeechHypothesized;
                            sre.SpeechRecognitionRejected += SreSpeechRecognitionRejected;

                            using (Stream s = source.Start())
                            {
                                sre.SetInputToAudioStream(s,
                                                          new SpeechAudioFormatInfo(
                                                              EncodingFormat.Pcm, 16000, 16, 1,
                                                              32000, 2, null));

                                //sre.RecognizeAsync(RecognizeMode.Multiple);
                                while (shouldContinue)
                                {
                                    sre.Recognize();
                                }
                                //sre.RecognizeAsyncStop();
                            }
                        }
                    }
                }

                void SreSpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
                {
                    Trace.WriteLine("\nSpeech Rejected, confidence: " + e.Result.Confidence);
                }

                void SreSpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
                {
                    Trace.Write("\rSpeech Hypothesized: \t{0}", e.Result.Text);
                }

                void SreSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
                {
                    //This first release of the Kinect language pack doesn't have a reliable confidence model, so 
                    //we don't use e.Result.Confidence here.
                    if (e.Result.Confidence < 0.90)
                    {
                        Trace.WriteLine("\nSpeech Rejected filtered, confidence: " + e.Result.Confidence);
                        return;
                    }

                    Trace.WriteLine("\nSpeech Recognized, confidence: " + e.Result.Confidence + ": \t{0}", e.Result.Text);
            
                    if (e.Result.Text == "computer show window")
                    {
                        this.Dispatcher.BeginInvoke((Action)delegate
                            {
                                this.Topmost = true;
                                this.WindowState = System.Windows.WindowState.Normal;
                            });
                    }
                    else if (e.Result.Text == "computer hide window")
                    {
                        this.Dispatcher.BeginInvoke((Action)delegate
                        {
                            this.Topmost = false;
                            this.WindowState = System.Windows.WindowState.Minimized;
                        });
                    }
                    else if (e.Result.Text == "computer hide circles")
                    {
                        this.Dispatcher.BeginInvoke((Action)delegate
                        {
                            this.HideCircles();
                        });
                    }
                    else if (e.Result.Text == "computer show circles")
                    {
                        this.Dispatcher.BeginInvoke((Action)delegate
                        {
                            this.ShowCircles();
                        });
                    }
                }
        */
    }
}
