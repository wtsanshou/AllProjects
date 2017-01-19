using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace _20120621FingerTracking
{
    class QuickSort
    {
        private List<Point> _handOutLine;

        public QuickSort(List<Point> handOutLine)
        {
            this._handOutLine = handOutLine;
        }
        
        public List<Point> getSort(int direction)
        {
            List<Point> dd = this._handOutLine;
            switch (direction)
            {
                case 1 :
                    SortLR(ref dd, 0, dd.Count - 1);
                    return dd;
                case 2 :
                    SortRL(ref dd, 0, dd.Count - 1);
                    return dd;
                case 3:
                    SortTB(ref dd, 0, dd.Count - 1);
                    return dd;
                case 4:
                    SortBT(ref dd, 0, dd.Count - 1);
                    return dd;
                default:
                    SortLR(ref dd, 0, dd.Count - 1);
                    return dd;
            }
        }
//Quick Sort from left to right
        private int PartitionLR(ref List<Point> a,int p,int r)
        {
            int x=(int)a[r].X;
	        int i=p-1;
	        Point key;
	        for(int j=p;j<r;j++)
	        {
		        if(a[j].X<=x)
		        {
			        i++;
			        key=a[i];
			        a[i]=a[j];
			        a[j]=key;
		        }
	        }
	        key=a[i+1];
	        a[i+1]=a[r];
	        a[r]=key;
	        return i+1;

        }
        private void SortLR(ref List<Point> a,int p,int r)
        {
	        int q;
            if (p > r || p == r)
            {
                return;
            } 
            q = PartitionLR(ref a, p, r);

            SortLR(ref a, p, q - 1);
            SortLR(ref a, q + 1, r);
        }

        // Quick Sort from Right to Left
        private int PartitionRL(ref List<Point> a, int p, int r)
        {
            int x = (int)a[r].X;
            int i = p - 1;
            Point key;
            for (int j = p; j < r; j++)
            {
                if (a[j].X >= x)
                {
                    i++;
                    key = a[i];
                    a[i] = a[j];
                    a[j] = key;
                }
            }
            key = a[i + 1];
            a[i + 1] = a[r];
            a[r] = key;
            return i + 1;

        }
        private void SortRL(ref List<Point> a, int p, int r)
        {
            int q;
            if (p > r || p == r)
            {
                return;
            }
            q = PartitionRL(ref a, p, r);

            SortRL(ref a, p, q - 1);
            SortRL(ref a, q + 1, r);
        }
        // Quick Sort from Top to Bottom
        private int PartitionTB(ref List<Point> a, int p, int r)
        {
            int x = (int)a[r].Y;
            int i = p - 1;
            Point key;
            for (int j = p; j < r; j++)
            {
                if (a[j].Y <= x)
                {
                    i++;
                    key = a[i];
                    a[i] = a[j];
                    a[j] = key;
                }
            }
            key = a[i + 1];
            a[i + 1] = a[r];
            a[r] = key;
            return i + 1;

        }
        private void SortTB(ref List<Point> a, int p, int r)
        {
            int q;
            if (p > r || p == r)
            {
                return;
            }
            q = PartitionTB(ref a, p, r);

            SortTB(ref a, p, q - 1);
            SortTB(ref a, q + 1, r);
        }
        // Quick Sort from left to right
        private int PartitionBT(ref List<Point> a, int p, int r)
        {
            int x = (int)a[r].Y;
            int i = p - 1;
            Point key;
            for (int j = p; j < r; j++)
            {
                if (a[j].Y >= x)
                {
                    i++;
                    key = a[i];
                    a[i] = a[j];
                    a[j] = key;
                }
            }
            key = a[i + 1];
            a[i + 1] = a[r];
            a[r] = key;
            return i + 1;

        }
        private void SortBT(ref List<Point> a, int p, int r)
        {
            int q;
            if (p > r || p == r)
            {
                return;
            }
            q = PartitionBT(ref a, p, r);

            SortBT(ref a, p, q - 1);
            SortBT(ref a, q + 1, r);
        }
    }
}
