using System.Windows.Forms;

namespace FingerSpelling.View
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.speechCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.detectionRateField = new System.Windows.Forms.NumericUpDown();
            this.groupBoxOrientation = new System.Windows.Forms.GroupBox();
            this.radioButtonRighty = new System.Windows.Forms.RadioButton();
            this.radioButtonLefty = new System.Windows.Forms.RadioButton();
            this.fingeralphabetButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.groupBoxCamera = new System.Windows.Forms.GroupBox();
            this.radioButtonDepth = new System.Windows.Forms.RadioButton();
            this.radioButtonRGB = new System.Windows.Forms.RadioButton();
            this.exitButton = new System.Windows.Forms.Button();
            this.detectedValueField = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.startButton = new System.Windows.Forms.Button();
            this.fixPoint = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.centerPointBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.palmPointBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.volumeTextField = new System.Windows.Forms.TextBox();
            this.fingerPointTextField = new System.Windows.Forms.TextBox();
            this.pointsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exportGestureButton = new System.Windows.Forms.Button();
            this.shapeBox = new System.Windows.Forms.PictureBox();
            this.readButton = new System.Windows.Forms.Button();
            this.gestureName = new System.Windows.Forms.TextBox();
            this.recordButton = new System.Windows.Forms.Button();
            this.videoControl = new CCT.NUI.Visual.VideoControl();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detectionRateField)).BeginInit();
            this.groupBoxOrientation.SuspendLayout();
            this.groupBoxCamera.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shapeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BackColor = System.Drawing.Color.White;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.splitContainer.Panel1.Controls.Add(this.speechCheckBox);
            this.splitContainer.Panel1.Controls.Add(this.label8);
            this.splitContainer.Panel1.Controls.Add(this.label7);
            this.splitContainer.Panel1.Controls.Add(this.detectionRateField);
            this.splitContainer.Panel1.Controls.Add(this.groupBoxOrientation);
            this.splitContainer.Panel1.Controls.Add(this.fingeralphabetButton);
            this.splitContainer.Panel1.Controls.Add(this.settingsButton);
            this.splitContainer.Panel1.Controls.Add(this.groupBoxCamera);
            this.splitContainer.Panel1.Controls.Add(this.exitButton);
            this.splitContainer.Panel1.Controls.Add(this.detectedValueField);
            this.splitContainer.Panel1.Controls.Add(this.linkLabel);
            this.splitContainer.Panel1.Controls.Add(this.startButton);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.splitContainer.Panel2.Controls.Add(this.fixPoint);
            this.splitContainer.Panel2.Controls.Add(this.label4);
            this.splitContainer.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer.Panel2.Controls.Add(this.exportGestureButton);
            this.splitContainer.Panel2.Controls.Add(this.shapeBox);
            this.splitContainer.Panel2.Controls.Add(this.readButton);
            this.splitContainer.Panel2.Controls.Add(this.gestureName);
            this.splitContainer.Panel2.Controls.Add(this.recordButton);
            this.splitContainer.Panel2.Controls.Add(this.videoControl);
            this.splitContainer.Size = new System.Drawing.Size(894, 772);
            this.splitContainer.SplitterDistance = 230;
            this.splitContainer.SplitterWidth = 2;
            this.splitContainer.TabIndex = 0;
            // 
            // speechCheckBox
            // 
            this.speechCheckBox.AutoSize = true;
            this.speechCheckBox.ForeColor = System.Drawing.Color.White;
            this.speechCheckBox.Location = new System.Drawing.Point(51, 552);
            this.speechCheckBox.Name = "speechCheckBox";
            this.speechCheckBox.Size = new System.Drawing.Size(123, 21);
            this.speechCheckBox.TabIndex = 22;
            this.speechCheckBox.Text = "Result to speech";
            this.speechCheckBox.UseVisualStyleBackColor = true;
            this.speechCheckBox.CheckedChanged += new System.EventHandler(this.speechCheckBox_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(183, 506);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 17);
            this.label8.TabIndex = 21;
            this.label8.Text = "ms";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(27, 504);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 17);
            this.label7.TabIndex = 20;
            this.label7.Text = "Detectionrate";
            // 
            // detectionRateField
            // 
            this.detectionRateField.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.detectionRateField.Location = new System.Drawing.Point(119, 502);
            this.detectionRateField.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.detectionRateField.Name = "detectionRateField";
            this.detectionRateField.ReadOnly = true;
            this.detectionRateField.Size = new System.Drawing.Size(58, 25);
            this.detectionRateField.TabIndex = 19;
            this.detectionRateField.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.detectionRateField.ValueChanged += new System.EventHandler(this.detectionRateField_ValueChanged);
            // 
            // groupBoxOrientation
            // 
            this.groupBoxOrientation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.groupBoxOrientation.Controls.Add(this.radioButtonRighty);
            this.groupBoxOrientation.Controls.Add(this.radioButtonLefty);
            this.groupBoxOrientation.ForeColor = System.Drawing.Color.White;
            this.groupBoxOrientation.Location = new System.Drawing.Point(30, 303);
            this.groupBoxOrientation.Name = "groupBoxOrientation";
            this.groupBoxOrientation.Size = new System.Drawing.Size(168, 60);
            this.groupBoxOrientation.TabIndex = 11;
            this.groupBoxOrientation.TabStop = false;
            this.groupBoxOrientation.Text = "Orientation";
            // 
            // radioButtonRighty
            // 
            this.radioButtonRighty.AutoSize = true;
            this.radioButtonRighty.Checked = true;
            this.radioButtonRighty.Location = new System.Drawing.Point(82, 26);
            this.radioButtonRighty.Name = "radioButtonRighty";
            this.radioButtonRighty.Size = new System.Drawing.Size(62, 21);
            this.radioButtonRighty.TabIndex = 3;
            this.radioButtonRighty.TabStop = true;
            this.radioButtonRighty.Text = "Righty";
            this.radioButtonRighty.UseVisualStyleBackColor = true;
            // 
            // radioButtonLefty
            // 
            this.radioButtonLefty.AutoSize = true;
            this.radioButtonLefty.Location = new System.Drawing.Point(8, 26);
            this.radioButtonLefty.Name = "radioButtonLefty";
            this.radioButtonLefty.Size = new System.Drawing.Size(53, 21);
            this.radioButtonLefty.TabIndex = 2;
            this.radioButtonLefty.Text = "Lefty";
            this.radioButtonLefty.UseVisualStyleBackColor = true;
            // 
            // fingeralphabetButton
            // 
            this.fingeralphabetButton.Location = new System.Drawing.Point(18, 136);
            this.fingeralphabetButton.Name = "fingeralphabetButton";
            this.fingeralphabetButton.Size = new System.Drawing.Size(195, 30);
            this.fingeralphabetButton.TabIndex = 18;
            this.fingeralphabetButton.Text = "Show German Fingeralphabet";
            this.fingeralphabetButton.UseVisualStyleBackColor = true;
            this.fingeralphabetButton.Click += new System.EventHandler(this.fingeralphabetButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(123, 83);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(90, 30);
            this.settingsButton.TabIndex = 10;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // groupBoxCamera
            // 
            this.groupBoxCamera.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.groupBoxCamera.Controls.Add(this.radioButtonDepth);
            this.groupBoxCamera.Controls.Add(this.radioButtonRGB);
            this.groupBoxCamera.ForeColor = System.Drawing.Color.White;
            this.groupBoxCamera.Location = new System.Drawing.Point(28, 212);
            this.groupBoxCamera.Name = "groupBoxCamera";
            this.groupBoxCamera.Size = new System.Drawing.Size(170, 60);
            this.groupBoxCamera.TabIndex = 9;
            this.groupBoxCamera.TabStop = false;
            this.groupBoxCamera.Text = "Camera";
            // 
            // radioButtonDepth
            // 
            this.radioButtonDepth.AutoSize = true;
            this.radioButtonDepth.Location = new System.Drawing.Point(85, 26);
            this.radioButtonDepth.Name = "radioButtonDepth";
            this.radioButtonDepth.Size = new System.Drawing.Size(61, 21);
            this.radioButtonDepth.TabIndex = 3;
            this.radioButtonDepth.Text = "Depth";
            this.radioButtonDepth.UseVisualStyleBackColor = true;
            this.radioButtonDepth.CheckedChanged += new System.EventHandler(this.imageMode_CheckedChanged);
            // 
            // radioButtonRGB
            // 
            this.radioButtonRGB.AutoSize = true;
            this.radioButtonRGB.Location = new System.Drawing.Point(8, 26);
            this.radioButtonRGB.Name = "radioButtonRGB";
            this.radioButtonRGB.Size = new System.Drawing.Size(50, 21);
            this.radioButtonRGB.TabIndex = 2;
            this.radioButtonRGB.Text = "RGB";
            this.radioButtonRGB.UseVisualStyleBackColor = true;
            this.radioButtonRGB.CheckedChanged += new System.EventHandler(this.imageMode_CheckedChanged);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(18, 83);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(90, 30);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // detectedValueField
            // 
            this.detectedValueField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.detectedValueField.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.detectedValueField.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detectedValueField.ForeColor = System.Drawing.Color.White;
            this.detectedValueField.Location = new System.Drawing.Point(35, 594);
            this.detectedValueField.Name = "detectedValueField";
            this.detectedValueField.Size = new System.Drawing.Size(150, 150);
            this.detectedValueField.TabIndex = 6;
            this.detectedValueField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel.LinkColor = System.Drawing.Color.White;
            this.linkLabel.Location = new System.Drawing.Point(64, 780);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(124, 17);
            this.linkLabel.TabIndex = 6;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "www.samuelstein.de";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(18, 16);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(195, 43);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start Detection";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // fixPoint
            // 
            this.fixPoint.BackColor = System.Drawing.Color.Red;
            this.fixPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fixPoint.Location = new System.Drawing.Point(320, 340);
            this.fixPoint.Name = "fixPoint";
            this.fixPoint.Size = new System.Drawing.Size(6, 6);
            this.fixPoint.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 510);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 17);
            this.label4.TabIndex = 32;
            this.label4.Text = "Gesture Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.centerPointBox);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.palmPointBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.volumeTextField);
            this.groupBox1.Controls.Add(this.fingerPointTextField);
            this.groupBox1.Controls.Add(this.pointsTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 551);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 218);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detection";
            // 
            // centerPointBox
            // 
            this.centerPointBox.Location = new System.Drawing.Point(123, 145);
            this.centerPointBox.Name = "centerPointBox";
            this.centerPointBox.ReadOnly = true;
            this.centerPointBox.Size = new System.Drawing.Size(176, 25);
            this.centerPointBox.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(23, 152);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 17);
            this.label6.TabIndex = 31;
            this.label6.Text = "Center Point";
            // 
            // palmPointBox
            // 
            this.palmPointBox.Location = new System.Drawing.Point(123, 114);
            this.palmPointBox.Name = "palmPointBox";
            this.palmPointBox.ReadOnly = true;
            this.palmPointBox.Size = new System.Drawing.Size(128, 25);
            this.palmPointBox.TabIndex = 30;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(23, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 17);
            this.label5.TabIndex = 29;
            this.label5.Text = "Palm Point";
            // 
            // volumeTextField
            // 
            this.volumeTextField.Location = new System.Drawing.Point(123, 52);
            this.volumeTextField.Name = "volumeTextField";
            this.volumeTextField.ReadOnly = true;
            this.volumeTextField.Size = new System.Drawing.Size(128, 25);
            this.volumeTextField.TabIndex = 23;
            // 
            // fingerPointTextField
            // 
            this.fingerPointTextField.Location = new System.Drawing.Point(123, 21);
            this.fingerPointTextField.Name = "fingerPointTextField";
            this.fingerPointTextField.ReadOnly = true;
            this.fingerPointTextField.Size = new System.Drawing.Size(48, 25);
            this.fingerPointTextField.TabIndex = 22;
            // 
            // pointsTextBox
            // 
            this.pointsTextBox.Location = new System.Drawing.Point(123, 83);
            this.pointsTextBox.Name = "pointsTextBox";
            this.pointsTextBox.ReadOnly = true;
            this.pointsTextBox.Size = new System.Drawing.Size(48, 25);
            this.pointsTextBox.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(23, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 17);
            this.label3.TabIndex = 27;
            this.label3.Text = "Contour Points";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Finger count";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "Volume";
            // 
            // exportGestureButton
            // 
            this.exportGestureButton.Location = new System.Drawing.Point(332, 506);
            this.exportGestureButton.Name = "exportGestureButton";
            this.exportGestureButton.Size = new System.Drawing.Size(90, 30);
            this.exportGestureButton.TabIndex = 30;
            this.exportGestureButton.Text = "Export";
            this.exportGestureButton.UseVisualStyleBackColor = true;
            this.exportGestureButton.Click += new System.EventHandler(this.exportGestureButton_Click);
            // 
            // shapeBox
            // 
            this.shapeBox.Location = new System.Drawing.Point(332, 552);
            this.shapeBox.Name = "shapeBox";
            this.shapeBox.Size = new System.Drawing.Size(320, 240);
            this.shapeBox.TabIndex = 26;
            this.shapeBox.TabStop = false;
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(442, 506);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(90, 30);
            this.readButton.TabIndex = 21;
            this.readButton.Text = "Read";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // gestureName
            // 
            this.gestureName.Location = new System.Drawing.Point(122, 507);
            this.gestureName.Name = "gestureName";
            this.gestureName.Size = new System.Drawing.Size(77, 25);
            this.gestureName.TabIndex = 20;
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(221, 506);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(90, 30);
            this.recordButton.TabIndex = 19;
            this.recordButton.Text = "Record";
            this.recordButton.UseVisualStyleBackColor = true;
            this.recordButton.Click += new System.EventHandler(this.recordButton_Click);
            // 
            // videoControl
            // 
            this.videoControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.videoControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.videoControl.Location = new System.Drawing.Point(12, 12);
            this.videoControl.Name = "videoControl";
            this.videoControl.Size = new System.Drawing.Size(640, 481);
            this.videoControl.Stretch = false;
            this.videoControl.TabIndex = 0;
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "Finished loading app.";
            this.notifyIcon.BalloonTipTitle = "Finger Spelling App";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Loading app...";
            this.notifyIcon.Visible = true;
            this.notifyIcon.Click += new System.EventHandler(this.notifyIcon_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(894, 772);
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Finger Spelling App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AppForm_FormClosing);
            this.Load += new System.EventHandler(this.AppForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AppForm_KeyDown);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.detectionRateField)).EndInit();
            this.groupBoxOrientation.ResumeLayout(false);
            this.groupBoxOrientation.PerformLayout();
            this.groupBoxCamera.ResumeLayout(false);
            this.groupBoxCamera.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shapeBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Button exitButton;
        private CCT.NUI.Visual.VideoControl videoControl;
        private System.Windows.Forms.GroupBox groupBoxCamera;
        private System.Windows.Forms.RadioButton radioButtonDepth;
        private System.Windows.Forms.RadioButton radioButtonRGB;
        private System.Windows.Forms.Label detectedValueField;
        private System.Windows.Forms.Button settingsButton;
        private System.Windows.Forms.GroupBox groupBoxOrientation;
        private System.Windows.Forms.RadioButton radioButtonRighty;
        private System.Windows.Forms.RadioButton radioButtonLefty;
        private System.Windows.Forms.Button fingeralphabetButton;
        private System.Windows.Forms.Button recordButton;
        private System.Windows.Forms.TextBox gestureName;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox volumeTextField;
        private System.Windows.Forms.TextBox fingerPointTextField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.PictureBox shapeBox;
        private System.Windows.Forms.TextBox pointsTextBox;
        private System.Windows.Forms.Label label3;
        private GroupBox groupBox1;
        private Button exportGestureButton;
        private Label label4;
        private TextBox centerPointBox;
        private Label label6;
        private TextBox palmPointBox;
        private Label label5;
        private Label label7;
        private NumericUpDown detectionRateField;
        private Label label8;
        private CheckBox speechCheckBox;
        private Panel fixPoint;

    }
}

