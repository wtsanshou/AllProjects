using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace KinectGestureLibrary
{
    public class CursorAdorner : Adorner
    {
        private readonly UIElement _adorningElement;
        private VisualCollection _visualChildren;
        private Canvas _CursorCanvas;
        protected FrameworkElement _cursor;
        Storyboard _gradientStopAnimationStoryboard;
        private bool _isOverridden;

        readonly static Color _backColor = Colors.White;
        readonly static Color _foreColor = Colors.Gray;

        public CursorAdorner(FrameworkElement adorningElement)
            : base(adorningElement)
        {
            this._adorningElement = adorningElement;
            CreateCursorAdorner();
            this.IsHitTestVisible = false;
        }

        public void CreateCursorAdorner()
        {
            var innerCursor = CreateCursor();
            CreateCursorAdorner(innerCursor);
        }

        public void CreateCursorAdorner(FrameworkElement innerCursor)
        {
            _visualChildren = new VisualCollection(this);
            _CursorCanvas = new Canvas();
            _cursor = innerCursor;
            _CursorCanvas.Children.Add(_cursor);
            _visualChildren.Add(this._CursorCanvas);
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(_adorningElement);
            layer.Add(this);
        }

        public CursorAdorner(FrameworkElement adorningElement, FrameworkElement innerCursor)
            : base(adorningElement)
        {
            this._adorningElement = adorningElement;
            CreateCursorAdorner(innerCursor);
            this.IsHitTestVisible = false;
        }

        public FrameworkElement CursorVisual
        {
            get
            {
                return _cursor;
            }
        }

        protected FrameworkElement CreateCursor()
        {
            var brush = new LinearGradientBrush();
            brush.EndPoint = new Point(0, 1);
            brush.StartPoint = new Point(0, 0);
            brush.GradientStops.Add(new GradientStop(_backColor, 1));
            brush.GradientStops.Add(new GradientStop(_foreColor, 1));
            var Cursor = new Ellipse()
            {
                Width = 50,
                Height = 50,
                Fill = brush
            };
            return Cursor;
        }

        //Adorner Base Class Method Overrides
        protected override int VisualChildrenCount
        {
            get
            {
                return _visualChildren.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visualChildren[index];
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this._CursorCanvas.Measure(constraint);
            return this._CursorCanvas.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this._CursorCanvas.Arrange(new Rect(finalSize));
            return finalSize;
        }

        //Passing Coordinate Positions to the CursorAdorner
        public void UpdateCursor(Point position, bool isOverride)
        {
            _isOverridden = isOverride;
            _cursor.SetValue(Canvas.LeftProperty, position.X - (_cursor.ActualWidth / 2));
            _cursor.SetValue(Canvas.TopProperty, position.Y - (_cursor.ActualHeight / 2));
        }

        public void UpdateCursor(Point position)
        {
            if (_isOverridden)
                return;

            _cursor.SetValue(Canvas.LeftProperty, position.X - (_cursor.ActualWidth / 2));
            _cursor.SetValue(Canvas.TopProperty, position.Y - (_cursor.ActualHeight / 2));
        }

        //Cursor Animations
        public virtual void AnimateCursor(double milliSeconds)
        {
            CreateGradientStopAnimation(milliSeconds);
            if (_gradientStopAnimationStoryboard != null)
            {
                _gradientStopAnimationStoryboard.Begin(this, true);
            }
        }

        public virtual void StopCursorAnimation()
        {
            if (_gradientStopAnimationStoryboard != null)
            {
                _gradientStopAnimationStoryboard.Stop(this);
            }
        }

        public virtual void CreateGradientStopAnimation(double milliSeconds)
        {
            NameScope.SetNameScope(this, new NameScope());
            var cursor = _cursor as Shape;
            if (cursor == null)
            {
                return;
            }
            var brush = cursor.Fill as LinearGradientBrush;
            var stop1 = brush.GradientStops[0];
            var stop2 = brush.GradientStops[1];
            this.RegisterName("GradientStop1", stop1);
            this.RegisterName("GradientStop2", stop2);

            DoubleAnimation offsetAnimation = new DoubleAnimation();
            offsetAnimation.From = 1.0;
            offsetAnimation.To = 0.0;
            offsetAnimation.Duration = TimeSpan.FromMilliseconds(milliSeconds);

            Storyboard.SetTargetName(offsetAnimation, "GradientStop1");
            Storyboard.SetTargetProperty(offsetAnimation, new PropertyPath(GradientStop.OffsetProperty));

            DoubleAnimation offsetAnimation2 = new DoubleAnimation();
            offsetAnimation2.From = 1.0;
            offsetAnimation2.To = 0.0;
            offsetAnimation2.Duration = TimeSpan.FromMilliseconds(milliSeconds);

            Storyboard.SetTargetName(offsetAnimation2, "GradientStop2");
            Storyboard.SetTargetProperty(offsetAnimation2, new PropertyPath(GradientStop.OffsetProperty));

            _gradientStopAnimationStoryboard = new Storyboard();
            _gradientStopAnimationStoryboard.Children.Add(offsetAnimation);
            _gradientStopAnimationStoryboard.Children.Add(offsetAnimation2);
            _gradientStopAnimationStoryboard.Completed += delegate { _gradientStopAnimationStoryboard.Stop(this); };
        }

        
    }
}

