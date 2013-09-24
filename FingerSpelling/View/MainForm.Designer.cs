﻿using System.Windows.Forms;

namespace FingerSpelling.View
{
    partial class AppForm
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBoxOrientation = new System.Windows.Forms.GroupBox();
            this.radioButtonRighty = new System.Windows.Forms.RadioButton();
            this.radioButtonLefty = new System.Windows.Forms.RadioButton();
            this.fingeralphabetButton = new System.Windows.Forms.Button();
            this.settingsButton = new System.Windows.Forms.Button();
            this.groupBoxCamera = new System.Windows.Forms.GroupBox();
            this.radioButtonDepth = new System.Windows.Forms.RadioButton();
            this.radioButtonRGB = new System.Windows.Forms.RadioButton();
            this.groupBoxOutput = new System.Windows.Forms.GroupBox();
            this.radioButtonSpeech = new System.Windows.Forms.RadioButton();
            this.radioButtonText = new System.Windows.Forms.RadioButton();
            this.exitButton = new System.Windows.Forms.Button();
            this.labelKey1 = new System.Windows.Forms.Label();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.labelKey2 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.labelKey3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.volumeTextField = new System.Windows.Forms.TextBox();
            this.fingerPointTextField = new System.Windows.Forms.TextBox();
            this.pointsTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exportGestureButton = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripMemoryStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.shapeBox = new System.Windows.Forms.PictureBox();
            this.readButton = new System.Windows.Forms.Button();
            this.gestureName = new System.Windows.Forms.TextBox();
            this.recordButton = new System.Windows.Forms.Button();
            this.videoControl = new CCT.NUI.Visual.VideoControl();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStripStatusLabelFPS = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.groupBoxOrientation.SuspendLayout();
            this.groupBoxCamera.SuspendLayout();
            this.groupBoxOutput.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shapeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.groupBoxOrientation);
            this.splitContainer.Panel1.Controls.Add(this.fingeralphabetButton);
            this.splitContainer.Panel1.Controls.Add(this.settingsButton);
            this.splitContainer.Panel1.Controls.Add(this.groupBoxCamera);
            this.splitContainer.Panel1.Controls.Add(this.groupBoxOutput);
            this.splitContainer.Panel1.Controls.Add(this.exitButton);
            this.splitContainer.Panel1.Controls.Add(this.labelKey1);
            this.splitContainer.Panel1.Controls.Add(this.linkLabel);
            this.splitContainer.Panel1.Controls.Add(this.labelKey2);
            this.splitContainer.Panel1.Controls.Add(this.startButton);
            this.splitContainer.Panel1.Controls.Add(this.labelKey3);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.label4);
            this.splitContainer.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer.Panel2.Controls.Add(this.exportGestureButton);
            this.splitContainer.Panel2.Controls.Add(this.statusStrip);
            this.splitContainer.Panel2.Controls.Add(this.shapeBox);
            this.splitContainer.Panel2.Controls.Add(this.readButton);
            this.splitContainer.Panel2.Controls.Add(this.gestureName);
            this.splitContainer.Panel2.Controls.Add(this.recordButton);
            this.splitContainer.Panel2.Controls.Add(this.videoControl);
            this.splitContainer.Size = new System.Drawing.Size(1004, 807);
            this.splitContainer.SplitterDistance = 255;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 0;
            // 
            // groupBoxOrientation
            // 
            this.groupBoxOrientation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.groupBoxOrientation.Controls.Add(this.radioButtonRighty);
            this.groupBoxOrientation.Controls.Add(this.radioButtonLefty);
            this.groupBoxOrientation.ForeColor = System.Drawing.Color.White;
            this.groupBoxOrientation.Location = new System.Drawing.Point(38, 450);
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
            this.fingeralphabetButton.Location = new System.Drawing.Point(25, 219);
            this.fingeralphabetButton.Name = "fingeralphabetButton";
            this.fingeralphabetButton.Size = new System.Drawing.Size(213, 30);
            this.fingeralphabetButton.TabIndex = 18;
            this.fingeralphabetButton.Text = "Show German Fingeralphabet";
            this.fingeralphabetButton.UseVisualStyleBackColor = true;
            this.fingeralphabetButton.Click += new System.EventHandler(this.fingeralphabetButton_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.Location = new System.Drawing.Point(78, 171);
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
            this.groupBoxCamera.Location = new System.Drawing.Point(38, 360);
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
            this.radioButtonDepth.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
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
            this.radioButtonRGB.CheckedChanged += new System.EventHandler(this.radioButtons_CheckedChanged);
            // 
            // groupBoxOutput
            // 
            this.groupBoxOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.groupBoxOutput.Controls.Add(this.radioButtonSpeech);
            this.groupBoxOutput.Controls.Add(this.radioButtonText);
            this.groupBoxOutput.ForeColor = System.Drawing.Color.White;
            this.groupBoxOutput.Location = new System.Drawing.Point(38, 273);
            this.groupBoxOutput.Name = "groupBoxOutput";
            this.groupBoxOutput.Size = new System.Drawing.Size(168, 60);
            this.groupBoxOutput.TabIndex = 3;
            this.groupBoxOutput.TabStop = false;
            this.groupBoxOutput.Text = "Output";
            // 
            // radioButtonSpeech
            // 
            this.radioButtonSpeech.AutoSize = true;
            this.radioButtonSpeech.Location = new System.Drawing.Point(82, 26);
            this.radioButtonSpeech.Name = "radioButtonSpeech";
            this.radioButtonSpeech.Size = new System.Drawing.Size(68, 21);
            this.radioButtonSpeech.TabIndex = 3;
            this.radioButtonSpeech.Text = "Speech";
            this.radioButtonSpeech.UseVisualStyleBackColor = true;
            this.radioButtonSpeech.Click += new System.EventHandler(this.radioButtonSpeech_Click);
            // 
            // radioButtonText
            // 
            this.radioButtonText.AutoSize = true;
            this.radioButtonText.Checked = true;
            this.radioButtonText.Location = new System.Drawing.Point(8, 26);
            this.radioButtonText.Name = "radioButtonText";
            this.radioButtonText.Size = new System.Drawing.Size(50, 21);
            this.radioButtonText.TabIndex = 2;
            this.radioButtonText.TabStop = true;
            this.radioButtonText.Text = "Text";
            this.radioButtonText.UseVisualStyleBackColor = true;
            this.radioButtonText.Click += new System.EventHandler(this.radioButtonText_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(48, 65);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(150, 30);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // labelKey1
            // 
            this.labelKey1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.labelKey1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKey1.ForeColor = System.Drawing.Color.White;
            this.labelKey1.Location = new System.Drawing.Point(56, 571);
            this.labelKey1.Name = "labelKey1";
            this.labelKey1.Size = new System.Drawing.Size(40, 40);
            this.labelKey1.TabIndex = 6;
            this.labelKey1.Text = "1";
            this.labelKey1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // labelKey2
            // 
            this.labelKey2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.labelKey2.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKey2.ForeColor = System.Drawing.Color.White;
            this.labelKey2.Location = new System.Drawing.Point(102, 571);
            this.labelKey2.Name = "labelKey2";
            this.labelKey2.Size = new System.Drawing.Size(40, 40);
            this.labelKey2.TabIndex = 7;
            this.labelKey2.Text = "2";
            this.labelKey2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(48, 16);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(150, 30);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start Detection";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // labelKey3
            // 
            this.labelKey3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(69)))), ((int)(((byte)(75)))));
            this.labelKey3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKey3.ForeColor = System.Drawing.Color.White;
            this.labelKey3.Location = new System.Drawing.Point(148, 571);
            this.labelKey3.Name = "labelKey3";
            this.labelKey3.Size = new System.Drawing.Size(40, 40);
            this.labelKey3.TabIndex = 8;
            this.labelKey3.Text = "3";
            this.labelKey3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(46, 514);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 32;
            this.label4.Text = "Gesture";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.volumeTextField);
            this.groupBox1.Controls.Add(this.fingerPointTextField);
            this.groupBox1.Controls.Add(this.pointsTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(49, 550);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(312, 218);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detection";
            // 
            // volumeTextField
            // 
            this.volumeTextField.Location = new System.Drawing.Point(114, 85);
            this.volumeTextField.Name = "volumeTextField";
            this.volumeTextField.ReadOnly = true;
            this.volumeTextField.Size = new System.Drawing.Size(150, 25);
            this.volumeTextField.TabIndex = 23;
            // 
            // fingerPointTextField
            // 
            this.fingerPointTextField.Location = new System.Drawing.Point(114, 41);
            this.fingerPointTextField.Name = "fingerPointTextField";
            this.fingerPointTextField.ReadOnly = true;
            this.fingerPointTextField.Size = new System.Drawing.Size(40, 25);
            this.fingerPointTextField.TabIndex = 22;
            // 
            // pointsTextBox
            // 
            this.pointsTextBox.Location = new System.Drawing.Point(114, 132);
            this.pointsTextBox.Name = "pointsTextBox";
            this.pointsTextBox.ReadOnly = true;
            this.pointsTextBox.Size = new System.Drawing.Size(40, 25);
            this.pointsTextBox.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(23, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 17);
            this.label3.TabIndex = 27;
            this.label3.Text = "Points";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Finger count";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(23, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "Volume";
            // 
            // exportGestureButton
            // 
            this.exportGestureButton.Location = new System.Drawing.Point(331, 507);
            this.exportGestureButton.Name = "exportGestureButton";
            this.exportGestureButton.Size = new System.Drawing.Size(90, 30);
            this.exportGestureButton.TabIndex = 30;
            this.exportGestureButton.Text = "Export";
            this.exportGestureButton.UseVisualStyleBackColor = true;
            this.exportGestureButton.Click += new System.EventHandler(this.exportGestureButton_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMemoryStatusLabel,
            this.toolStripStatusLabelFPS});
            this.statusStrip.Location = new System.Drawing.Point(0, 783);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(742, 22);
            this.statusStrip.TabIndex = 29;
            // 
            // toolStripMemoryStatusLabel
            // 
            this.toolStripMemoryStatusLabel.ForeColor = System.Drawing.Color.White;
            this.toolStripMemoryStatusLabel.Name = "toolStripMemoryStatusLabel";
            this.toolStripMemoryStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // shapeBox
            // 
            this.shapeBox.Location = new System.Drawing.Point(369, 543);
            this.shapeBox.Name = "shapeBox";
            this.shapeBox.Size = new System.Drawing.Size(320, 240);
            this.shapeBox.TabIndex = 26;
            this.shapeBox.TabStop = false;
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(438, 506);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(90, 30);
            this.readButton.TabIndex = 21;
            this.readButton.Text = "Read";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // gestureName
            // 
            this.gestureName.Location = new System.Drawing.Point(126, 510);
            this.gestureName.Name = "gestureName";
            this.gestureName.Size = new System.Drawing.Size(77, 25);
            this.gestureName.TabIndex = 20;
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(223, 507);
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
            this.videoControl.Location = new System.Drawing.Point(49, 16);
            this.videoControl.Name = "videoControl";
            this.videoControl.Size = new System.Drawing.Size(640, 480);
            this.videoControl.Stretch = false;
            this.videoControl.TabIndex = 0;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Text = "everything is connected";
            this.notifyIcon.Visible = true;
            // 
            // toolStripStatusLabelFPS
            // 
            this.toolStripStatusLabelFPS.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabelFPS.Name = "toolStripStatusLabelFPS";
            this.toolStripStatusLabelFPS.Size = new System.Drawing.Size(0, 17);
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(28)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(1004, 807);
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AppForm";
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
            this.groupBoxOrientation.ResumeLayout(false);
            this.groupBoxOrientation.PerformLayout();
            this.groupBoxCamera.ResumeLayout(false);
            this.groupBoxCamera.PerformLayout();
            this.groupBoxOutput.ResumeLayout(false);
            this.groupBoxOutput.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.shapeBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox groupBoxOutput;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.RadioButton radioButtonSpeech;
        private System.Windows.Forms.RadioButton radioButtonText;
        private System.Windows.Forms.Button exitButton;
        private CCT.NUI.Visual.VideoControl videoControl;
        private System.Windows.Forms.GroupBox groupBoxCamera;
        private System.Windows.Forms.RadioButton radioButtonDepth;
        private System.Windows.Forms.RadioButton radioButtonRGB;
        private System.Windows.Forms.Label labelKey1;
        private System.Windows.Forms.Label labelKey3;
        private System.Windows.Forms.Label labelKey2;
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
        private StatusStrip statusStrip;
        private ToolStripStatusLabel toolStripMemoryStatusLabel;
        private Label label4;
        private ToolStripStatusLabel toolStripStatusLabelFPS;

    }
}

