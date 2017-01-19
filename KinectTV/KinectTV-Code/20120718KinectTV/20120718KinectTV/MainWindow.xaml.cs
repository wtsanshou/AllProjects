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

namespace _20120718KinectTV
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private KinectSensor _kinectDevice;
        private Skeleton[] _FrameSkeletons;

        

        public MainWindow()
        {
            InitializeComponent();
            this._sm = new SpeechManager();
            this.DataContext = this._sm;

            
            this.Unloaded += delegate
            {
                _kinectDevice.SkeletonStream.Disable();
                _sre.RecognizeAsyncCancel();
                _sre.RecognizeAsyncStop();
                //_source.Dispose();
                _sre.Dispose();
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

                    DateTime startMarker = DateTime.Now;
                    //this._WaveGesture.Update(this._FrameSkeletons, frame.Timestamp);
                    
                }
            }
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

        private void Gesture_Click(object sender, RoutedEventArgs e)
        {
            GSPage.Visibility = Visibility.Hidden;
            Grid.SetColumn(GSPage, 1);
            GesturePage.Visibility = Visibility.Visible;
            Grid.SetColumn(GesturePage, 0);

        }

        private void GestureBack_Click(object sender, RoutedEventArgs e)
        {
            GesturePage.Visibility = Visibility.Hidden;
            Grid.SetColumn(GesturePage, 1);
            GSPage.Visibility = Visibility.Visible;
            Grid.SetColumn(GSPage, 0);

        }

        private SpeechManager _sm;
        private SpeechRecognitionEngine _sre;
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

            this._sre = new SpeechRecognitionEngine();

            this._source = CreateAudioSource();

            

           

            this._sm.StartSpeechRecognition(this._sre, this._source);
           
           
        }

        private void SpeechBack_Click(object sender, RoutedEventArgs e)
        {
            

            this._sm.CloseSpeech(this._sre);
            SpeechPage.Visibility = Visibility.Hidden;
            Grid.SetColumn(SpeechPage, 1);
            GSPage.Visibility = Visibility.Visible;
            Grid.SetColumn(GSPage, 0);
            
        }

       
    }
}
