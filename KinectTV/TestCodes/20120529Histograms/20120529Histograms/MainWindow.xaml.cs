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
namespace _20120529Histograms
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor _Kinect;




        private short[] _DepthImagePixelData;


        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (s, e) => { DiscoverKinectSensor(); };
            this.Unloaded += (s, e) => { this.Kinect = null; };
        }

        public KinectSensor Kinect
        {
            get { return this._Kinect; }
            set
            {
                if (this._Kinect != value)
                {
                    if (this._Kinect != null)
                    {
                        UninitializeKinectSensor(this._Kinect);
                        this._Kinect = null;
                    }
                    if (value != null && value.Status == KinectStatus.Connected)
                    {
                        this._Kinect = value;
                        InitializeKinectSensor(this._Kinect);
                    }
                }
            }
        }

        private void InitializeKinectSensor(KinectSensor sensor)
        {
            if (sensor != null)
            {
                DepthImageStream depthStream = sensor.DepthStream;
                sensor.DepthStream.Enable();



                sensor.DepthFrameReady += Kinect_DepthFrameReady;
                sensor.Start();
            }
        }

        private void UninitializeKinectSensor(KinectSensor sensor)
        {
            if (sensor != null)
            {
                sensor.Stop();
                sensor.DepthFrameReady -= Kinect_DepthFrameReady;
            }
        }
        private void DiscoverKinectSensor()
        {
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_statusChanged;
            this.Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
        }

        public void KinectSensors_statusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Connected:
                    if (this.Kinect == null)
                    {
                        this.Kinect = e.Sensor;
                    }
                    break;

                case KinectStatus.Disconnected:
                    if (this.Kinect == e.Sensor)
                    {
                        this.Kinect = null;
                        this.Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                        if (this.Kinect == null)
                        { }
                    }
                    break;
            }
        }

        private void Kinect_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (DepthImageFrame frame = e.OpenDepthImageFrame())
            {
                if (frame != null)
                {
                    this._DepthImagePixelData = new short[frame.PixelDataLength];
                    frame.CopyPixelDataTo(this._DepthImagePixelData);

                    //EnhancedDepthImage.Source = BitmapSource.Create(frame.Width, frame.Height, 96, 96, PixelFormats.Gray16, null,
                    //                                                this._DepthImagePixelData, frame.Width * frame.BytesPerPixel);
                    CreateBetterShadeOfGray(frame, this._DepthImagePixelData);
                    CreateDepthHistogram(frame, this._DepthImagePixelData);
                  
                }
            }

        }

        private void CreateDepthHistogram(DepthImageFrame depthFrame, short[] pixelData)
        {
            int depth;
            int[] depths = new int[4096];
            int maxValue = 0;
            double chartBarWidth = DepthHistogram.ActualWidth / depths.Length;

            DepthHistogram.Children.Clear();
            Console.WriteLine(pixelData.Length);
            Console.WriteLine(depthFrame.BytesPerPixel);
            for (int i = 0; i < pixelData.Length; i += depthFrame.BytesPerPixel)
            {
                depth = pixelData[i] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                if (depth > 0)
                {
                    depths[depth]++;
                }
            }

            for (int i = 0; i < depths.Length; i++)
            { 
                maxValue = Math.Max(maxValue,depths[i]);
            }

            //build the histogram
            for (int i = 0; i < depths.Length; i++)
            {
                if (depths[i] > 0)
                {
                    Rectangle r = new Rectangle();
                    r.Fill = Brushes.Black;
                    r.Width = chartBarWidth;
                    r.Height = DepthHistogram.ActualHeight * (depths[i] / (double)maxValue);
                    r.Margin = new Thickness(1, 0, 1, 0);
                    r.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                    DepthHistogram.Children.Add(r);
                }
            }

                //EnhancedDepthImage.Source = BitmapSource.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Gray16, null,
                      //                                                 enhPixeldata, depthFrame.Width * depthFrame.BytesPerPixel);
        }

        private void CreateBetterShadeOfGray(DepthImageFrame depthFrame, short[] pixelData)
        {
            int depth;
            int loThreshold = 1220;
            int hiThreshold = 3048;
            short[] enhPixeldata = new short[depthFrame.Width * depthFrame.Height];

            for (int i = 0; i < pixelData.Length; i++)
            {
                depth = pixelData[i] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                if (depth < loThreshold || depth > hiThreshold)
                {
                    enhPixeldata[i] = 0xFF;
                }
                else
                {
                    enhPixeldata[i] = (short)~pixelData[i];
                }
            }
            DepthImage.Source = BitmapSource.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Gray16, null,
                                                                   enhPixeldata, depthFrame.Width * depthFrame.BytesPerPixel);
            
        }
    }
}
