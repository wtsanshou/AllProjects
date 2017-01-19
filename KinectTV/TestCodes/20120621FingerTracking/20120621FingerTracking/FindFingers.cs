using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace _20120621FingerTracking
{
    class FindFingers
    {
        private int _middleFingerWidth;
        private List<Point> _handPoints;

        private List<Point> _leftToRight;
        private List<Point> _rightToLeft;
        private List<Point> _topToBottom;
        private List<Point> _bottomToTop;

        private struct Outline
        {
            public Point hand;
            public int flag;
        }

        private List<Outline> _handOutline;

        public FindFingers(int middleFingerWidth, List<Point> handOutLine)
        {
            this._handOutline = new List<Outline>();
            this._middleFingerWidth = middleFingerWidth;
            this._handPoints = handOutLine;

            Outline outlin = new Outline();
            outlin.hand = handOutLine[0];
            outlin.flag = 1;
            this._handOutline.Add(outlin);
            for (int i = 1; i < handOutLine.Count; i++)
            {
                outlin.hand = handOutLine[i];
                outlin.flag = 0;
                this._handOutline.Add(outlin);
            }
           
            //QuickSort qs = new QuickSort(handOutLine);
           // this._leftToRight = qs.getSort(1);
            //this._rightToLeft = qs.getSort(2);
            //this._topToBottom = qs.getSort(3);
            //this._bottomToTop = qs.getSort(4);
            
        }

        private bool isIn(double x, double y)
        {
            int i;
            int j;
            
            for (j = (int)y - 10; j < y + 10; j++)
            {
                for (i = (int)x - 10; i < x + 10; i++)
                {
                    if (i < 0)
                    {
                        i = 0;
                    }
                    if (j < 0)
                    {
                        j = 0;
                    }
                    if (i > 99)
                    {
                        continue;
                    }
                    if (y > 99)
                    {
                        continue;
                    }

                    if (this._handPoints[j * 100 + i].X == x && this._handPoints[j * 100 + i].Y == y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private Point Top(Point oldPoint, int distance)
        {
            if (isIn(oldPoint.X, oldPoint.Y - distance))
            {
                Point p = new Point(oldPoint.X, oldPoint.Y - distance);
                return p;
            }
            return oldPoint;
        }

        private Point RightTop(Point oldPoint, int distance)
        {
            if (isIn(oldPoint.X + distance, oldPoint.Y - distance))
            {
                Point p = new Point(oldPoint.X + distance, oldPoint.Y - distance);
                return p;
            }
            return oldPoint;
        }
        private Point Right(Point oldPoint, int distance)
        {
            if (isIn(oldPoint.X + distance, oldPoint.Y))
            {
                Point p = new Point(oldPoint.X + distance, oldPoint.Y);
                return p;
            }
            return oldPoint;
        }
        private Point RightBottom(Point oldPoint, int distance)
        {
            if (isIn(oldPoint.X + distance, oldPoint.Y + distance))
            {
                Point p = new Point(oldPoint.X + distance, oldPoint.Y + distance);
                return p;
            }
            return oldPoint;
        }
        private Point Bottom(Point oldPoint, int distance)
        {
            if (isIn(oldPoint.X, oldPoint.Y + distance))
            {
                Point p = new Point(oldPoint.X, oldPoint.Y + distance);
                return p;
            }
            return oldPoint;
        }
        private Point LeftBottom(Point oldPoint, int distance)
        {
            if (isIn(oldPoint.X - distance, oldPoint.Y + distance))
            {
                Point p = new Point(oldPoint.X - distance, oldPoint.Y + distance);
                return p;
            }
            return oldPoint;
        }
        private Point Left(Point oldPoint, int distance)
        {
            if (isIn(oldPoint.X - distance, oldPoint.Y))
            {
                Point p = new Point(oldPoint.X - distance, oldPoint.Y);
                return p;
            }
            return oldPoint;
        }
        private Point LeftTop(Point oldPoint, int distance)
        {
            if (isIn(oldPoint.X - distance, oldPoint.Y - distance))
            {
                Point p = new Point(oldPoint.X - distance, oldPoint.Y - distance);
                return p;
            }
            return oldPoint;
        }


        private Point fromTop(Point oldPoint, int distance)
        {
            if (Top(oldPoint, distance) != oldPoint)
            {
                return Top(oldPoint, distance);
            }
            else if (RightTop(oldPoint, distance) != oldPoint)
            {
                return RightTop(oldPoint, distance);
            }
            else if (Right(oldPoint, distance) != oldPoint)
            {
                return Right(oldPoint, distance);
            }
            else if (RightBottom(oldPoint, distance) != oldPoint)
            {
                return RightBottom(oldPoint, distance);
            }
            else if (Bottom(oldPoint, distance) != oldPoint)
            {
                return Bottom(oldPoint, distance);
            }
            else if (LeftBottom(oldPoint, distance) != oldPoint)
            {
                return LeftBottom(oldPoint, distance);
            }
            else if (Left(oldPoint, distance) != oldPoint)
            {
                return Left(oldPoint, distance);
            }
            return oldPoint;
        }
        private Point fromRightTop(Point oldPoint, int distance)
        {
            if (RightTop(oldPoint, distance) != oldPoint)
            {
                return RightTop(oldPoint, distance);
            }
            else if (Right(oldPoint, distance) != oldPoint)
            {
                return Right(oldPoint, distance);
            }
            else if (RightBottom(oldPoint, distance) != oldPoint)
            {
                return RightBottom(oldPoint, distance);
            }
            else if (Bottom(oldPoint, distance) != oldPoint)
            {
                return Bottom(oldPoint, distance);
            }
            else if (LeftBottom(oldPoint, distance) != oldPoint)
            {
                return LeftBottom(oldPoint, distance);
            }
            else if (Left(oldPoint, distance) != oldPoint)
            {
                return Left(oldPoint, distance);
            }
            else if (LeftTop(oldPoint, distance) != oldPoint)
            {
                return LeftTop(oldPoint, distance);
            }
            return oldPoint;
        }
        private Point fromRight(Point oldPoint, int distance)
        {
            if (Right(oldPoint, distance) != oldPoint)
            {
                return Right(oldPoint, distance);
            }
            else if (RightBottom(oldPoint, distance) != oldPoint)
            {
                return RightBottom(oldPoint, distance);
            }
            else if (Bottom(oldPoint, distance) != oldPoint)
            {
                return Bottom(oldPoint, distance);
            }
            else if (LeftBottom(oldPoint, distance) != oldPoint)
            {
                return LeftBottom(oldPoint, distance);
            }
            else if (Left(oldPoint, distance) != oldPoint)
            {
                return Left(oldPoint, distance);
            }
            else if (LeftTop(oldPoint, distance) != oldPoint)
            {
                return LeftTop(oldPoint, distance);
            }
            else if (Top(oldPoint, distance) != oldPoint)
            {
                return Top(oldPoint, distance);
            }
            return oldPoint;
        }
        private Point fromRightBottom(Point oldPoint, int distance)
        {
            if (RightBottom(oldPoint, distance) != oldPoint)
            {
                return RightBottom(oldPoint, distance);
            }
            else if (Bottom(oldPoint, distance) != oldPoint)
            {
                return Bottom(oldPoint, distance);
            }
            else if (LeftBottom(oldPoint, distance) != oldPoint)
            {
                return LeftBottom(oldPoint, distance);
            }
            else if (Left(oldPoint, distance) != oldPoint)
            {
                return Left(oldPoint, distance);
            }
            else if (LeftTop(oldPoint, distance) != oldPoint)
            {
                return LeftTop(oldPoint, distance);
            }
            else if (Top(oldPoint, distance) != oldPoint)
            {
                return Top(oldPoint, distance);
            }
            else if (RightTop(oldPoint, distance) != oldPoint)
            {
                return RightTop(oldPoint, distance);
            }
            return oldPoint;
        }
        private Point fromBottom(Point oldPoint, int distance)
        {
            if (Bottom(oldPoint, distance) != oldPoint)
            {
                return Bottom(oldPoint, distance);
            }
            else if (LeftBottom(oldPoint, distance) != oldPoint)
            {
                return LeftBottom(oldPoint, distance);
            }
            else if (Left(oldPoint, distance) != oldPoint)
            {
                return Left(oldPoint, distance);
            }
            else if (LeftTop(oldPoint, distance) != oldPoint)
            {
                return LeftTop(oldPoint, distance);
            }
            else if (Top(oldPoint, distance) != oldPoint)
            {
                return Top(oldPoint, distance);
            }
            else if (RightTop(oldPoint, distance) != oldPoint)
            {
                return RightTop(oldPoint, distance);
            }
            else if (Right(oldPoint, distance) != oldPoint)
            {
                return Right(oldPoint, distance);
            }
            return oldPoint;
        }
        private Point fromLeftBottom(Point oldPoint, int distance)
        {
            if (LeftBottom(oldPoint, distance) != oldPoint)
            {
                return LeftBottom(oldPoint, distance);
            }
            else if (Left(oldPoint, distance) != oldPoint)
            {
                return Left(oldPoint, distance);
            }
            else if (LeftTop(oldPoint, distance) != oldPoint)
            {
                return LeftTop(oldPoint, distance);
            }
            else if (Top(oldPoint, distance) != oldPoint)
            {
                return Top(oldPoint, distance);
            }
            else if (RightTop(oldPoint, distance) != oldPoint)
            {
                return RightTop(oldPoint, distance);
            }
            else if (Right(oldPoint, distance) != oldPoint)
            {
                return Right(oldPoint, distance);
            }
            else if (RightBottom(oldPoint, distance) != oldPoint)
            {
                return RightBottom(oldPoint, distance);
            }
            return oldPoint;
        }
        private Point fromLeft(Point oldPoint, int distance)
        {
            if (Left(oldPoint, distance) != oldPoint)
            {
                return Left(oldPoint, distance);
            }
            else if (LeftTop(oldPoint, distance) != oldPoint)
            {
                return LeftTop(oldPoint, distance);
            }
            else if (Top(oldPoint, distance) != oldPoint)
            {
                return Top(oldPoint, distance);
            }
            else if (RightTop(oldPoint, distance) != oldPoint)
            {
                return RightTop(oldPoint, distance);
            }
            else if (Right(oldPoint, distance) != oldPoint)
            {
                return Right(oldPoint, distance);
            }
            else if (RightBottom(oldPoint, distance) != oldPoint)
            {
                return RightBottom(oldPoint, distance);
            }
            else if (Bottom(oldPoint, distance) != oldPoint)
            {
                return Bottom(oldPoint, distance);
            }
            return oldPoint;
        }
        private Point fromLeftTop(Point oldPoint, int distance)
        {
            if (LeftTop(oldPoint, distance) != oldPoint)
            {
                return LeftTop(oldPoint, distance);
            }
            else if (Top(oldPoint, distance) != oldPoint)
            {
                return Top(oldPoint, distance);
            }
            else if (RightTop(oldPoint, distance) != oldPoint)
            {
                return RightTop(oldPoint, distance);
            }
            else if (Right(oldPoint, distance) != oldPoint)
            {
                return Right(oldPoint, distance);
            }
            else if (RightBottom(oldPoint, distance) != oldPoint)
            {
                return RightBottom(oldPoint, distance);
            }
            else if (Bottom(oldPoint, distance) != oldPoint)
            {
                return Bottom(oldPoint, distance);
            }
            else if (LeftBottom(oldPoint, distance) != oldPoint)
            {
                return LeftBottom(oldPoint, distance);
            }
            return oldPoint;
        }

        public List<Point> SortHandOutine()
        {
            List<Point> sortHand = new List<Point>();
            sortHand.Add(this._handOutline[0].hand);
            Point newPoint = this._handOutline[0].hand;

            Point oldPoint = new Point(0, 0);
            Point goalPoint = new Point(0, 0);
            sortHand.Add(newPoint);
            if (this._handPoints.Count > 0)
            {
                while (goalPoint != this._handOutline[0].hand)
                {
                    //Top to Bottom
                    if (newPoint.X == oldPoint.X && newPoint.Y > oldPoint.Y)
                    {
                        goalPoint = fromRightTop(newPoint, 1);
                        if (goalPoint != newPoint)
                        {
                            oldPoint = newPoint;
                            newPoint = goalPoint;
                            sortHand.Add(newPoint);
                        }
                    }
                    //LeftTop to RightBottom
                    else if (newPoint.X > oldPoint.X && newPoint.Y > oldPoint.Y)
                    {
                        goalPoint = fromTop(newPoint, 1);
                        if (goalPoint != newPoint)
                        {
                            oldPoint = newPoint;
                            newPoint = goalPoint;
                            sortHand.Add(newPoint);
                        }
                    }
                    //Left to Right
                    else if (newPoint.X > oldPoint.X && newPoint.Y == oldPoint.Y)
                    {
                        goalPoint = fromLeftTop(newPoint, 1);
                        if (goalPoint != newPoint)
                        {
                            oldPoint = newPoint;
                            newPoint = goalPoint;
                            sortHand.Add(newPoint);
                        }
                    }
                    //LeftBottom to RightTop
                    else if (newPoint.X > oldPoint.X && newPoint.Y < oldPoint.Y)
                    {
                        goalPoint = fromLeft(newPoint, 1);
                        if (goalPoint != newPoint)
                        {
                            oldPoint = newPoint;
                            newPoint = goalPoint;
                            sortHand.Add(newPoint);
                        }
                    }
                    //Bottom to Top
                    else if (newPoint.X == oldPoint.X && newPoint.Y > oldPoint.Y)
                    {
                        goalPoint = fromLeftBottom(newPoint, 1);
                        if (goalPoint != newPoint)
                        {
                            oldPoint = newPoint;
                            newPoint = goalPoint;
                            sortHand.Add(newPoint);
                        }
                    }
                    //RightBottom to LeftTop
                    else if (newPoint.X < oldPoint.X && newPoint.Y < oldPoint.Y)
                    {
                        goalPoint = fromBottom(newPoint, 1);
                        if (goalPoint != newPoint)
                        {
                            oldPoint = newPoint;
                            newPoint = goalPoint;
                            sortHand.Add(newPoint);
                        }
                    }
                    //Right to Left
                    if (newPoint.X < oldPoint.X && newPoint.Y == oldPoint.Y)
                    {
                        goalPoint = fromRightBottom(newPoint, 1);
                        if (goalPoint != newPoint)
                        {
                            oldPoint = newPoint;
                            newPoint = goalPoint;
                            sortHand.Add(newPoint);
                        }
                    }
                    //RightTop to LeftBottom
                    else if (newPoint.X < oldPoint.X && newPoint.Y > oldPoint.Y)
                    {
                        goalPoint = fromRight(newPoint, 1);
                        if (goalPoint != newPoint)
                        {
                            oldPoint = newPoint;
                            newPoint = goalPoint;
                            sortHand.Add(newPoint);
                        }
                    }
                }
                
            }
            return sortHand;
        }   
            /*
                    
                }
                for (int i = 1; i < this._handPoints.Count; i++)
                {
                    //The Right point
                    int right = isIn(newPoint.X + 1, newPoint.Y);
                    if (right != -1)
                    {
                        //Eliminate old point
                        if (newPoint.X + 1 == oldPoint.X && newPoint.Y == oldPoint.Y)
                        { }
                        else
                        {
                            //Set the new OldPoint
                            oldPoint = newPoint;

                            //add this new point into sort hand outline
                            newPoint = new Point(newPoint.X + 1, newPoint.Y);
                            sortHand.Add(newPoint);

                            //mark the used point whose flag equals 1
                            Outline a = new Outline();
                            a.hand = this._handOutline[right].hand;
                            a.flag = 1;
                            this._handOutline[right] = a;

                            //go on the For loop
                            continue;

                        }
                    }

                    //The Right Bottom point
                    int rightBottom = isIn(newPoint.X + 1, newPoint.Y + 1);
                    if (rightBottom != -1)
                    {
                        //Eliminate old point
                        if (newPoint.X + 1 == oldPoint.X && newPoint.Y + 1 == oldPoint.Y)
                        { }
                        else
                        {
                            //Set the new OldPoint
                            oldPoint = newPoint;

                            //add this new point into sort hand outline
                            newPoint = new Point(newPoint.X + 1, newPoint.Y + 1);
                            sortHand.Add(newPoint);

                            //mark the used point whose flag equals 1
                            Outline a = new Outline();
                            a.hand = this._handOutline[rightBottom].hand;
                            a.flag = 1;
                            this._handOutline[rightBottom] = a;

                            //go on the For loop
                            continue;
                        }
                    }
                    //The Bottom point
                    int bottom = isIn(newPoint.X, newPoint.Y + 1);
                    if (bottom != -1)
                    {
                        //Eliminate old point
                        if (newPoint.X == oldPoint.X && newPoint.Y + 1 == oldPoint.Y)
                        { }
                        else
                        {
                            //Set the new OldPoint
                            oldPoint = newPoint;

                            //add this new point into sort hand outline
                            newPoint = new Point(newPoint.X, newPoint.Y + 1);
                            sortHand.Add(newPoint);

                            //mark the used point whose flag equals 1
                            Outline a = new Outline();
                            a.hand = this._handOutline[bottom].hand;
                            a.flag = 1;
                            this._handOutline[bottom] = a;

                            //go on the For loop
                            continue;
                        }
                    }

                    //The Left Bottom point
                    int leftBottom = isIn(newPoint.X - 1, newPoint.Y + 1);
                    if (leftBottom != -1)
                    {
                        //Eliminate old point
                        if (newPoint.X - 1 == oldPoint.X && newPoint.Y + 1 == oldPoint.Y)
                        { }
                        else
                        {
                            //Set the new OldPoint
                            oldPoint = newPoint;

                            //add this new point into sort hand outline
                            newPoint = new Point(newPoint.X - 1, newPoint.Y + 1);
                            sortHand.Add(newPoint);

                            //mark the used point whose flag equals 1
                            Outline a = new Outline();
                            a.hand = this._handOutline[leftBottom].hand;
                            a.flag = 1;
                            this._handOutline[leftBottom] = a;

                            //go on the For loop
                            continue;
                        }
                    }

                    //The Left point
                    int left = isIn(newPoint.X - 1, newPoint.Y);
                    if (left != -1)
                    {
                        //Eliminate old point
                        if (newPoint.X - 1 == oldPoint.X && newPoint.Y == oldPoint.Y)
                        { }
                        else
                        {
                            //Set the new OldPoint
                            oldPoint = newPoint;

                            //add this new point into sort hand outline
                            newPoint = new Point(newPoint.X - 1, newPoint.Y);
                            sortHand.Add(newPoint);

                            //mark the used point whose flag equals 1
                            Outline a = new Outline();
                            a.hand = this._handOutline[left].hand;
                            a.flag = 1;
                            this._handOutline[left] = a;

                            //go on the For loop
                            continue;
                        }
                    }

                    //The Left Top point
                    int leftTop = isIn(newPoint.X - 1, newPoint.Y - 1);
                    if (leftTop != -1)
                    {
                        //Eliminate old point
                        if (newPoint.X - 1 == oldPoint.X && newPoint.Y == oldPoint.Y - 1)
                        { }
                        else
                        {
                            //Set the new OldPoint
                            oldPoint = newPoint;

                            //add this new point into sort hand outline
                            newPoint = new Point(newPoint.X - 1, newPoint.Y - 1);
                            sortHand.Add(newPoint);

                            //mark the used point whose flag equals 1
                            Outline a = new Outline();
                            a.hand = this._handOutline[leftTop].hand;
                            a.flag = 1;
                            this._handOutline[leftTop] = a;

                            //go on the For loop
                            continue;
                        }
                    }

                    //The Top point
                    int top = isIn(newPoint.X, newPoint.Y - 1);
                    if (top != -1)
                    {
                        //Eliminate old point
                        if (newPoint.X == oldPoint.X && newPoint.Y == oldPoint.Y - 1)
                        { }
                        else
                        {
                            //Set the new OldPoint
                            oldPoint = newPoint;

                            //add this new point into sort hand outline
                            newPoint = new Point(newPoint.X, newPoint.Y - 1);
                            sortHand.Add(newPoint);

                            //mark the used point whose flag equals 1
                            Outline a = new Outline();
                            a.hand = this._handOutline[top].hand;
                            a.flag = 1;
                            this._handOutline[top] = a;

                            //go on the For loop
                            continue;
                        }
                    }

                    //The Right Top point
                    int rightTop = isIn(newPoint.X + 1, newPoint.Y - 1);
                    if (rightTop != -1)
                    {
                        //Eliminate old point
                        if (newPoint.X == oldPoint.X + 1 && newPoint.Y == oldPoint.Y - 1)
                        { }
                        else
                        {
                            //Set the new OldPoint
                            oldPoint = newPoint;

                            //add this new point into sort hand outline
                            newPoint = new Point(newPoint.X + 1, newPoint.Y - 1);
                            sortHand.Add(newPoint);

                            //mark the used point whose flag equals 1
                            Outline a = new Outline();
                            a.hand = this._handOutline[rightTop].hand;
                            a.flag = 1;
                            this._handOutline[rightTop] = a;

                            //go on the For loop
                            continue;
                        }
                    }

                    
                     
                }
            }
                     
            return sortHand;
                     * /
                //
                /*
                //Right to Left
                if (newPoint.X < oldPoint.X && newPoint.Y == oldPoint.Y)
                {

                }
                //RightTop to LeftBottom
                else if (newPoint.X < oldPoint.X && newPoint.Y > oldPoint.Y)
                { }
                //Top to Bottom
                else if (newPoint.X == oldPoint.X && newPoint.Y > oldPoint.Y)
                { }
                //LeftTop to RightBottom
                else if (newPoint.X > oldPoint.X && newPoint.Y > oldPoint.Y)
                { }
                //Left to Right
                else if (newPoint.X > oldPoint.X && newPoint.Y == oldPoint.Y)
                { }
                //LeftBottom to RightTop
                else if (newPoint.X > oldPoint.X && newPoint.Y < oldPoint.Y)
                { }
                //Bottom to Top
                else if (newPoint.X == oldPoint.X && newPoint.Y > oldPoint.Y)
                { }
                //RightBottom to LeftTop
                else if (newPoint.X < oldPoint.X && newPoint.Y < oldPoint.Y)
                { }
                 * */
           
       


    }
}
