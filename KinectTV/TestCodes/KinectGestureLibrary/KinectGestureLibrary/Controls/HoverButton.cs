using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Windows;

namespace KinectGestureLibrary
{
    public class HoverButton : KinectButton
    {
        readonly DispatcherTimer _hoverTimer = new DispatcherTimer();
        protected bool _timerEnabled = true;

        public double HoverInterval
        {
            get { return (double)GetValue(HoverIntervalProperty); }
            set { SetValue(HoverIntervalProperty, value); }
        }

        public static readonly DependencyProperty HoverIntervalProperty =
            DependencyProperty.Register("HoverInterval", typeof(double), typeof(HoverButton), new UIPropertyMetadata(2000d));

        //p202 The Heart of the HoverButton
        public HoverButton()
        {
            _hoverTimer.Interval = TimeSpan.FromMilliseconds(HoverInterval);
            _hoverTimer.Tick += _hoverTimer_Tick;
            _hoverTimer.Stop();
        }
        void _hoverTimer_Tick(object sender, EventArgs e)
        {
            _hoverTimer.Stop();
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }

        protected override void OnKinectCursorLeave(object sender, KinectCursorEventArgs e)
        {
            if (_timerEnabled)
            {
                e.Cursor.StopCursorAnimation();
                _hoverTimer.Stop();
            }
        }

        protected override void OnKinectCursorEnter(object sender, KinectCursorEventArgs e)
        {
            if (_timerEnabled)
            {
                _hoverTimer.Interval = TimeSpan.FromMilliseconds(HoverInterval);
                e.Cursor.AnimateCursor(HoverInterval);
                _hoverTimer.Start();
            }
        }
    }
}
