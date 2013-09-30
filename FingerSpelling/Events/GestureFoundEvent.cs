using System;
using FingerSpelling.Gestures;

namespace FingerSpelling.Events
{
    /// <summary> 
    /// Event is fired when gesture is found.</summary>
    public class GestureFoundEvent:EventArgs
    {
        public Gesture gesture;

        public GestureFoundEvent(Gesture gesture)
        {
            this.gesture = gesture;
        }
    }
}
