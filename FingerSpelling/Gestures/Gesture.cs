using System;
using CCT.NUI.Core;
using CCT.NUI.Core.Shape;
using CCT.NUI.HandTracking;

namespace FingerSpelling.Gestures
{
    [Serializable]
    public class Gesture
    {

        public String gestureName;
        public HandData hand;
        //private String hash;

        public Gesture()
        {
            this.gestureName = "empty";
            this.hand=null;
        }


        public Gesture(String gestureName, HandData handData)
        {
            this.gestureName = gestureName;
            this.hand = handData;
        }

    }
}
