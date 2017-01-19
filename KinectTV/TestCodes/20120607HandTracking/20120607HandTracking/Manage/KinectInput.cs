using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace _20120607HandTracking.Manage
{
    public delegate void KinectCursorEventHandler(object sender, KinectCurosrEventArgs e);

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


        public static void AddKinectCursorLeaveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorMoveEvent, handler);
        }

        public static void RemoveKinectCursorLeaveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorMoveEvent, handler);
        }

        //KinectCursorActivated
        public static readonly RoutedEvent KinectCursorActivatedEvent =
            EventManager.RegisterRoutedEvent("KinectCursorActivated", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));


        public static void AddKinectCursorLeaveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorActivatedEvent, handler);
        }

        public static void RemoveKinectCursorLeaveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorActivatedEvent, handler);
        }

        //KinectCursorDeactivated
        public static readonly RoutedEvent KinectCursorDeactivatedEvent =
            EventManager.RegisterRoutedEvent("KinectCursorDeactivated", RoutingStrategy.Bubble, typeof(KinectCursorEventHandler), typeof(KinectInput));


        public static void AddKinectCursorLeaveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).AddHandler(KinectCursorDeactivatedEvent, handler);
        }

        public static void RemoveKinectCursorLeaveHandler(DependencyObject o, KinectCursorEventHandler handler)
        {
            ((UIElement)o).RemoveHandler(KinectCursorDeactivatedEvent, handler);
        }
    }
}
