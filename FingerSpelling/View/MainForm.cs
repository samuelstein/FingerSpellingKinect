using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;
using CCT.NUI.Core;
using CCT.NUI.Core.Clustering;
using CCT.NUI.Core.OpenNI;
using CCT.NUI.Core.Shape;
using CCT.NUI.Core.Video;
using CCT.NUI.HandTracking;
using CCT.NUI.Visual;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using FingerSpelling.Events;
using FingerSpelling.Gestures;
using FingerSpelling.Speech;
using FingerSpelling.tools;
using MetroFramework.Forms;
using OpenNI;
using System.Windows.Threading;
using Raven.Client.Embedded;

namespace FingerSpelling.View
{
    public partial class AppForm : Form
    {
        private IList<IDataSource> activeDataSources;
        private IDataSourceFactory dataSourceFactory;
        //private IImageDataSource rgbImageDataSource;
        //private IHandDataSource handDataSource;

        private GestureDetector gestureDetector;
        private ScriptNode scriptNode;

        private Boolean initializationDone = false;

        // This BackgroundWorker is used to demonstrate the  
        // preferred way of performing asynchronous operations. 
        //private BackgroundWorker setTextWorker;

        // This delegate enables asynchronous calls for setting 
        // the text property on a TextBox control. 
        delegate void SetTextCallback(string text, Control control);

        private ClusterDataSourceSettings clusteringSettings = new ClusterDataSourceSettings();
        private ShapeDataSourceSettings shapeSettings = new ShapeDataSourceSettings();
        private HandDataSourceSettings handDetectionSettings = new HandDataSourceSettings();
        private SpeechSynthesizerHandler speechSynthesizer = new SpeechSynthesizerHandler();
        private bool textToSpeachEnabled = false;
        private bool isCalibrated;

        private Context context;                            // The OpenNI context used for most OpenNI-related operations
        private DepthGenerator depth;                       // This will generate the depth image for you

        private readonly String xmlPath = "Resources/OpenNI-config.xml";

        private Graphics g;
        private HandData handData;

        private CalibrationForm calibrationForm;
        private Stopwatch fpsWatch;

        public AppForm()
        {
            InitializeComponent();
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            Cursor.Current = Cursors.WaitCursor;
            this.initializationDone = InitializeOpenNi();
            this.activeDataSources = new List<IDataSource>();
            Cursor.Current = Cursors.Default;
            this.videoControl.ClearLayers();
            this.fpsWatch = new Stopwatch();

            g = this.shapeBox.CreateGraphics();

            DataPersister.initializeDB();

            gestureDetector = GestureDetector.GetGestureDetector;

            // Initialize the context from the configuration file
            //this.context = new Context(@"config.xml");
            //this.context = Context.CreateFromXmlFile(@"..\..\..\Resources\OpenNIconfig.xml", out scriptNode);
            this.context = Context.CreateFromXmlFile(@xmlPath, out scriptNode);

            // Get the depth generator from the config file.
            this.depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            if (this.depth == null)
            {
                MessageBox.Show("Error in OpenNI-config.xml. No depth node found.", "OpenNI Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.startButton.Enabled = false;
            }

            MapOutputMode mapMode = depth.MapOutputMode;

            Console.WriteLine("FPS: " + mapMode.FPS.ToString());

            //depth.NewDataAvailable += depth_NewDataAvailable;

            // Set the timer to update the depth image every 10 ms.
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            //dispatcherTimer.Start();
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1000);
            //Console.WriteLine("Finished loading app.");

        }

        /// <summary>
        /// This method gets executed every time the timer ticks, which is every 10 ms.
        /// In it we update the depth image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.context.WaitAndUpdateAll();

                // Get information about the depth image
                DepthMetaData depthMD = new DepthMetaData();
                depth.GetMetaData(depthMD);

                //depth.Timestamp;

                //Console.WriteLine(depth.GetMetaData().FPS);
                //Console.WriteLine(depth.FrameID.ToString());
            }
            catch (Exception)
            {
            }
            //UpdateDepth();
        }

        private void depth_NewDataAvailable(object sender, EventArgs e)
        {
            var depth = sender as DepthGenerator;
            Console.WriteLine("depth generator " + depth);
            //Console.WriteLine("FPS :" + depth.GetMetaData().FPS.ToString());
        }

        private void AppForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Clear();
            this.removeEventHandlers();
            this.gestureDetector.Clear();

            if (this.dataSourceFactory != null)
            {
                this.dataSourceFactory.Dispose();
            }

        }

        private Boolean InitializeOpenNi()
        {
            try
            {
                this.dataSourceFactory = new OpenNIDataSourceFactory(xmlPath);
                (this.dataSourceFactory as OpenNIDataSourceFactory).SetAlternativeViewpointCapability();
                return true;
            }
            catch (Exception)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Could not connect to Kinect.", "Kinect Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.startButton.Enabled = false;
                return false;
            }
        }

        private void Clear()
        {
            speechSynthesizer.stopVoice();
            foreach (var dataSource in this.activeDataSources)
            {
                dataSource.Stop();
            }

            this.videoControl.Invalidate();
            this.videoControl.ClearLayers();
            this.activeDataSources.Clear();
            this.videoControl.Clear();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.startButton.Enabled = false;

            if (this.initializationDone)
            {
                startHandTracking();
            }
        }

        private void removeEventHandlers()
        {
            this.gestureDetector.handFoundEventHandler -= gestureDetector_handFoundEventHandler;
            this.gestureDetector.calibrationEventHandler -= gestureDetector_calibrationEventHandler;
            this.gestureDetector.toCloseEventHandler -= gestureDetector_toCloseEventHandler;
            this.gestureDetector.gestureFoundEventHandler -= gestureDetector_gestureFoundEventHandler;
        }

        private void SetHandDataSource(IHandDataSource dataSource, bool overrideSource)
        {
            SetDataSource(dataSource, new HandLayer(dataSource), overrideSource);
        }

        private void SetDataSource(IDataSource dataSource, ILayer layer, bool overrideSource)
        {
            if (overrideSource)
            {
                this.Clear();
            }
            this.activeDataSources.Add(dataSource);
            this.videoControl.AddLayer(layer);
            dataSource.Start();
        }

        private void SetImageDataSource(IBitmapDataSource dataSource)
        {
            this.Clear();
            this.activeDataSources.Add(dataSource);
            this.videoControl.SetImageSource(dataSource);
            dataSource.Start();
        }


        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.samuelstein.de");
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Clear();
            this.removeEventHandlers();
            this.gestureDetector.Clear();

            if (this.dataSourceFactory != null)
            {
                this.dataSourceFactory.Dispose();
            }
            this.Close();
        }

        // This method demonstrates a pattern for making thread-safe 
        // calls on a Windows Forms control.  
        // 
        // If the calling thread is different from the thread that 
        // created the TextBox control, this method creates a 
        // SetTextCallback and calls itself asynchronously using the 
        // Invoke method. 
        // 
        // If the calling thread is the same as the thread that created 
        // the TextBox control, the Text property is set directly.  
        private void addText(String text, Control control)
        {
            if (control.InvokeRequired)
            {
                var d = new SetTextCallback(addText);
                this.Invoke(d, new object[] { text, control });
            }
            else
            {
                Type t = typeof(Control);
                if (t == typeof(TextBox))
                {
                    ((TextBox)control).AppendText(text + "\n");
                }
                else
                {
                    control.Text = text;
                }
            }
        }

        private void startHandTracking()
        {
            if (this.initializationDone)
            {
                this.radioButtonDepth.Checked = true;
                var handDataSource = new HandDataSource(this.dataSourceFactory.CreateShapeDataSource(this.clusteringSettings, this.shapeSettings), this.handDetectionSettings);
                this.SetHandDataSource(handDataSource, false);

                gestureDetector.StartDetecting(handDataSource, this.radioButtonLefty.Checked, this.radioButtonRighty.Checked, this.speechCheckBox.Checked, (int)this.detectionRateField.Value);
                gestureDetector.calibrationEventHandler += gestureDetector_calibrationEventHandler;
                gestureDetector.toCloseEventHandler += gestureDetector_toCloseEventHandler;
                gestureDetector.gestureFoundEventHandler += gestureDetector_gestureFoundEventHandler;
                gestureDetector.handFoundEventHandler += new HandFoundEventHandler(gestureDetector_handFoundEventHandler);
                gestureDetector.noHandFoundEventHandler += gestureDetector_noHandFoundEventHandler;
            }

            //fpsWatch.Start();

            if (!isCalibrated)
            {
                calibrationForm = new CalibrationForm();
                calibrationForm.ShowDialog();
                //Cursor.Current = Cursors.WaitCursor;
                //this.UseWaitCursor = true;
            }
        }

        private void gestureDetector_noHandFoundEventHandler(object sender, NoHandFoundEvent e)
        {
            this.addText("", this.volumeTextField);
            this.addText("", this.palmPointBox);
            this.addText("", this.centerPointBox);
            this.addText("", this.fingerPointTextField);
            this.addText("", this.pointsTextBox);
            this.addText("-", this.detectionRateField);
        }

        void gestureDetector_gestureFoundEventHandler(object sender, GestureFoundEvent e)
        {
            this.addText(e.gesture.gestureName, this.detectedValueField);
            Speak(e.gesture.gestureName);
        }

        void gestureDetector_toCloseEventHandler(object sender, ToCloseEvent e)
        {
            //MessageBox.Show("Your hand is to close for detection", "To Close Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void gestureDetector_calibrationEventHandler(object sender, CalibrationEvent e)
        {

            if (this.InvokeRequired)
            {
                this.Invoke((Action)delegate { gestureDetector_calibrationEventHandler(sender, e); }, null);
            }
            else
            {
                this.isCalibrated = true;
                this.startButton.Enabled = false;
                calibrationForm.Close();
                //Cursor.Current = Cursors.Default;
                //this.UseWaitCursor = false;
                gestureDetector.calibrationEventHandler -= gestureDetector_calibrationEventHandler;
            }
        }

        void gestureDetector_handFoundEventHandler(object sender, HandFoundEvent e)
        {
            this.handData = e.handData;
            //toolStripMemoryStatusLabel.Text = "Total Memory: " + GC.GetTotalMemory(false);
            //toolStripMemoryStatusLabel.Text="Memory Usage: "+System.Diagnostics.Process.GetCurrentProcess().WorkingSet64/1000;
            //toolStripStatusLabelFPS.Text = "FPS: "+1/fpsWatch.ElapsedMilliseconds;

            //g.Clear(Color.Black);

            if (handData != null && isCalibrated)
            {
                this.addText("W: " + handData.Volume.Width + " H: " + handData.Volume.Height + " D: " +
                             handData.Volume.Depth, this.volumeTextField);

                this.addText(handData.Contour.Points.Count.ToString(), this.pointsTextBox);
                this.addText(handData.PalmPoint.ToString(), this.palmPointBox);
                this.addText(handData.Location.ToString(), this.centerPointBox);

                if (handData.HasFingers)
                {
                    this.addText(handData.FingerPoints.Count.ToString(), this.fingerPointTextField);
                }
                //g.DrawPolygon(new Pen(Color.White, 2), MathHelper.convertToDrawablePointList(handData).ToArray());
            }
        }

        private void Speak(String word)
        {
            if (this.textToSpeachEnabled)
            {
                speechSynthesizer.stopVoice();
                speechSynthesizer.textToSpeech(word, -4, 100);
            }
            else
            {
                speechSynthesizer.stopVoice();
            }
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;

            if (radioButtonRGB.Checked && this.initializationDone)
            {
                //this.videoControl.Invalidate();
                this.gestureDetector.Clear();
                this.SetImageDataSource(this.dataSourceFactory.CreateRGBBitmapDataSource());
                this.startButton.Enabled = true;
            }
            else if (radioButtonDepth.Checked && this.initializationDone)
            {
                //this.videoControl.Invalidate();
                this.SetImageDataSource(this.dataSourceFactory.CreateDepthBitmapDataSource());
                this.startButton.Enabled = true;
            }
        }


        private void settingsButton_Click(object sender, EventArgs e)
        {
            new SettingsForm(this.clusteringSettings, this.shapeSettings, this.handDetectionSettings).Show();
        }

        private void AppForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Clear();
                    this.gestureDetector.Clear();
                    this.removeEventHandlers();

                    if (this.dataSourceFactory != null)
                    {
                        this.dataSourceFactory.Dispose();
                    }
                    this.Close();
                    break;

                case Keys.Space:
                    gestureDetector = GestureDetector.GetGestureDetector;
                    bool success = gestureDetector.RecordGesture(gestureName.Text);

                    if (!success)
                    {
                        MessageBox.Show("Could not store Gesture", "Gesture Store Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                    break;
            }
        }

        private void fingeralphabetButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Resources\Assets\fingeralphabet-GERv02-A4_G01.pdf");
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            gestureDetector = GestureDetector.GetGestureDetector;

            if (!String.IsNullOrWhiteSpace(gestureName.Text))
            {
                bool success = gestureDetector.RecordGesture(gestureName.Text);

                String key = DataPersister.saveToDB(new Gesture(gestureName.Text, this.handData));

                if (String.IsNullOrEmpty(key))
                {
                    MessageBox.Show("Could not store Gesture", "Gesture Store Error", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

        }

        private void readButton_Click(object sender, EventArgs e)
        {
            if (gestureName.Text != "")
            {
                //Open the file written above and read values from it.
                //Console.WriteLine("Resources/Gestures/" + gestureName.Text + ".dat");
                //Stream stream = File.Open(@"Resources/Gestures/" + gestureName.Text + ".dat", FileMode.Open);
                //BinaryFormatter bformatter = new BinaryFormatter();
                //bformatter = new BinaryFormatter();

                //Console.WriteLine("Reading Gesture");
                //Gesture g = (Gesture)bformatter.Deserialize(stream);
                //MessageBox.Show(g.hand.Contour.Count.ToString());
                //stream.Close();

                Gesture returnedGesture = DataPersister.readFromDB("gestures/" + gestureName.Text);
                MessageBox.Show("GESTURE " + returnedGesture.gestureName + ", " + returnedGesture.center + ", " + returnedGesture.fingerCount + ", " + returnedGesture.volumeHeight);
            }

        }

        private void exportGestureButton_Click(object sender, EventArgs e)
        {
            gestureDetector = GestureDetector.GetGestureDetector;

            if (!String.IsNullOrWhiteSpace(gestureName.Text))
            {
                bool success = gestureDetector.RecordGesture(gestureName.Text);

                if (success)
                {
                    MessageBox.Show("Could not export Gesture", "Gesture Export Error", MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);

                }
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
        }

        private void detectionRateField_ValueChanged(object sender, EventArgs e)
        {
            var detectionRate = sender as NumericUpDown;

            gestureDetector = GestureDetector.GetGestureDetector;
            gestureDetector.SetDetectionRate((int)detectionRate.Value);
        }

        private void speechCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var speechCheckbox = sender as CheckBox;

            if (speechCheckbox != null && speechCheckbox.Checked)
            {
                this.textToSpeachEnabled = true;
            }
            else
            {
                this.textToSpeachEnabled = false;
            }
        }
    }
}
