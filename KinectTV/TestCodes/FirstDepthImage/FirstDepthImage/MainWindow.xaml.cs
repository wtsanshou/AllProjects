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

namespace FirstDepthImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor _Kinect;

        private DepthImageFrame _LastDepthFrame;
        private Int32Rect _RawDepthImageRect;
        private WriteableBitmap _RawDepthImage;
        private short[] _DepthImagePixelData;
        private int _RawDepthImageStride;
        

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

                this._RawDepthImage = new WriteableBitmap(depthStream.FrameWidth, depthStream.FrameHeight, 96, 96, PixelFormats.Gray16, null);
                
                this._RawDepthImageRect = new Int32Rect(0, 0, depthStream.FrameWidth, depthStream.FrameHeight);
                this._RawDepthImageStride = depthStream.FrameWidth * depthStream.FrameBytesPerPixel;
                DepthImage.Source = this._RawDepthImage;


            

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
           
                if (this._LastDepthFrame != null)
                {
                    this._LastDepthFrame.Dispose();
                    this._LastDepthFrame = null;

                    
                }
                this._LastDepthFrame = e.OpenDepthImageFrame();
                if (this._LastDepthFrame != null)
                {
                    this._DepthImagePixelData = new short[_LastDepthFrame.PixelDataLength];
                    this._LastDepthFrame.CopyPixelDataTo(this._DepthImagePixelData);
                    this._RawDepthImage.WritePixels(this._RawDepthImageRect, this._DepthImagePixelData, this._RawDepthImageStride, 0);
                   
                }

        }

        private void DepthImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(DepthImage);

            if (this._DepthImagePixelData != null && this._DepthImagePixelData.Length > 0)
            {
                int pixelIndex = (int)(p.X + ((int)p.Y * this._LastDepthFrame.Width));
                int depth = this._DepthImagePixelData[pixelIndex] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                int depthInches = (int)(depth * 0.0393700787);
                int depthFt = depthInches / 12;
                depthInches = depthInches % 12;

                PixelDepth.Text = string.Format("{0}mm ~ {1}'{2}\"", depth, depthFt, depthInches);
            }
        }

        private void CreateLighterShadesOfGray(DepthImageFrame depthFrame, short[] pixelData)
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

                EnhancedDepthImage.Source = BitmapSource.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Gray16, null,
                                                                    enhPixeldata, depthFrame.Width * depthFrame.BytesPerPixel);
            }
        }

        private void CreateBetterShadeOfGray(DepthImageFrame depthFrame, short[] pixelData)
        {
            int depth;
            int gray;
            int loThreshold = 1220;
            int hiThreshold = 3048;
            byte[] enhPixeldata = new byte[depthFrame.Width * depthFrame.Height*depthFrame.BytesPerPixel];

            for (int i = 0, j = 0; i < pixelData.Length; i++, j += depthFrame.BytesPerPixel)
            {
                depth = pixelData[i] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                if (depth < loThreshold || depth > hiThreshold)
                {
                    gray = 0xFF;
                }
                else
                {
                    gray = (255 * depth / 0xFFF);
                }
                enhPixeldata[j] = (byte)gray;
                enhPixeldata[j+1] = (byte)gray;
                enhPixeldata[j+2] = (byte)gray;
            }
            BetterShadeDepthImage.Source = BitmapFrame.Create(depthFrame.Width, depthFrame.Height, 96, 96, PixelFormats.Bgr32,
                                                                null,enhPixeldata, depthFrame.Width * depthFrame.BytesPerPixel);
        }
    }
}
