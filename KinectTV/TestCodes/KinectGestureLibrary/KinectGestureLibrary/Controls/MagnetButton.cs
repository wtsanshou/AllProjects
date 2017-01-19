using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Controls;

namespace KinectGestureLibrary
{
    public class MagnetButton : HoverButton
    {
        public static readonly RoutedEvent KinectCursorLockEvent =
            KinectInput.KinectCursorLockEvent.AddOwner(typeof(MagnetButton));

        public static readonly RoutedEvent KinectCursorUnlockEvent =
            KinectInput.KinectCursorUnlockEvent.AddOwner(typeof(MagnetButton));

        public event KinectCursorEventHandler KinectCursorLock
        {
            add { base.AddHandler(KinectCursorLockEvent, value); }
            remove { base.RemoveHandler(KinectCursorLockEvent, value); }
        }

        public event KinectCursorEventHandler KinectCursorUnlock
        {
            add { base.AddHandler(KinectCursorUnlockEvent, value); }
            remove { base.RemoveHandler(KinectCursorUnlockEvent, value); }
        }

        public double LockInterval
        {
            get { return (double)GetValue(LockIntervalProperty); }
            set { SetValue(LockIntervalProperty, value); }
        }

        public static readonly DependencyProperty LockIntervalProperty =
            DependencyProperty.Register("LockInterval", typeof(double), typeof(MagnetButton), new UIPropertyMetadata(200d));

        public double UnlockInterval
        {
            get { return (double)GetValue(UnlockIntervalProperty); }
            set { SetValue(UnlockIntervalProperty, value); }
        }

        public static readonly DependencyProperty UnlockIntervalProperty =
            DependencyProperty.Register("UnlockInterval", typeof(double), typeof(MagnetButton), new UIPropertyMetadata(80d));

        //p207 Heart of the MagnetButton
        private T FindAncestor<T>(DependencyObject dependencyObject) where T : class
        {
            DependencyObject target = dependencyObject;
            do
            {
                target = VisualTreeHelper.GetParent(target);
            }
            while (target != null && !(target is T));
            return target as T;
        }

        protected override void OnKinectCursorEnter(object sender, KinectCursorEventArgs e)
        {
            var rootVisual = FindAncestor<Window>(this);
            var point = this.TransformToAncestor(rootVisual).Transform(new Point(0, 0));

            var x = point.X + this.ActualWidth / 2;
            var y = point.Y + this.ActualHeight / 2;

            var cursor = e.Cursor;
            cursor.UpdateCursor(new Point(e.X, e.Y), true);

            //find target position
            Point lockPoint = new Point(x - cursor.CursorVisual.ActualWidth / 2, y - cursor.CursorVisual.ActualHeight / 2);

            //find current location
            Point cursorPoint = new Point(e.X - cursor.CursorVisual.ActualWidth / 2, e.Y - cursor.CursorVisual.ActualHeight / 2);

            //guide cursor to its final position
            AnimateToLockPosition(e, x, y, cursor, ref lockPoint, ref cursorPoint);
            base.OnKinectCursorEnter(sender, e);
        }

        protected override void OnKinectCursorLeave(object sender, KinectCursorEventArgs e)
        {
            base.OnKinectCursorLeave(sender, e);

            e.Cursor.UpdateCursor(new Point(e.X, e.Y), false);

            //get button position
            var rootVisual = FindAncestor<Window>(this);
            var point = this.TransformToAncestor(rootVisual).Transform(new Point(0, 0));

            var x = point.X + this.ActualWidth / 2;
            var y = point.Y + this.ActualHeight / 2;

            var cursor = e.Cursor;
            

            //find target position
            Point lockPoint = new Point(x - cursor.CursorVisual.ActualWidth / 2, y - cursor.CursorVisual.ActualHeight / 2);

            //find current location
            Point cursorPoint = new Point(e.X - cursor.CursorVisual.ActualWidth / 2, e.Y - cursor.CursorVisual.ActualHeight / 2);

            //guide cursor to its final position
            AnimateCursorAwayFromLockPosition(e, cursor, ref lockPoint, ref cursorPoint);
        }

        //P209 Animating the Cursor on lock and Unlock

        private Storyboard _move;

        private void AnimateToLockPosition(KinectCursorEventArgs e, double x, double y, CursorAdorner cursor, ref Point lockPoint,
                                            ref Point cursorPoint)
        {
            DoubleAnimation moveLeft = new DoubleAnimation(cursorPoint.X, lockPoint.X, new Duration(TimeSpan.FromMilliseconds(LockInterval)));
            Storyboard.SetTarget(moveLeft, cursor.CursorVisual);
            Storyboard.SetTargetProperty(moveLeft, new PropertyPath(Canvas.LeftProperty));

            DoubleAnimation moveTop = new DoubleAnimation(cursorPoint.Y, lockPoint.Y, new Duration(TimeSpan.FromMilliseconds(LockInterval)));
            Storyboard.SetTarget(moveTop, cursor.CursorVisual);
            Storyboard.SetTargetProperty(moveTop, new PropertyPath(Canvas.TopProperty));

            _move = new Storyboard();
            _move.Children.Add(moveTop);
            _move.Children.Add(moveLeft);

            _move.Completed += delegate
            {
                this.RaiseEvent(new KinectCursorEventArgs(KinectCursorLockEvent, new Point(x, y), e.Z)
                {
                    Cursor = e.Cursor
                });
            };
            if (_move != null)
            {
                _move.Stop(e.Cursor);
            }
            _move.Begin(cursor, false);
        }

        private void AnimateCursorAwayFromLockPosition(KinectCursorEventArgs e, CursorAdorner cursor, ref Point lockPoint, ref Point cursorPoint)
        {
            DoubleAnimation moveLeft = new DoubleAnimation(lockPoint.X, cursorPoint.X, new Duration(TimeSpan.FromMilliseconds(UnlockInterval)));
            Storyboard.SetTarget(moveLeft, cursor.CursorVisual);
            Storyboard.SetTargetProperty(moveLeft, new PropertyPath(Canvas.LeftProperty));

            DoubleAnimation moveTop = new DoubleAnimation(lockPoint.Y, cursorPoint.Y, new Duration(TimeSpan.FromMilliseconds(UnlockInterval)));
            Storyboard.SetTarget(moveTop, cursor.CursorVisual);
            Storyboard.SetTargetProperty(moveTop, new PropertyPath(Canvas.TopProperty));

            _move = new Storyboard();
            _move.Children.Add(moveTop);
            _move.Children.Add(moveLeft);

            _move.Completed += delegate
            {
                _move.Stop(cursor);
                cursor.UpdateCursor(new Point(e.X, e.Y), false);
                this.RaiseEvent(new KinectCursorEventArgs(KinectCursorUnlockEvent, new Point(e.X, e.Y), e.Z)
                {
                    Cursor = e.Cursor
                });
            };
            if (_move != null)
            {
                _move.Stop(e.Cursor);
            }
            _move.Begin(cursor, true);
        }
    }
}
