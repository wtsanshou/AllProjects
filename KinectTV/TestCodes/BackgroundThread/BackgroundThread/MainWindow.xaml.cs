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
using System.ComponentModel;

namespace BackgroundThread
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         private KinectSensor _Kinect;
        private WriteableBitmap _ColorImageBitmap;
        private Int32Rect _ColorImageBitmapRect;
        private int _ColorImageStride;
        private byte[] _ColorImagePixelData;
        private BackgroundWorker _Worker;

        public MainWindow()
        {
            InitializeComponent();

            this._Worker = new BackgroundWorker();
            this._Worker.DoWork += Worker_DoWork;
            this._Worker.RunWorkerAsync();

            this.Unloaded += (s, e) => { this._Worker.CancelAsync(); };
        }

        public void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (worker != null)
            {
                while (!worker.CancellationPending)
                {
                    DiscoverKinectSensor();
                    PlooColorImageStream();
                }
            }
        }

        private void DiscoverKinectSensor()
        {
            if (this._Kinect != null && this._Kinect.Status != KinectStatus.Connected)
            {
                this._Kinect = null;
            }

            if (this._Kinect == null)
            {
                this._Kinect = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
                if (this._Kinect != null)
                {
                    this._Kinect.ColorStream.Enable();
                    this._Kinect.Start();
                    ColorImageStream colorStream = this._Kinect.ColorStream;
                    this.ColorImageElement.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this._ColorImageBitmap = new WriteableBitmap(colorStream.FrameWidth, colorStream.FrameHeight, 96, 96, PixelFormats.Bgr32, null);
                        this._ColorImageBitmapRect = new Int32Rect(0, 0, colorStream.FrameWidth, colorStream.FrameHeight);
                        this._ColorImageStride = colorStream.FrameWidth * colorStream.FrameBytesPerPixel;

                        this._ColorImagePixelData = new byte[colorStream.FramePixelDataLength];
                        this.ColorImageElement.Source = this._ColorImageBitmap;
                    }));
                }
            }
        }

        private void PlooColorImageStream()
        {
            if (this._Kinect == null)
            {

            }
            else
            {
                try
                {
                    using (ColorImageFrame frame = this._Kinect.ColorStream.OpenNextFrame(100))
                    {
                        if (frame != null)
                        {
                            frame.CopyPixelDataTo(this._ColorImagePixelData);
                            this.ColorImageElement.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                this._ColorImageBitmap.WritePixels(this._ColorImageBitmapRect, this._ColorImagePixelData, this._ColorImageStride, 0);
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        
    }
}
