using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
