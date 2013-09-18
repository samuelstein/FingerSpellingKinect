using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCT.NUI.Core.Shape;
using CCT.NUI.HandTracking;
using FingerSpelling.Gestures;

namespace FingerSpelling.Events
{
    public class GestureFoundEvent:EventArgs
    {
        //private HandData handData { get; set; }
        private Gesture gesture { get; set; }
        //private long timestamp { get; set; }
        //private Shape shape { get; set; }

        //public GestureFoundEvent(HandData handData, long timestamp)
        //{
        //    this.handData = handData;
        //    this.timestamp = timestamp;
        //    //this.shape = shape;
        //}

        public GestureFoundEvent(Gesture gesture)
        {
            this.gesture = gesture;
        }
    }
}
