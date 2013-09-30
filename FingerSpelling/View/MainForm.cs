using System.ComponentModel;
using System.Drawing;
using System.IO;
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
using OpenNI;
using System.Windows.Threading;

namespace FingerSpelling.View
{
    /// <summary> 
    /// Main class. Represents the GUI and GUI Eventhandling.</summary>
    public partial class MainForm : Form
    {
        private IList<IDataSource> activeDataSources;
        private IDataSourceFactory dataSourceFactory;

        private GestureDetector gestureDetector;
        private ScriptNode scriptNode;

        private Boolean initializationDone = false;

        // This delegate enables asynchronous calls for setting 
        // the text property on a TextBox control. 
        delegate void SetTextCallback(string text, Control control);

        private ClusterDataSourceSettings clusteringSettings = new ClusterDataSourceSettings();
        private ShapeDataSourceSettings shapeSettings = new ShapeDataSourceSettings();
        private HandDataSourceSettings handDetectionSettings = new HandDataSourceSettings();
        private SpeechSynthesizerHandler speechSynthesizer = new SpeechSynthesizerHandler();
        private bool textToSpeachEnabled = false;

        private Context context;                            // The OpenNI context used for most OpenNI-related operations
        private DepthGenerator depth;                       // This will generate the depth image for you

        private readonly String xmlPath = "Resources/OpenNI-config.xml";

        private Graphics g;
        private HandData handData;
        private BackgroundWorker dbWorker;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// This method does all the initialisation. It is called from the GUI.</summary> 
        private void AppForm_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            Cursor.Current = Cursors.WaitCursor;
            this.initializationDone = InitializeOpenNi();
            this.activeDataSources = new List<IDataSource>();
            Cursor.Current = Cursors.Default;
            this.videoControl.ClearLayers();

            g = this.shapeBox.CreateGraphics();

            DataPersister.InitializeDb();

            dbWorker = new BackgroundWorker();
            dbWorker.WorkerReportsProgress = false;
            dbWorker.WorkerSupportsCancellation = true;
            dbWorker.DoWork += DbWorkerDoWork;
            dbWorker.RunWorkerCompleted += DbWorkerRunWorkerCompleted;

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
            notifyIcon.ShowBalloonTip(10000);
        }

        /// <summary> 
        /// Its the result handler of dbWorker. Shows an error if gesture couldn't be saved.</summary> 
        void DbWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((e.Error != null) || (e.Cancelled == true))
            {
                MessageBox.Show("Could not store data into database", "Database Connection Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            else
            {
                String result = (string)e.Result;

                gestureDetector = GestureDetector.GetGestureDetector;
                gestureDetector.InitializeBackgroundWorker();

                if (String.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Could not store data into database", "Database Connection Error", MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                }
            }
        }

        /// <summary> 
        /// In that method the dbWorker will write the captured gesture to db.</summary> 
        void DbWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            if ((worker.CancellationPending == true))
            {
                e.Cancel = true;
            }
            else
            {
                Gesture recordedGesture = (Gesture)e.Argument;

                String key = DataPersister.SaveToDb(recordedGesture);

                e.Result = key;
            }

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
            }
            catch (Exception)
            {
            }
        }

        /// <summary> 
        /// Test method. Writes the depth image object to console.</summary>
        private void depth_NewDataAvailable(object sender, EventArgs e)
        {
            var depth = sender as DepthGenerator;
            Console.WriteLine("depth generator " + depth);
        }

        /// <summary> 
        /// The app  closing eventhandler is removing all eventhandlers and cleaning the depth image view.</summary>
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

        /// <summary> 
        /// The xml-configuration of the OpenNI 3d sensor is read and sensor will be initialised.</summary>
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

        /// <summary> 
        /// clean the application, invalidate the videostream display and stop the voice.</summary>
        private void Clear()
        {
            speechSynthesizer.StopVoice();
            foreach (var dataSource in this.activeDataSources)
            {
                dataSource.Stop();
            }

            this.videoControl.Invalidate();
            this.videoControl.ClearLayers();
            this.activeDataSources.Clear();
            this.videoControl.Clear();
        }

        /// <summary> 
        /// starts the hand and finger detection.</summary>
        private void startButton_Click(object sender, EventArgs e)
        {
            this.startButton.Enabled = false;

            if (this.initializationDone)
            {
                StartHandTracking();
            }
        }

        /// <summary> 
        /// Cleans the eventhandler before closing the app.</summary>
        private void removeEventHandlers()
        {
            this.gestureDetector.handFoundEventHandler -= gestureDetector_handFoundEventHandler;
            this.gestureDetector.toCloseEventHandler -= gestureDetector_toCloseEventHandler;
            this.gestureDetector.gestureFoundEventHandler -= gestureDetector_gestureFoundEventHandler;

            if (dbWorker != null && dbWorker.WorkerSupportsCancellation == true)
            {
                dbWorker.CancelAsync();
            }
        }

        /// <summary> 
        /// Sets the datasource to hand detection.</summary>
        private void SetHandDataSource(IHandDataSource dataSource, bool overrideSource)
        {
            SetDataSource(dataSource, new HandLayer(dataSource), overrideSource);
        }

        /// <summary> 
        /// switch the datasources.</summary>
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

        /// <summary> 
        /// Sets datasource to image or depth.</summary>
        private void SetImageDataSource(IBitmapDataSource dataSource)
        {
            this.Clear();
            this.activeDataSources.Add(dataSource);
            this.videoControl.SetImageSource(dataSource);
            dataSource.Start();
        }

        /// <summary> 
        /// Shows a website after clicking the label.</summary>
        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.samuelstein.de");
        }

        /// <summary> 
        /// exit the app.</summary>
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

        /// <summary> 
        /// display help message.</summary>
        protected override void OnHelpButtonClicked(CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnHelpButtonClicked(e);
            MessageBox.Show("Help goes here.");
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
        private void AddText(String text, Control control)
        {
            if (control.InvokeRequired)
            {
                var d = new SetTextCallback(AddText);
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

        /// <summary> 
        /// initializes the gesture detector for hand detection.</summary>
        private void StartHandTracking()
        {
            if (this.initializationDone)
            {
                this.radioButtonDepth.Checked = true;
                var handDataSource = new HandDataSource(this.dataSourceFactory.CreateShapeDataSource(this.clusteringSettings, this.shapeSettings), this.handDetectionSettings);
                this.SetHandDataSource(handDataSource, false);

                gestureDetector.StartDetecting(handDataSource, this.radioButtonLefty.Checked, this.radioButtonRighty.Checked, this.speechCheckBox.Checked, (int)this.detectionRateField.Value);
                gestureDetector.toCloseEventHandler += gestureDetector_toCloseEventHandler;
                gestureDetector.gestureFoundEventHandler += gestureDetector_gestureFoundEventHandler;
                gestureDetector.handFoundEventHandler += new HandFoundEventHandler(gestureDetector_handFoundEventHandler);
                gestureDetector.noHandFoundEventHandler += gestureDetector_noHandFoundEventHandler;
            }
        }

        /// <summary> 
        /// removes all entries from detection view if no hand data is available.</summary>
        private void gestureDetector_noHandFoundEventHandler(object sender, NoHandFoundEvent e)
        {
            this.AddText("-", this.volumeTextField);
            this.AddText("-", this.palmPointBox);
            this.AddText("-", this.centerPointBox);
            this.AddText("-", this.fingerPointTextField);
            this.AddText("-", this.pointsTextBox);
            this.AddText("-", this.detectedValueField);
        }

        /// <summary> 
        /// shows the name of the detected gesture.</summary>
        void gestureDetector_gestureFoundEventHandler(object sender, GestureFoundEvent e)
        {
            this.AddText(e.gesture.gestureName, this.detectedValueField);
            Speak(e.gesture.gestureName);
        }

        /// <summary> 
        /// handles the event if the users hand is to close to sensor.</summary>
        void gestureDetector_toCloseEventHandler(object sender, ToCloseEvent e)
        {
            //MessageBox.Show("Your hand is to close for detection", "To Close Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary> 
        /// Eventhandler which is called after a hand is detected. He shows hand data.</summary>
        void gestureDetector_handFoundEventHandler(object sender, HandFoundEvent e)
        {
            this.handData = e.handData;
            //toolStripMemoryStatusLabel.Text = "Total Memory: " + GC.GetTotalMemory(false);
            //toolStripMemoryStatusLabel.Text="Memory Usage: "+System.Diagnostics.Process.GetCurrentProcess().WorkingSet64/1000;
            //toolStripStatusLabelFPS.Text = "FPS: "+1/fpsWatch.ElapsedMilliseconds;

            if (handData != null)
            {
                this.AddText("W: " + handData.Volume.Width + " H: " + handData.Volume.Height + " D: " +
                             handData.Volume.Depth, this.volumeTextField);

                this.AddText(handData.Contour.Points.Count.ToString(), this.pointsTextBox);
                this.AddText(handData.PalmPoint.ToString(), this.palmPointBox);
                this.AddText(handData.Location.ToString(), this.centerPointBox);

                if (handData.HasFingers)
                {
                    this.AddText(handData.FingerPoints.Count.ToString(), this.fingerPointTextField);
                }
                //g.DrawPolygon(new Pen(Color.White, 2), MathHelper.convertToDrawablePointList(handData).ToArray());
            }
        }

        /// <summary> 
        /// the recognized gesture will be spoken.</summary>
        private void Speak(String word)
        {
            if (this.textToSpeachEnabled)
            {
                speechSynthesizer.StopVoice();
                speechSynthesizer.TextToSpeech(word, -4, 100);
            }
            else
            {
                speechSynthesizer.StopVoice();
            }
        }

        /// <summary> 
        /// This handler switches between camera and depth image.</summary>
        private void imageMode_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;

            if (radioButtonRGB.Checked && this.initializationDone)
            {
                this.gestureDetector.Clear();
                this.SetImageDataSource(this.dataSourceFactory.CreateRGBBitmapDataSource());
                this.startButton.Enabled = true;
            }
            else if (radioButtonDepth.Checked && this.initializationDone)
            {
                this.SetImageDataSource(this.dataSourceFactory.CreateDepthBitmapDataSource());
                this.startButton.Enabled = true;
            }
        }

        /// <summary> 
        /// Opens the settings view for the hand detection algorithm.</summary>
        private void settingsButton_Click(object sender, EventArgs e)
        {
            new SettingsForm(this.clusteringSettings, this.shapeSettings, this.handDetectionSettings).Show();
        }

        /// <summary> 
        /// Key down handler.</summary>
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

        /// <summary> 
        /// Opens the acrobat reader with the fingeralphabet view.</summary>
        private void fingeralphabetButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"Resources\Assets\fingeralphabet-GERv02-A4_G01.pdf");
        }

        /// <summary> 
        /// In that method the dbWorker will be called to write the gesture to db.</summary>
        private void recordButton_Click(object sender, EventArgs e)
        {
            gestureDetector = GestureDetector.GetGestureDetector;

            if (!String.IsNullOrWhiteSpace(gestureName.Text))
            {
                if (dbWorker.IsBusy != true)
                {
                    dbWorker.RunWorkerAsync(new Gesture(gestureName.Text.ToLower(), this.handData));
                }
            }
            else
            {
                MessageBox.Show("Please insert a name.", "Gesture Store Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
            }

        }

        /// <summary> 
        /// In that method the dbWorker will read the gesture from db.</summary>
        private void readButton_Click(object sender, EventArgs e)
        {
            if (gestureName.Text != "")
            {
                Gesture returnedGesture = DataPersister.ReadFromDb("gestures/" + gestureName.Text);
                MessageBox.Show("GESTURE " + returnedGesture.gestureName + ", " + returnedGesture.center + ", " + returnedGesture.fingerCount + ", " + returnedGesture.contourPoints);
            }
            else
            {
                MessageBox.Show("Please insert a name.", "Gesture Read Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
            }

        }

        /// <summary> 
        /// In that method the gesture will be exported to file system.</summary>
        private void exportGestureButton_Click(object sender, EventArgs e)
        {
            gestureDetector = GestureDetector.GetGestureDetector;

            if (!String.IsNullOrWhiteSpace(gestureName.Text))
            {
                bool success = DataPersister.SaveToFile("XML", gestureName.Text.ToLower(), FileMode.Create, FileAccess.Write, new Gesture(gestureName.Text.ToLower(), handData));

                if (!success)
                {
                    MessageBox.Show("Could not export Gesture", "Gesture Export Error", MessageBoxButtons.OK,
                                   MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Please insert a name.", "Gesture Store Error", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
            }
        }

        /// <summary> 
        /// Removes the notification icon if clicked.</summary>
        private void notifyIcon_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
        }

        /// <summary> 
        /// Changes rate of gesture detection.</summary>
        private void detectionRateField_ValueChanged(object sender, EventArgs e)
        {
            var detectionRate = sender as NumericUpDown;

            gestureDetector = GestureDetector.GetGestureDetector;
            gestureDetector.SetDetectionRate((int)detectionRate.Value);
        }

        /// <summary> 
        /// Enables or disables speech.</summary>
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
