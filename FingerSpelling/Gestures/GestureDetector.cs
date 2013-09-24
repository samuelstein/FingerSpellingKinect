using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using CCT.NUI.Core;
using CCT.NUI.Core.Shape;
using CCT.NUI.HandTracking;
using FingerSpelling.Events;
using FingerSpelling.tools;

namespace FingerSpelling.Gestures
{
    //public delegate void NewDataHandler<HandCollection>(object sender, EventArgs e);
    public delegate void GestureFoundEventHandler(object sender, GestureFoundEvent e);
    public delegate void HandFoundEventHandler(object sender, HandFoundEvent e);
    public delegate void CalibrationEventHandler(object sender, CalibrationEvent e);

    public delegate void ToCloseEventHandler(object sender, ToCloseEvent e);

    //Singleton

    public sealed class GestureDetector
    {
        private bool lefty;
        private bool righty;
        private bool speech;
        private bool text;
        private HandDataSource handDataSource;
        private HandData actualHand;
        private HandData referenceHand { get; set; }

        public event GestureFoundEventHandler gestureFoundEventHandler;
        public event HandFoundEventHandler handFoundEventHandler;
        public event CalibrationEventHandler calibrationEventHandler;
        public event ToCloseEventHandler toCloseEventHandler;

        private static readonly GestureDetector gestureDetector = new GestureDetector();

        //public event CCT.NUI.Core.NewDataHandler<HandCollection> handDataEventHandler;

        static GestureDetector()
        {

        }

        public static GestureDetector getGestureDetector
        {
            get
            {
                return gestureDetector;
            }
        }

        public void startDetecting(HandDataSource handDataSource, bool lefty, bool righty, bool speech, bool text)
        {
            this.lefty = lefty;
            this.righty = righty;
            this.speech = speech;
            this.text = text;
            this.handDataSource = handDataSource;

            this.handDataSource.NewDataAvailable += new NewDataHandler<HandCollection>(handDataSource_NewDataAvailable);
            handDataSource.Start();
        }


        private void handDataSource_NewDataAvailable(HandCollection handData)
        {

            if (!handData.IsEmpty)
            {
                //always use first (closest) hand
                actualHand = handData.Hands[0];

                if (handFoundEventHandler != null)
                {
                    handFoundEventHandler(this, new HandFoundEvent(actualHand));
                }

                //30

                if (actualHand.Volume.Depth >= 80)
                {
                    if (toCloseEventHandler != null)
                    {
                        toCloseEventHandler(this, new ToCloseEvent());
                    }
                }

                if (actualHand != null && actualHand.HasFingers)
                {
                    if (actualHand.FingerCount == 5)
                    {
                        if (calibrationEventHandler != null)
                        {
                            calibrationEventHandler(this, new CalibrationEvent());
                            this.referenceHand = actualHand;
                        }
                    }
                }
            }

            //for (var index = 0; index < handData.Count; index++)
            //{
            //    HandData hand = handData.Hands[index];
            //    //this.addText(index+ " Hand(s).\nFingers: "+hand.FingerCount);
            //    //this.addText(string.Format("Fingers on hand {0}: {1}", index, hand.FingerCount));
            //    //this.addText("Palm Center: " + hand.PalmPoint);
            //    //Console.WriteLine("Hand location "+hand.Location);
            //}

            //if (righty)
            //{
            //    if (handData.Count == 1)
            //    {
            //        HandData hand = handData.Hands.First();
            //        //GestureFoundEvent gestureFoundEvent;
            //        Gesture gesture = new Gesture();

            //        switch (hand.FingerCount)
            //        {
            //            case 5:
            //                //generateOutput("key five");
            //                //gestureFoundEvent = new GestureFoundEvent(hand, getTimestamp());
            //                if (gestureFoundEventHandler != null)
            //                {
            //                    //gesture= new Gesture("test", hand);
            //                    gestureFoundEventHandler(this, new GestureFoundEvent(gesture));

            //                }
            //                break;
            //            case 4:
            //                if (gestureFoundEventHandler != null)
            //                {
            //                    //gesture = new Gesture("test", hand);
            //                    gestureFoundEventHandler(this, new GestureFoundEvent(gesture));

            //                }

            //                //generateOutput("key four");
            //                break;
            //            default:

            //                //generateOutput("no key detected");
            //                break;
            //        }
            //    }
            //    if (handData.Count == 2)
            //    {
            //        var leftHand = handData.Hands.OrderBy(h => h.Location.X).First();
            //        var rightHand = handData.Hands.OrderBy(h => h.Location.X).Last();
            //        if (leftHand.FingerCount == 2 && rightHand.FingerCount == 2)
            //        {
            //            //SurfaceMode(handData);
            //        }
            //        else if (leftHand.FingerCount >= 4 && rightHand.FingerCount == 0)
            //        {
            //            //StopMode();
            //        }
            //        else if (leftHand.FingerCount >= 4 && rightHand.FingerCount >= 1)
            //        {
            //            //TimeShiftMode(rightHand);
            //        }
            //        else if (rightHand.FingerCount == 0 && leftHand.FingerCount == 0)
            //        {
            //            //isNew = true;
            //            //DisabeMoveMode();
            //        }
            //        else if (leftHand.FingerCount == 0)
            //        {
            //            //DisabeMoveMode();
            //        }
            //        else if (rightHand.FingerCount >= 4 && leftHand.FingerCount == 1)
            //        {
            //            //CancelMode(leftHand);
            //        }
            //    }
            //}

        }

        private long getTimestamp()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public bool recordGesture(String gestureName)
        {
            Gesture gesture = new Gesture(gestureName, actualHand);

            return DataPersister.saveToFile("XML", gestureName, FileMode.Create, FileAccess.Write, gesture);
        }

        private bool readGesture()
        {
            return false;
        }

        public Gesture getActualGesture()
        {
            if (actualHand != null)
            {
                return new Gesture("", actualHand);
            }
            else
            {
                return new Gesture();
            }
        }
    }
}
