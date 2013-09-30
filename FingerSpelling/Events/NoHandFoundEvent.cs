using System;

namespace FingerSpelling.Events
{
    /// <summary> 
    /// Event is fired if no hand is available.</summary>
    public class NoHandFoundEvent : EventArgs
    {
        public NoHandFoundEvent()
        {
        }
    }
}
