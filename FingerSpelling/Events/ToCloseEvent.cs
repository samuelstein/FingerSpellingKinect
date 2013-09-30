using System;

namespace FingerSpelling.Events
{
    /// <summary> 
    /// Event is fired if userhand is to close to sensor.</summary>
    public class ToCloseEvent : EventArgs
    {
        public ToCloseEvent()
        {
        }
    }
}
