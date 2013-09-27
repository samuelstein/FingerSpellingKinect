using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using CCT.NUI.Core;
using CCT.NUI.HandTracking;
using FingerSpelling.Events;
using FingerSpelling.tools;
using Raven.Client.Embedded;

namespace FingerSpelling.Gestures
{
    public delegate void GestureFoundEventHandler(object sender, GestureFoundEvent e);
    public delegate void HandFoundEventHandler(object sender, HandFoundEvent e);
    public delegate void CalibrationEventHandler(object sender, CalibrationEvent e);
    public delegate void NoHandFoundEventHandler(object sender, NoHandFoundEvent e);
    public delegate void ToCloseEventHandler(object sender, ToCloseEvent e);

    //Singleton
    public sealed class GestureDetector
    {
        private bool lefty;
        private bool righty;
        private bool speech;
        private HandDataSource handDataSource;
        private HandData actualHand;
        private HandData referenceHand { get; set; }
        private int detectionRate = 1;

        public event GestureFoundEventHandler gestureFoundEventHandler;
        public event HandFoundEventHandler handFoundEventHandler;
        public event NoHandFoundEventHandler noHandFoundEventHandler;
        public event CalibrationEventHandler calibrationEventHandler;
        public event ToCloseEventHandler toCloseEventHandler;

        private static readonly GestureDetector gestureDetector = new GestureDetector();
        private EmbeddableDocumentStore database = RavenDBEmbedded.getRavenDBInstance.getDBInstance();

        private System.Timers.Timer detectionTimer = new System.Timers.Timer();

        private List<Gesture> allGestures;
        private BackgroundWorker backgroundWorker;

        static GestureDetector()
        {

        }

        public static GestureDetector GetGestureDetector
        {
            get
            {
                return gestureDetector;
            }
        }

        public void StartDetecting(HandDataSource handDataSource, bool lefty, bool righty, bool speech, int detectionRate)
        {
            this.lefty = lefty;
            this.righty = righty;
            this.speech = speech;
            this.handDataSource = handDataSource;
            this.detectionRate = detectionRate;

            this.handDataSource.NewDataAvailable += new NewDataHandler<HandCollection>(handDataSource_NewDataAvailable);
            handDataSource.Start();

            InitializeBackgroundWorker();
        }

        private void InitializeBackgroundWorker()
        {

            backgroundWorker = new BackgroundWorker();
            // Start the asynchronous operation.
            backgroundWorker.RunWorkerAsync();

            backgroundWorker.DoWork +=
                new DoWorkEventHandler(backgroundWorker_DoWork);
            backgroundWorker.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker_RunWorkerCompleted);
            //backgroundWorker.ProgressChanged +=
            //    new ProgressChangedEventHandler(
            //backgroundWorker_ProgressChanged);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                Console.WriteLine("SUCCESSFULLY READ ALL GESTURES FROM DB.");
                detectionTimer.Interval = detectionRate; //ms
                detectionTimer.Elapsed += new ElapsedEventHandler(RunEvent);
                detectionTimer.Enabled = true;
            }

        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            allGestures = DataPersister.fetchAll();
        }

        private void RunEvent(object sender, ElapsedEventArgs e)
        {

            int threshold = 180;//200; //percent
            Gesture identified = new Gesture();

            Console.WriteLine("detection in progress");

            if (actualHand != null)
            {
                identified = new Gesture("-", actualHand);
                //List<Gesture> resultList = DataPersister.searchMatchingGesture(new Gesture("", actualHand));
                List<Gesture> resultList = (from gesture in allGestures where gesture.fingerCount == actualHand.FingerCount select gesture).ToList();

                Dictionary<Gesture, double> hausdorffDistancesGestures = new Dictionary<Gesture, double>();

                foreach (var gesture in resultList)
                {
                    Console.WriteLine("possible gesture from db: " + gesture.gestureName);
                    hausdorffDistancesGestures.Add(gesture, MathHelper.calculateHausdorffDistance((List<Point>)actualHand.Contour.Points,
                                                                    (List<Point>)gesture.contourPoints));
                }

                if (hausdorffDistancesGestures.Count > 0)
                {
                    // Order by values.
                    // ... Use LINQ to specify sorting by value.
                    var mostMatchingGesture = from pair in hausdorffDistancesGestures
                                              orderby pair.Value descending
                                              select pair;

                    Console.WriteLine("SMALLEST HAUSDORFF DISTANCE " + mostMatchingGesture.First().Value);

                    if (threshold >= mostMatchingGesture.First().Value)
                    {
                        identified = mostMatchingGesture.First().Key;
                    }
                    if (gestureFoundEventHandler != null)
                    {
                        gestureFoundEventHandler(this, new GestureFoundEvent(identified));
                    }
                }
            }
        }

        public void SetDetectionRate(int rate)
        {
            this.detectionRate = rate;
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

                if (actualHand != null)
                {
                    //30
                    if (actualHand.Volume.Depth >= 80)
                    {
                        if (toCloseEventHandler != null)
                        {
                            toCloseEventHandler(this, new ToCloseEvent());
                        }
                    }

                    if (actualHand.HasFingers)
                    {
                        //CALIBRATION
                        if (actualHand.FingerCount == 5)
                        {
                            if (calibrationEventHandler != null)
                            {
                                calibrationEventHandler(this, new CalibrationEvent());

                                //Thread.Sleep(500);
                                this.referenceHand = actualHand;
                            }
                        }
                    }
                }
            }
            else
            {
                if (noHandFoundEventHandler != null)
                {
                    noHandFoundEventHandler(this, new NoHandFoundEvent());
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

        private long GetTimestamp()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public bool RecordGesture(String gestureName)
        {
            Gesture gesture = new Gesture(gestureName, actualHand);

            return DataPersister.saveToFile("XML", gestureName, FileMode.Create, FileAccess.Write, gesture);
        }


        public void Clear()
        {
            this.detectionTimer.Stop();
            this.handDataSource.NewDataAvailable -= handDataSource_NewDataAvailable;
        }
    }
}
