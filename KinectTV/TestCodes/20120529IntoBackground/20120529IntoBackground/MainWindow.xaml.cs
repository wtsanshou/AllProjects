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
namespace _20120529IntoBackground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private KinectSensor _Kinect;
        private short[] _DepthImagePixelData;
        private WriteableBitmap _GreenScreenImage;
        private Int32Rect _GreenScreenImageRect;
        private int _GreenScreenImageStride;
        private byte[] _ColorPixelData;

        public MainWindow()
        {
            InitializeComponent();

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            DiscoverKinectSensor();
            //book has fault
            if (this._Kinect != null)
            {
                try
                {
                    ColorImageStream colorStream = this._Kinect.ColorStream;
                    DepthImageStream depthStream = this._Kinect.DepthStream;

                    using (ColorImageFrame colorFrame = colorStream.OpenNextFrame(100))
                    {
                        using (DepthImageFrame depthFrame = depthStream.OpenNextFrame(100))
                        {
                            RenderGreenScreen(this._Kinect, colorFrame, depthFrame);
                        }
                    }
                }
                catch (Exception)
                { 
                
                }
            }
            
        }

        private void RenderGreenScreen(KinectSensor kinectSensor, ColorImageFrame colorFrame, DepthImageFrame depthFrame)
        {
            int depthPixelIndex;
            int playerIndex;
            int colorPixelIndex;
            ColorImagePoint colorPoint;
            int colorStride = colorFrame.BytesPerPixel * colorFrame.Width;
            int bytesPerpixel = 4;
            byte[] playerImage = new byte[depthFrame.Height * this._GreenScreenImageStride];
            int playerImageIndex = 0;

            depthFrame.CopyPixelDataTo(this._DepthImagePixelData);
            colorFrame.CopyPixelDataTo(this._ColorPixelData);

            for (int depthY = 0; depthY < depthFrame.Height; depthY++)
            {
                for (int depthX = 0; depthX < depthFrame.Width; depthX++, playerImageIndex += bytesPerpixel)
                {
                    depthPixelIndex = depthX + (depthY * depthFrame.Width);
                    playerIndex = this._DepthImagePixelData[depthPixelIndex] & DepthImageFrame.PlayerIndexBitmask;

                    if (playerIndex != 0)
                    {
                                                                            //book has fault
                        colorPoint = kinectSensor.MapDepthToColorImagePoint(depthFrame.Format,depthX, depthY, this._DepthImagePixelData[depthPixelIndex],
                                                                            colorFrame.Format);
                        colorPixelIndex = (colorPoint.X * colorFrame.BytesPerPixel) + (colorPoint.Y * colorStride);

                        playerImage[playerImageIndex] = this._ColorPixelData[colorPixelIndex];
                        playerImage[playerImageIndex + 1] = this._ColorPixelData[colorPixelIndex + 1];
                        playerImage[playerImageIndex + 2] = this._ColorPixelData[colorPixelIndex + 1];
                        playerImage[playerImageIndex + 3] = 0xFF;
                    }
                }
            }
            this._GreenScreenImage.WritePixels(this._GreenScreenImageRect, playerImage, this._GreenScreenImageStride, 0);
        }



        private void DiscoverKinectSensor()
        {
            if (this._Kinect != null && this._Kinect.Status != KinectStatus.Connected)
            {
                this._Kinect.ColorStream.Disable();
                this._Kinect.DepthStream.Disable();
                this._Kinect.SkeletonStream.Disable();
                this._Kinect.Stop();
                this._Kinect = null;
            }

            if (this._Kinect == null)
            {
                this._Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                if (this._Kinect != null)
                {
                    this._Kinect.SkeletonStream.Enable();

                    this._Kinect.ColorStream.Enable(ColorImageFormat.RgbResolution1280x960Fps12);
                    this._Kinect.DepthStream.Enable(DepthImageFormat.Resolution320x240Fps30);

                    DepthImageStream depthStream = this._Kinect.DepthStream;
                    this._GreenScreenImage = new WriteableBitmap(depthStream.FrameWidth, depthStream.FrameHeight, 96, 96, PixelFormats.Bgra32, null);
                                                                //book has fault
                    this._GreenScreenImageRect = new Int32Rect(0, 0,
                                                            (int)Math.Ceiling((double)depthStream.FrameWidth),
                                                            (int)Math.Ceiling((double)depthStream.FrameHeight));

                    this._GreenScreenImageStride = depthStream.FrameWidth * 4;
                    this.GreenScreenImage.Source = this._GreenScreenImage;

                    this._DepthImagePixelData = new short[depthStream.FramePixelDataLength];
                    //book has fault
                    //int colorFramePixelDataLength = this._ColorPixelData =
                    this._ColorPixelData = new byte[this._Kinect.ColorStream.FramePixelDataLength];


                    this._Kinect.Start();
                    
                }
            }
        }
    }
}
