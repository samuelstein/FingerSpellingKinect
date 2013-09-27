using System;
using FingerSpelling.Gestures;

namespace FingerSpelling.Events
{
    public class GestureFoundEvent:EventArgs
    {
        public Gesture gesture;

        public GestureFoundEvent(Gesture gesture)
        {
            this.gesture = gesture;
        }
    }
}
