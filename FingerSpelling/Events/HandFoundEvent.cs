using System;
using CCT.NUI.HandTracking;

namespace FingerSpelling.Events
{
    /// <summary> 
    /// Event is fired if hand was found.</summary>
    public class HandFoundEvent : EventArgs
    {
        public HandData handData;

        public HandFoundEvent(HandData handData)
        {
            this.handData=handData;
        }
    }
}
