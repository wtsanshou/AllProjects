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

using _20120608HandTrackingLibrary;
using Microsoft.Kinect;
using Microsoft.Speech.Recognition;
using _20120718KinectTV.Speech;
using System.Threading;
using System.ComponentModel;
using _20120718KinectTV.Gesture;
using System.Media;
using System.Diagnostics;

namespace _20120718KinectTV
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private KinectSensor _kinectDevice;
        private Skeleton[] _FrameSkeletons;

        
        private const string SoundURI = "/Voice/";

        /// <summary>
        /// Array of arrays of contiguous line segements that represent a skeleton.
        /// </summary>
        private static readonly JointType[][] SkeletonSegmentRuns = new JointType[][]
        {
            new JointType[] 
            { 
                JointType.Head, JointType.ShoulderCenter, JointType.HipCenter 
            },
            new JointType[] 
            { 
                JointType.HandLeft, JointType.WristLeft, JointType.ElbowLeft, JointType.ShoulderLeft,
                JointType.ShoulderCenter,
                JointType.ShoulderRight, JointType.ElbowRight, JointType.WristRight, JointType.HandRight
            },
            new JointType[]
            {
                JointType.FootLeft, JointType.AnkleLeft, JointType.KneeLeft, JointType.HipLeft,
                JointType.HipCenter,
                JointType.HipRight, JointType.KneeRight, JointType.AnkleRight, JointType.FootRight
            }
        };

        public MainWindow()
        {
            InitializeComponent();
            
            
            this.Unloaded += delegate
            {
                _kinectDevice.SkeletonStream.Disable();
            };
            this.Loaded += delegate
            {
                this._kinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                this._kinectDevice.SkeletonFrameReady += KinectDevice_SkeletonFrameReady;
                this._kinectDevice.Start();
            };
        }

        private void KinectDevice_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    this._FrameSkeletons = new Skeleton[_kinectDevice.SkeletonStream.FrameSkeletonArrayLength];
                    frame.CopySkeletonDataTo(this._FrameSkeletons);

                    Skeleton skeleton = GetPrimarySkeleton(this._FrameSkeletons);
                    if (skeleton != null)
                    {
                        if (isGesturePage)
                        {
                            //DateTime startMarker = DateTime.Now;
                            //this._WaveGesture.Update(this._FrameSkeletons, frame.Timestamp);

                            this._gesture.TrackPose(skeleton);
                        }

                        this.DrawStickMen(skeleton);
                    }
                }
            }
        }

        private void DrawStickMen(Skeleton skeleton)
        {
            // Remove any previous skeletons.
            StickMen.Children.Clear();

            
                if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                {
                    // Draw a background for the next pass.
                    this.DrawStickMan(skeleton, Brushes.CornflowerBlue, 7);
                }
           
            
        }

        

        /// <summary>
        /// Draw an individual skeleton.
        /// </summary>
        /// <param name="skeleton">The skeleton to draw.</param>
        /// <param name="brush">The brush to use.</param>
        /// <param name="thickness">This thickness of the stroke.</param>
        private void DrawStickMan(Skeleton skeleton, Brush brush, int thickness)
        {
            Debug.Assert(skeleton.TrackingState == SkeletonTrackingState.Tracked, "The skeleton is being tracked.");

            foreach (var run in SkeletonSegmentRuns)
            {
                var next = this.GetJointPoint(skeleton, run[0]);
                for (var i = 1; i < run.Length; i++)
                {
                    var prev = next;
                    next = this.GetJointPoint(skeleton, run[i]);

                    var line = new Line
                    {
                        Stroke = brush,
                        StrokeThickness = thickness,
                        X1 = prev.X,
                        Y1 = prev.Y,
                        X2 = next.X,
                        Y2 = next.Y,
                        StrokeEndLineCap = PenLineCap.Round,
                        StrokeStartLineCap = PenLineCap.Round
                    };

                    StickMen.Children.Add(line);
                }
            }
        }

        private Point GetJointPoint(Skeleton skeleton, JointType jointType)
        {
            var joint = skeleton.Joints[jointType];

            // Points are centered on the StickMen canvas and scaled according to its height allowing
            // approximately +/- 1.5m from center line.
            var point = new Point
            {
                X = (StickMen.Width / 2) + (StickMen.Height * joint.Position.X / 3),
                Y = (StickMen.Width / 2) - (StickMen.Height * joint.Position.Y / 3)
            };

            return point;
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

        private void Button_KinectCursorLeave(object sender, KinectCursorEventArgs e)
        {

        }

        private void TV_Click(object sender, RoutedEventArgs e)
        {
            firstPage.Visibility= Visibility.Hidden;
            Grid.SetColumn(firstPage, 1);
            GSPage.Visibility = Visibility.Visible;
            Grid.SetColumn(GSPage, 0);
        }

        

        private void GSBack_Click(object sender, RoutedEventArgs e)
        {
            GSPage.Visibility = Visibility.Hidden;
            Grid.SetColumn(GSPage, 1);
            firstPage.Visibility = Visibility.Visible;
            Grid.SetColumn(firstPage, 0);
            
        }

        private bool isGesturePage = false;
        //private WaveGesture _WaveGesture;

        private GestureManager _gesture;

        private void Gesture_Click(object sender, RoutedEventArgs e)
        {
            GSPage.Visibility = Visibility.Hidden;
            Grid.SetColumn(GSPage, 1);
            GesturePage.Visibility = Visibility.Visible;
            Grid.SetColumn(GesturePage, 0);

            //this._WaveGesture = new WaveGesture();
            isGesturePage = true;

            this._gesture = new GestureManager(this._kinectDevice);

            this.DataContext = this._gesture;

            
        }

        private void GestureBack_Click(object sender, RoutedEventArgs e)
        {
            isGesturePage = false;
            //this._WaveGesture = null;
            this._gesture.ClosePort();
            this._gesture = null;
            
            GesturePage.Visibility = Visibility.Hidden;
            Grid.SetColumn(GesturePage, 1);
            GSPage.Visibility = Visibility.Visible;
            Grid.SetColumn(GSPage, 0);

        }

        private SpeechManager _sm;
       
        private KinectAudioSource _source;
        

        private KinectAudioSource CreateAudioSource()
        {
            var source = KinectSensor.KinectSensors[0].AudioSource;
            source.AutomaticGainControlEnabled = false;
            source.EchoCancellationMode = EchoCancellationMode.None;
            return source;
        }

        private void Speech_Click(object sender, RoutedEventArgs e)
        {
            GSPage.Visibility = Visibility.Hidden;
            Grid.SetColumn(GSPage, 1);
            SpeechPage.Visibility = Visibility.Visible;
            Grid.SetColumn(SpeechPage, 0);

            

            this._sm = new SpeechManager();

            this.DataContext = this._sm;

            this._source = CreateAudioSource();

            this._sm.StartSpeechRecognition(this._source, Dispatcher);

            this._sm.QuitDetected += new EventHandler(SpeechBack_Click);

            
        }

        private void SpeechBack_Click(object sender, EventArgs e)
        {
            this._sm.CloseSpeech();
            this._sm = null;

            SpeechPage.Visibility = Visibility.Hidden;
            Grid.SetColumn(SpeechPage, 1);
            GSPage.Visibility = Visibility.Visible;
            Grid.SetColumn(GSPage, 0);
            
        }

        private void Play(String FileName)
        {
            using (SoundPlayer player = new SoundPlayer())
            {
                string location = System.Environment.CurrentDirectory + "\\Voices\\" + FileName;
                player.SoundLocation = location;
                player.Play();
            }
        }

        private void SoundNextGuidence_Click(object sender, RoutedEventArgs e)
        {
            Play("NextPage.WAV");
        }
        private void SoundPreviousGuidence_Click(object sender, RoutedEventArgs e)
        {
            Play("PreviousPage.WAV");
        }
        private void SoundTurnOnTV_Click(object sender, RoutedEventArgs e)
        {
            Play("TurnOnTV.WAV");
        }
        private void SoundTurnOffTV_Click(object sender, RoutedEventArgs e)
        {
            Play("TurnOffTV.WAV");
        }
        private void SoundNextChannel_Click(object sender, RoutedEventArgs e)
        {
            Play("NextChannel.WAV");
        }
        private void SoundPreviousChannel_Click(object sender, RoutedEventArgs e)
        {
            Play("PreviousChannel.WAV");
        }
        private void SoundIncreaseSound_Click(object sender, RoutedEventArgs e)
        {
            Play("IncreaseSound.WAV");
        }
        private void SoundDecreaseSound_Click(object sender, RoutedEventArgs e)
        {
            Play("DecreaseSound.WAV");
        }
        private void SoundChannel1_Click(object sender, RoutedEventArgs e)
        {
            Play("Channel1.WAV");
        }
        private void SoundChannel2_Click(object sender, RoutedEventArgs e)
        {
            Play("Channel2.WAV");
        }
        
    }
}
