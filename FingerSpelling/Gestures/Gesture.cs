using System;
using System.Collections.Generic;
using CCT.NUI.Core;
using CCT.NUI.Core.Shape;
using CCT.NUI.HandTracking;

namespace FingerSpelling.Gestures
{
    [Serializable]
    public class Gesture
    {
        public String gestureName;
        public IList<Point> contourPoints;
        public ConvexHull convexHull;
        public int fingerCount;
        public Point? palmPoint;
        public Point center;
        public float volumeDepth;
        public float volumeWidth;
        public float volumeHeight;
        public double zoomfactor;

        public Gesture()
        {
            this.gestureName = "empty";
        }


        public Gesture(String gestureName, HandData handData)
        {
            this.gestureName = gestureName;
            this.contourPoints = handData.Contour.Points;
            this.center = handData.Location;
            this.convexHull = handData.ConvexHull;
            this.fingerCount = handData.FingerCount;
            this.palmPoint = handData.PalmPoint;
            this.volumeDepth = handData.Volume.Depth;
            this.volumeHeight = handData.Volume.Height;
            this.volumeWidth = handData.Volume.Width;
            this.zoomfactor = 0;
        }

    }
}
