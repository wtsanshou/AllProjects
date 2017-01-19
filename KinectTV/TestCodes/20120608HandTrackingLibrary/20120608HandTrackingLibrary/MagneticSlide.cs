using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace _20120608HandTrackingLibrary
{
    public class MagneticSlide : MagnetButton
    {
        private bool _isLookingForSwipes;

        public MagneticSlide()
        {
            base._timerEnabled = false;
        }

        private void InitializeSwipe()
        {
            if (_isLookingForSwipes)
            {
                return;
            }
            _isLookingForSwipes = true;
            var kinectMgr = KinectCursorManager.Instance;
            kinectMgr.GesturePointTrackingInitialize(SwipeLenght, MaxDeviation, MaxSwipeTime, XOutOfBoundsLenght);
            kinectMgr.swipeDetected += new KinectCursorEventHandler(kinectMgr_SwipeOutOfBoundsDetected);
            kinectMgr.GesturePointTrackingStart();
        }

        private void DeInitializeSwipe()
        {
            var kinectMgr = KinectCursorManager.Instance;
            kinectMgr.swipeDetected -= kinectMgr_SwipeDetected;
            kinectMgr.SwipeOutOfBoundsDetected -= kinectMgr_SwipeOutOfBoundsDetected;
            kinectMgr.GesturePointTrackingStop();
            _isLookingForSwipes = false;
        }

        public static readonly DependencyProperty SwipeLenghtProperty =
            DependencyProperty.Register("SwipeLenght", typeof(double), typeof(MagneticSlide), new UIPropertyMetadata(-500d));

        public double SwipeLenght
        {
            get { return (double)GetValue(SwipeLenghtProperty); }
            set { SetValue(SwipeLenghtProperty, value); }
        }

        public static readonly DependencyProperty MaxDeviationProperty =
            DependencyProperty.Register("MaxDeviation", typeof(double), typeof(MagneticSlide), new UIPropertyMetadata(100d));

        public double MaxDeviation
        {
            get { return (double)GetValue(MaxDeviationProperty); }
            set { SetValue(MaxDeviationProperty, value); }
        }

        public static readonly DependencyProperty MaxSwipeTimeProperty =
            DependencyProperty.Register("MaxSwipeTime", typeof(int), typeof(MagneticSlide), new UIPropertyMetadata(300));

        public int MaxSwipeTime
        {
            get { return (int)GetValue(MaxSwipeTimeProperty); }
            set { SetValue(MaxSwipeTimeProperty, value); }
        }

        

        public static readonly DependencyProperty XOutOfBoundsLenghtProperty =
            DependencyProperty.Register("XOutOfBoundsLenght", typeof(double), typeof(MagneticSlide), new UIPropertyMetadata(-700d));

        public double XOutOfBoundsLenght
        {
            get { return (double)GetValue(XOutOfBoundsLenghtProperty); }
            set { SetValue(XOutOfBoundsLenghtProperty, value); }
        }


        //P217 Magnetic Slide EventManagement
        public static readonly RoutedEvent SwipeOutOfBoundsEvent
            = EventManager.RegisterRoutedEvent("SwipeOutOfBounds", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));
        public event RoutedEventHandler SwipeOutOfBounds
        {
            add { AddHandler(SwipeOutOfBoundsEvent, value); }
            remove { RemoveHandler(SwipeOutOfBoundsEvent, value); }
        }

        public void kinectMgr_SwipeOutOfBoundsDetected(object sender, KinectCursorEventArgs e)
        {
            DeInitializeSwipe();
            RaiseEvent(new KinectCursorEventArgs(SwipeOutOfBoundsEvent));
        }

        public void kinectMgr_SwipeDetected(object sender, KinectCursorEventArgs e)
        {
            DeInitializeSwipe();
            RaiseEvent(new RoutedEventArgs(ClickEvent));
        }

        protected override void OnKinectCursorEnter(object sender, KinectCursorEventArgs e)
        {
            InitializeSwipe();
            base.OnKinectCursorEnter(sender, e);
        }
    }
}
