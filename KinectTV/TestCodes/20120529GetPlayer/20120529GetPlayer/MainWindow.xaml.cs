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
namespace _20120529GetPlayer
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
                sensor.SkeletonStream.Enable();


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

                    CreateDepthImage(frame, this._DepthImagePixelData);
                    GeneratePlayerDepthImage(frame, this._DepthImagePixelData);
                }
            }

        }

        private void CreateDepthImage(DepthImageFrame depthFrame, short[] pixelData)
        {

            int depth;
            int gray;
          
            int bytesPerPixel = 4;
            byte[] enhPixeldata = new byte[depthFrame.Width * depthFrame.Height * bytesPerPixel];

            for (int i = 0, j = 0; i < pixelData.Length; i++, j += bytesPerPixel)
            {
                depth = pixelData[i] >> DepthImageFrame.PlayerIndexBitmaskWidth;
               
                    gray = (255 * depth / 0xFFF);

                enhPixeldata[j] = (byte)gray;
                enhPixeldata[j + 1] = (byte)gray;
                enhPixeldata[j + 2] = (byte)gray;
            }
            DepthImage.Source = BitmapFrame.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Bgr32,
                                                                null, enhPixeldata, depthFrame.Width * bytesPerPixel);
        }

        private void GeneratePlayerDepthImage(DepthImageFrame depthFrame, short[] pixelData)
        {
            
            int playerIndex = 0;
            int depthBytePerPixel = 4;
            byte[] enhPixelData = new byte[depthFrame.Height * depthFrame.Width * depthBytePerPixel];

            for (int i = 0, j = 0; i < pixelData.Length; i++, j += depthBytePerPixel)
            {
                playerIndex = pixelData[i] & DepthImageFrame.PlayerIndexBitmask;
                if (playerIndex == 0)
                {
                    enhPixelData[j] = 0xFF;
                    enhPixelData[j + 1] = 0xFF;
                    enhPixelData[j + 2] = 0xFF;
                }
                else
                {
                    enhPixelData[j] = 0x00;
                    enhPixelData[j + 1] = 0x00;
                    enhPixelData[j + 2] = 0x00;
                }
            }
            player.Source = BitmapFrame.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Bgr32,
                                                                null, enhPixelData, depthFrame.Width * depthBytePerPixel);
        }
    }
}
