using System;
using System.Collections.Generic;
using System.Linq;
using CCT.NUI.Core;
using CCT.NUI.HandTracking;
using FingerSpelling.Gestures;

namespace FingerSpelling.tools
{
    /// <summary> 
    /// Static helper class for mathematical functions.</summary>
    public static class MathHelper
    {
        /// <summary> 
        /// Converts a point from candescent NUI to c# point for drawing</summary>
        public static System.Drawing.Point ConvertToDrawablePoint(Point originalPoint)
        {
            return new System.Drawing.Point((int)originalPoint.X, (int)originalPoint.Y);
        }

        /// <summary> 
        /// Converts a list of points from candescent NUI to c# drawable points.</summary>
        public static IList<System.Drawing.Point> ConvertToDrawablePointList(Gesture gesture)
        {
            List<System.Drawing.Point> convertedList = new List<System.Drawing.Point>();

            foreach (var point in gesture.contourPoints)
            {
                convertedList.Add(ConvertToDrawablePoint(point));
            }

            return convertedList;
        }

        /// <summary> 
        /// Calculates the hausdorff distance between two sets to find the smallest distance.</summary>
        public static double CalculateHausdorffDistance(List<Point> set1, List<Point> set2)
        {
            double hausdorffDistance = 100.0;

            IList<Double> euclideanDistances = new List<Double>();
            IList<Double> hausdorffDistancesTemp = new List<Double>();

            

            foreach (Point point1 in set1)
            {
                foreach (Point point2 in set2)
                {
                    euclideanDistances.Add(CalculateEuclideanDistance(point1, point2));
                }
                hausdorffDistancesTemp.Add(euclideanDistances.Min());
                euclideanDistances.Clear();
            }

            //h(set1, set2)
            hausdorffDistance = hausdorffDistancesTemp.Max();

            return hausdorffDistance;
        }

        /// <summary> 
        /// Calculates the euclidean distance between two points. Its faster then the following method.</summary>
        public static double CalculateEuclideanDistance(Point p, Point q)
        {
            double a = p.X - q.X;
            double b = p.Y - q.Y;
            return Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// Return the distance between 2 points
        /// </summary>
        public static double Euclidean(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        /// <summary>
        /// Calculates the similarity between 2 points using Euclidean distance.
        /// Returns a value between 0 and 1 where 1 means they are identical
        /// </summary>
        public static double EuclideanSimilarity(Point p1, Point p2)
        {
            return 1 / (1 + Euclidean(p1, p2));
        }

        /// <summary> 
        /// Translates a list of points with center point to origin.</summary>
        public static List<Point> TranslateToOrigin(List<Point> set, Point center)
        {
            Vector translationVector = new Vector(Point.Zero.X - center.X, Point.Zero.Y - center.Y, Point.Zero.Z - center.Z);

            List<Point> translatedPoints=new List<Point>();

            for (int i = 0; i < set.Count; i++)
            {
                translatedPoints.Add(new Point((float)Math.Round(set[i].X + translationVector.X, 0), (float)Math.Round(set[i].Y + translationVector.Y), (float)Math.Round(set[i].Z + translationVector.Z)));
            }
            return set;
        }

    }
}
