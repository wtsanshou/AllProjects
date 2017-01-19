using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace KinectGestureLibrary
{
    public delegate void KinectCursorEventHandler(object sender, KinectCursorEventArgs e);

    public static class KinectInput
    {

        //KinectCursorEnter
        public static readonly RoutedEvent KinectCursorEnterEvent =
            EventManager.RegisterRoutedEvent("KinectCursorEnter", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));

        public static void AddKinectCursorEnterHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorEnterEvent, handler);
        }

        public static void RemoveKinectCursorEnterHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorEnterEvent, handler);
        }

        //KinectCursorLeave
        public static readonly RoutedEvent KinectCursorLeaveEvent =
            EventManager.RegisterRoutedEvent("KinectCursorLeave", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));


        public static void AddKinectCursorLeaveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorLeaveEvent, handler);
        }

        public static void RemoveKinectCursorLeaveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorLeaveEvent, handler);
        }

        //KinectCursorMove
        public static readonly RoutedEvent KinectCursorMoveEvent =
            EventManager.RegisterRoutedEvent("KinectCursorMove", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));


        public static void AddKinectCursorMoveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorMoveEvent, handler);
        }

        public static void RemoveKinectCursorMoveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorMoveEvent, handler);
        }

        //KinectCursorActivated
        public static readonly RoutedEvent KinectCursorActivatedEvent =
            EventManager.RegisterRoutedEvent("KinectCursorActivated", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));


        public static void AddKinectCursorActivatedHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorActivatedEvent, handler);
        }

        public static void RemoveKinectCursorActivatedHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorActivatedEvent, handler);
        }

        //KinectCursorDeactivated
        public static readonly RoutedEvent KinectCursorDeactivatedEvent =
            EventManager.RegisterRoutedEvent("KinectCursorDeactivated", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));


        public static void AddKinectCursorDeactivatedHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorDeactivatedEvent, handler);
        }

        public static void RemoveKinectCursorDeactivatedHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorDeactivatedEvent, handler);
        }

        //p205 Lock and Unlock for MagnetButton
        public static readonly RoutedEvent KinectCursorLockEvent =
            EventManager.RegisterRoutedEvent("KinectCursorLockEvent", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));


        public static void AddKinectCursorLockHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorLockEvent, handler);
        }

        public static readonly RoutedEvent KinectCursorUnlockEvent =
            EventManager.RegisterRoutedEvent("KinectCursorUnlockdEvent", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));


        public static void AddKinectCursorUnlockHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorUnlockEvent, handler);
        }
    }
}

