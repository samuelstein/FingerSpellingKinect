using System;
using CCT.NUI.HandTracking;

namespace FingerSpelling.Events
{
    public class HandFoundEvent : EventArgs
    {
        public HandData handData;

        public HandFoundEvent(HandData handData)
        {
            this.handData=handData;
        }
    }
}
