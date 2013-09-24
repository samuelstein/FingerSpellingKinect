using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using CCT.NUI.HandTracking;
using FingerSpelling.Gestures;

namespace FingerSpelling.tools
{
    public static class MathHelper
    {
        public static Point convertToDrawablePoint(CCT.NUI.Core.Point originalPoint)
        {
            return new Point((int)originalPoint.X, (int)originalPoint.Y);
        }

        public static IList<Point> convertToDrawablePointList(Gesture gesture)
        {
            List<Point> convertedList=new List<Point>();

            foreach (var point in gesture.contourPoints)
            {
                convertedList.Add(convertToDrawablePoint(point));
            }

            return convertedList;
        }

        public static IList<Point> convertToDrawablePointList(HandData handData)
        {
            List<Point> convertedList = new List<Point>();

            foreach (var point in handData.Contour.Points)
            {
                convertedList.Add(convertToDrawablePoint(point));
            }

            return convertedList;
        }
    }
}
