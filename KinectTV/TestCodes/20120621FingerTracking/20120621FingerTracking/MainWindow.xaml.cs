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

namespace _20120621FingerTracking
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

        private ImageSource _imageSource;

        public MainWindow()
        {
            InitializeComponent();

            

            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            // Create the drawing group we'll use for drawing
            this.drawingGroup = new DrawingGroup();

            // Create an image source that we can use in our image control
            this._imageSource = new DrawingImage(this.drawingGroup);

            // Display the drawing using our image control
            HandImage.Source = this._imageSource;

            DiscoverKinectSensor();
            //book has fault
            if (this._Kinect != null)
            {
                try
                {
                    ColorImageStream colorStream = this._Kinect.ColorStream;
                    DepthImageStream depthStream = this._Kinect.DepthStream;
                    SkeletonStream skeletonStream = this._Kinect.SkeletonStream;
                    using (SkeletonFrame skeletonFrame = skeletonStream.OpenNextFrame(100))
                    {
                        //using (ColorImageFrame colorFrame = colorStream.OpenNextFrame(100))
                        //{
                        using (DepthImageFrame depthFrame = depthStream.OpenNextFrame(100))
                        {
                            RenderGreenScreen(this._Kinect, depthFrame, skeletonFrame);
                        }
                        // }
                    }
                }
                catch
                { }
                
                
            }

        }

        private void RenderGreenScreen(KinectSensor kinectSensor, DepthImageFrame depthFrame, SkeletonFrame skeletonFrame)
        {
            int depthPixelIndex;
            int playerIndex;
            //int colorPixelIndex;
            //ColorImagePoint colorPoint;
            //int colorStride = colorFrame.BytesPerPixel * colorFrame.Width;
            int bytesPerpixel = 4;
            byte[] playerImage = new byte[depthFrame.Height * this._GreenScreenImageStride];

            
            int playerImageIndex = 0;

            Joint hand = new Joint();
            DepthImagePoint point;

            int x =0;
            int y =0;
            int handDepth =0;
            if (skeletonFrame != null)
            {
                Skeleton[] skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                skeletonFrame.CopySkeletonDataTo(skeletons);

                Skeleton skeleton = GetPrimarySkeleton(skeletons);
                if (skeleton != null)
                {
                    hand = skeleton.Joints[JointType.HandRight];

                    point = this._Kinect.MapSkeletonPointToDepth(hand.Position, DepthImageFormat.Resolution640x480Fps30);
                    x = (int)point.X;
                    y = (int)point.Y;
                   
                }
            }

            DrawCircle(x, y);

            depthFrame.CopyPixelDataTo(this._DepthImagePixelData);
            //colorFrame.CopyPixelDataTo(this._ColorPixelData);

            handDepth = this._DepthImagePixelData[y * depthFrame.Width + x] >> DepthImageFrame.PlayerIndexBitmaskWidth;

            this._handOutline = new List<Point>();

            int depthLeft = 0;
            int depthRight = 0;
            int depthTop = 0;
            int depthBottom = 0;

            for (int depthY = 0; depthY < depthFrame.Height; depthY++)
            {
                for (int depthX = 0; depthX < depthFrame.Width; depthX++, playerImageIndex += bytesPerpixel)
                {
                    depthPixelIndex = depthX + (depthY * depthFrame.Width);
                    playerIndex = this._DepthImagePixelData[depthPixelIndex] & DepthImageFrame.PlayerIndexBitmask;
                    int depth = this._DepthImagePixelData[depthY * depthFrame.Width + depthX] >> DepthImageFrame.PlayerIndexBitmaskWidth;

                    
                    if (depthY > 0 && depthY < depthFrame.Height - 1 && depthX > 0 && depthX < depthFrame.Width - 1)
                    {
                        depthLeft = this._DepthImagePixelData[depthY * depthFrame.Width + depthX - 1] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                        depthRight = this._DepthImagePixelData[depthY * depthFrame.Width + depthX + 1] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                        depthTop = this._DepthImagePixelData[(depthY - 1) * depthFrame.Width + depthX] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                        depthBottom = this._DepthImagePixelData[(depthY + 1) * depthFrame.Width + depthX] >> DepthImageFrame.PlayerIndexBitmaskWidth;
                    }

                    if (playerIndex != 0 &&
                        depth >= handDepth - 50 && depth < handDepth + 50 &&
                        depthX >= x - 50 && depthX < x + 50 && depthY >= y - 50 && depthY < y + 50)
                    
                    {
                        //book has fault
                        /*
                       colorPoint = kinectSensor.MapDepthToColorImagePoint(depthFrame.Format, depthX, depthY, this._DepthImagePixelData[depthPixelIndex],
                                                                           colorFrame.Format);
                       colorPixelIndex = (colorPoint.X * colorFrame.BytesPerPixel) + (colorPoint.Y * colorStride);
                      
                       playerImage[playerImageIndex] = this._ColorPixelData[colorPixelIndex];
                       playerImage[playerImageIndex + 1] = this._ColorPixelData[colorPixelIndex + 1];
                       playerImage[playerImageIndex + 2] = this._ColorPixelData[colorPixelIndex + 1];
                       playerImage[playerImageIndex + 3] = 0xFF;
                  */
                        /*
                        if ((Math.Abs(depthLeft - depth) > 50) || (Math.Abs(depthRight - depth) > 50) ||
                            (Math.Abs(depthTop - depth) > 50) || (Math.Abs(depthBottom - depth) > 50))
                        {*/
                            Point p = new Point(depthX - x, depthY - y);
                            this._handOutline.Add(p);
                            
                        
                    }
                }
            }
            DrawHandOutline(this._handOutline);
            this._GreenScreenImage.WritePixels(this._GreenScreenImageRect, playerImage, this._GreenScreenImageStride, 0);
        }

        List<Point> _handOutline;
        Pen drawPen;
        private DrawingGroup drawingGroup;

        private void DrawHandOutline(List<Point> handPoint)
        {
            //String test = "";
            //FindFingers ff = new FindFingers(1, handPoint);
            
            //QuickSort qs = new QuickSort(handPoint);
            //List<Point> handSort = qs.getSort(1);
            
           
            //XY.Items.Add( = test;

            //outline from the whole hand
            List<Point> outline = new List<Point>();
            FindFingers ff = new FindFingers(1, handPoint);
            if (handPoint != null)
            {
                outline = ff.SortHandOutine();
            }
            //XY.Items.Clear();
            Console.WriteLine("----------------------------"+handPoint.Count+"----------------");
            for (int i = 0; i < outline.Count - 1; i++)
            {
                Console.WriteLine(outline[i].X + "\t" + outline[i].Y + "\n");
            }
            drawPen = new Pen(Brushes.Blue, 1);

            using (DrawingContext dc = this.drawingGroup.Open())
            {
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, 640.0f, 480.0f));
                if (handPoint.Count > 0)
                {
                    for (int i = 0; i < handPoint.Count - 1; i++)
                    {
                        dc.DrawLine(drawPen, handPoint[i], handPoint[i+1]);
                    }
                }
                
                this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, 640.0f, 480.0f));
            }

        }

        private void DrawCircle(int x, int y)
        {
            Canvas.SetLeft(hand, x);
            Canvas.SetTop(hand, y);
            hand.Visibility = System.Windows.Visibility.Visible;
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
                    this._Kinect.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);

                    DepthImageStream depthStream = this._Kinect.DepthStream;
                    this._GreenScreenImage = new WriteableBitmap(depthStream.FrameWidth, depthStream.FrameHeight, 96, 96, PixelFormats.Bgra32, null);
                    //book has fault
                    this._GreenScreenImageRect = new Int32Rect(0, 0,
                                                            (int)Math.Ceiling((double)depthStream.FrameWidth),
                                                            (int)Math.Ceiling((double)depthStream.FrameHeight));

                    this._GreenScreenImageStride = depthStream.FrameWidth * 4;
                    this.HandImage.Source = this._GreenScreenImage;

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

