using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
using System.Linq;
using System.Windows.Forms;
using FingerSpelling.Gestures;
using FingerSpelling.Speech;
using FingerSpelling.tools;
using OpenNI;
using System.Windows.Threading;
using Point = CCT.NUI.Core.Point;

namespace FingerSpelling
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

        // This delegate enables asynchronous calls for setting 
        // the text property on a TextBox control. 
        delegate void SetTextCallback(string text);

        private ClusterDataSourceSettings clusteringSettings = new ClusterDataSourceSettings();
        private ShapeDataSourceSettings shapeSettings = new ShapeDataSourceSettings();
        private HandDataSourceSettings handDetectionSettings = new HandDataSourceSettings();
        private SpeechSynthesizerHandler speechSynthesizer = new SpeechSynthesizerHandler();
        private Boolean textToSpeachEnabled = false;

        private Context context;                            // The OpenNI context used for most OpenNI-related operations
        private DepthGenerator depth;                       // This will generate the depth image for you

        private readonly String xmlPath = "config.xml";

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

            gestureDetector = GestureDetector.getGestureDetector;

            // Initialize the context from the configuration file
            //this.context = new Context(@"config.xml");
            this.context = Context.CreateFromXmlFile(@"config.xml", out scriptNode);
            // Get the depth generator from the config file.
            this.depth = context.FindExistingNode(NodeType.Depth) as DepthGenerator;
            if (this.depth == null)
                throw new Exception(@"Error in config.xml. No depth node found.");
            MapOutputMode mapMode = depth.MapOutputMode;

            Console.WriteLine("FPS: " + mapMode.FPS.ToString());

            //depth.NewDataAvailable += depth_NewDataAvailable;

            // Set the timer to update the depth image every 10 ms.
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            //dispatcherTimer.Start();
            Console.WriteLine("Finished loading app.");

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
            //Console.WriteLine("ACTIVE SOURCES " + this.activeDataSources.Count);
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
        private void addText(String text)
        {
            //System.Threading.Thread.Sleep(500);
            if (this.textBox.InvokeRequired)
            {
                var d = new SetTextCallback(addText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox.AppendText(text + "\n");
            }
        }

        private void startHandTracking()
        {
            if (this.initializationDone)
            {
                this.radioButtonDepth.Checked = true;
                var handDataSource = new HandDataSource(this.dataSourceFactory.CreateShapeDataSource(this.clusteringSettings, this.shapeSettings), this.handDetectionSettings);
                this.SetHandDataSource(handDataSource, false);

                //gestureDetector = new GestureDetector(handDataSource, this.radioButtonLefty.Checked, this.radioButtonRighty.Checked, this.radioButtonSpeech.Checked, this.radioButtonText.Checked);


                gestureDetector.startDetecting(handDataSource, this.radioButtonLefty.Checked, this.radioButtonRighty.Checked, this.radioButtonSpeech.Checked, this.radioButtonText.Checked);

            }
        }


        private void generateOutput(String gesture)
        {
            this.addText(gesture);
            if (this.textToSpeachEnabled)
            {
                speechSynthesizer.stopVoice();
                speechSynthesizer.textToSpeech(gesture, -4, 100);
            }
            else
            {
                speechSynthesizer.stopVoice();

                this.labelKey5.BackColor = System.Drawing.Color.Black;
                this.labelKey4.BackColor = System.Drawing.Color.Black;
            }
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = sender as RadioButton;

            if (radioButtonRGB.Checked && this.initializationDone)
            {
                //this.videoControl.Invalidate();
                this.SetImageDataSource(this.dataSourceFactory.CreateRGBBitmapDataSource());
            }
            else if (radioButtonDepth.Checked && this.initializationDone)
            {
                //this.videoControl.Invalidate();
                this.SetImageDataSource(this.dataSourceFactory.CreateDepthBitmapDataSource());
            }
        }

        private void radioButtonText_Click(object sender, EventArgs e)
        {
            this.textToSpeachEnabled = false;
        }

        private void radioButtonSpeech_Click(object sender, EventArgs e)
        {
            this.textToSpeachEnabled = true;
        }

        private void calibrateButton_Click(object sender, EventArgs e)
        {

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

                    if (this.dataSourceFactory != null)
                    {
                        this.dataSourceFactory.Dispose();
                    }
                    this.Close();
                    break;

                case Keys.Space:
                    gestureDetector = GestureDetector.getGestureDetector;
                    bool success = gestureDetector.recordGesture(gestureName.Text);

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
            System.Diagnostics.Process.Start(@"Resources\fingeralphabet-GERv02-A4_G01.pdf");
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            //if (gestureDetector != null)
            //{
            gestureDetector = GestureDetector.getGestureDetector;
            bool success = gestureDetector.recordGesture(gestureName.Text);

            if (!success)
            {
                MessageBox.Show("Could not store Gesture", "Gesture Store Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            //}
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            if (gestureName.Text != "")
            {
                //Open the file written above and read values from it.
                Console.WriteLine("Resources/Gestures/" + gestureName.Text + ".dat");
                Stream stream = File.Open(@"Resources/Gestures/" + gestureName.Text + ".dat", FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter = new BinaryFormatter();

                Console.WriteLine("Reading Gesture");
                Gesture g = (Gesture)bformatter.Deserialize(stream);
                MessageBox.Show(g.hand.Contour.Count.ToString());
                stream.Close();
            }

        }

        private void shapeBox_Paint(object sender, PaintEventArgs e)
        {

            gestureDetector = GestureDetector.getGestureDetector;
            Gesture gesture = gestureDetector.getActualGesture();

            Console.WriteLine(gesture);

            //e.Graphics.DrawPolygon(new Pen(Color.White,2),  MathHelper.convertToDrawablePointList(gesture).ToArray());
            e.Graphics.DrawLine(
            new Pen(Color.Red, 2f),
            new System.Drawing.Point(0, 0),
            new System.Drawing.Point(shapeBox.Size.Width, shapeBox.Size.Height));

            //e.Graphics.DrawEllipse(
            //    new Pen(Color.Red, 2f),
            //    0, 0, shapeBox.Size.Width, shapeBox.Size.Height);
        }
    }
}
