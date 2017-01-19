using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;


namespace _20120607HandTracking.Manage
{
    public class KinectCurosrEventArgs : RoutedEventArgs
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public CursorAdorner Cursor { get; set; }
        
        public KinectCurosrEventArgs(double x, double y)
        {
            X = x;
            Y = y;
        }
        public KinectCurosrEventArgs(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
    }
}
