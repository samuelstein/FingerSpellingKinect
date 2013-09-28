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

        //thread safty??
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

        public void InitializeBackgroundWorker()
        {

            if (detectionTimer != null)
            {
                detectionTimer.Stop();
            }

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = false;
            backgroundWorker.WorkerSupportsCancellation = true;
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
            if ((e.Error != null) || (e.Cancelled == true))
            {
                MessageBox.Show("Could not read data from database", "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            BackgroundWorker worker = sender as BackgroundWorker;

            if ((worker.CancellationPending == true))
            {
                e.Cancel = true;
                //break;
            }
            else
            {
                allGestures = DataPersister.FetchAll();
            }
        }

        private void ResetRestartTimer(System.Timers.Timer timer, int interval)
        {
            timer.Stop();
            timer.Interval = interval;
            timer.Start();
        }

        private void RunEvent(object sender, ElapsedEventArgs e)
        {
            HandData recordedHand = this.actualHand;

            int threshold = 40;//180;//200; //percent
            Gesture identified = new Gesture();

            Console.WriteLine("detection in progress. interval: " + detectionRate);

            if (recordedHand != null && allGestures != null)
            {
                //identified = new Gesture("-", recordedHand);
                List<Gesture> resultList = (from gesture in allGestures where gesture.fingerCount == recordedHand.FingerCount select gesture).ToList();

                Dictionary<Gesture, double> hausdorffDistancesGestures = new Dictionary<Gesture, double>();

                List<Point> recordedTranslatedHandPoints = MathHelper.TranslateToOrigin((List<Point>)recordedHand.Contour.Points,
                                                                    recordedHand.Location);

                foreach (var gesture in resultList)
                {
                    //translate to origin + calculate hausdorff distance
                    List<Point> translatedPoints = MathHelper.TranslateToOrigin((List<Point>)gesture.contourPoints, gesture.center);

                    double hausdorffDistance = MathHelper.CalculateHausdorffDistance(recordedTranslatedHandPoints,
                                                                                     translatedPoints);

                    hausdorffDistancesGestures.Add(gesture, hausdorffDistance);

                    Console.WriteLine("possible gestures: " + gesture.gestureName + ", h: " + hausdorffDistance);
                }

                if (hausdorffDistancesGestures.Count > 0)
                {
                    // Order by values. Use LINQ to specify sorting by value.
                    var mostMatchingGestures = from pair in hausdorffDistancesGestures
                                               orderby pair.Value ascending
                                               select pair;

                    foreach (KeyValuePair<Gesture, double> keyValuePair in mostMatchingGestures)
                    {
                        Console.WriteLine(keyValuePair.Key + " - " + keyValuePair.Value);
                    }

                    Console.WriteLine("SMALLEST HAUSDORFF DISTANCE: " + mostMatchingGestures.First().Value);

                    if (threshold >= mostMatchingGestures.First().Value)
                    {
                        identified = mostMatchingGestures.First().Key;

                        if (gestureFoundEventHandler != null)
                        {
                            Console.WriteLine("\nGESTURE DETECTED: " + identified.gestureName + "\n");
                            gestureFoundEventHandler(this, new GestureFoundEvent(identified));
                        }
                    }
                }
            }

            ResetRestartTimer(detectionTimer, detectionRate);
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

        }

        private long GetTimestamp()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public bool RecordGesture(String gestureName)
        {
            Gesture gesture = new Gesture(gestureName, actualHand);

            return DataPersister.SaveToFile("XML", gestureName, FileMode.Create, FileAccess.Write, gesture);
        }


        public void Clear()
        {
            this.detectionTimer.Stop();
            if (handDataSource != null)
            {
                this.handDataSource.NewDataAvailable -= handDataSource_NewDataAvailable;
            }

            if (backgroundWorker != null && backgroundWorker.WorkerSupportsCancellation == true)
            {
                backgroundWorker.CancelAsync();
            }
        }
    }
}
