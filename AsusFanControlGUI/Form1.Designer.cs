﻿namespace AsusFanControlGUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.trackBarFanSpeed = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRefreshRPM = new System.Windows.Forms.Button();
            this.labelRPM = new System.Windows.Forms.Label();
            this.checkBoxTurnOn = new System.Windows.Forms.CheckBox();
            this.labelCPUTemp = new System.Windows.Forms.Label();
            this.buttonRefreshCPUTemp = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTurnOffControlOnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemForbidUnsafeSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMinimizeToTrayOnClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAutoRefreshStats = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxAutoTempControl = new System.Windows.Forms.CheckBox();
            this.labelTempThreshold = new System.Windows.Forms.Label();
            this.numericUpDownTempThreshold = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFanSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempThreshold)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // trackBarFanSpeed
            //
            this.trackBarFanSpeed.Location = new System.Drawing.Point(12, 62);
            this.trackBarFanSpeed.Maximum = 100;
            this.trackBarFanSpeed.Name = "trackBarFanSpeed";
            this.trackBarFanSpeed.Size = new System.Drawing.Size(300, 45);
            this.trackBarFanSpeed.TabIndex = 0;
            this.trackBarFanSpeed.Value = 100;
            this.trackBarFanSpeed.KeyUp += new System.Windows.Forms.KeyEventHandler(this.trackBarFanSpeed_KeyUp);
            this.trackBarFanSpeed.MouseCaptureChanged += new System.EventHandler(this.trackBarFanSpeed_MouseCaptureChanged);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current value:";
            //
            // labelValue
            //
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(91, 110);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(10, 13);
            this.labelValue.TabIndex = 2;
            this.labelValue.Text = "-";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Current RPM:";
            //
            // buttonRefreshRPM
            //
            this.buttonRefreshRPM.Location = new System.Drawing.Point(12, 134);
            this.buttonRefreshRPM.Name = "buttonRefreshRPM";
            this.buttonRefreshRPM.Size = new System.Drawing.Size(22, 23);
            this.buttonRefreshRPM.TabIndex = 4;
            this.buttonRefreshRPM.Text = "↻";
            this.buttonRefreshRPM.UseVisualStyleBackColor = true;
            this.buttonRefreshRPM.Click += new System.EventHandler(this.buttonRefreshRPM_Click);
            //
            // labelRPM
            //
            this.labelRPM.AutoSize = true;
            this.labelRPM.Location = new System.Drawing.Point(117, 139);
            this.labelRPM.Name = "labelRPM";
            this.labelRPM.Size = new System.Drawing.Size(10, 13);
            this.labelRPM.TabIndex = 5;
            this.labelRPM.Text = "-";
            //
            // checkBoxTurnOn
            //
            this.checkBoxTurnOn.AutoSize = true;
            this.checkBoxTurnOn.Location = new System.Drawing.Point(12, 37);
            this.checkBoxTurnOn.Name = "checkBoxTurnOn";
            this.checkBoxTurnOn.Size = new System.Drawing.Size(116, 17);
            this.checkBoxTurnOn.TabIndex = 6;
            this.checkBoxTurnOn.Text = "Turn on fan control";
            this.checkBoxTurnOn.UseVisualStyleBackColor = true;
            this.checkBoxTurnOn.CheckedChanged += new System.EventHandler(this.checkBoxTurnOn_CheckedChanged);
            //
            // labelCPUTemp
            //
            this.labelCPUTemp.AutoSize = true;
            this.labelCPUTemp.Location = new System.Drawing.Point(141, 168);
            this.labelCPUTemp.Name = "labelCPUTemp";
            this.labelCPUTemp.Size = new System.Drawing.Size(10, 13);
            this.labelCPUTemp.TabIndex = 9;
            this.labelCPUTemp.Text = "-";
            //
            // buttonRefreshCPUTemp
            //
            this.buttonRefreshCPUTemp.Location = new System.Drawing.Point(12, 163);
            this.buttonRefreshCPUTemp.Name = "buttonRefreshCPUTemp";
            this.buttonRefreshCPUTemp.Size = new System.Drawing.Size(22, 23);
            this.buttonRefreshCPUTemp.TabIndex = 8;
            this.buttonRefreshCPUTemp.Text = "↻";
            this.buttonRefreshCPUTemp.UseVisualStyleBackColor = true;
            this.buttonRefreshCPUTemp.Click += new System.EventHandler(this.buttonRefreshCPUTemp_Click);
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Current CPU temp:";
            //
            // menuStrip1
            //
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItemCheckForUpdates});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(324, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            //
            // toolStripMenuItem1
            //
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTurnOffControlOnExit,
            this.toolStripMenuItemForbidUnsafeSettings,
            this.toolStripMenuItemMinimizeToTrayOnClose,
            this.toolStripMenuItemAutoRefreshStats});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(72, 20);
            this.toolStripMenuItem1.Text = "Advanced";
            //
            // toolStripMenuItemTurnOffControlOnExit
            //
            this.toolStripMenuItemTurnOffControlOnExit.CheckOnClick = true;
            this.toolStripMenuItemTurnOffControlOnExit.Name = "toolStripMenuItemTurnOffControlOnExit";
            this.toolStripMenuItemTurnOffControlOnExit.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItemTurnOffControlOnExit.Text = "Turn off control on exit";
            this.toolStripMenuItemTurnOffControlOnExit.CheckedChanged += new System.EventHandler(this.toolStripMenuItemTurnOffControlOnExit_CheckedChanged);
            //
            // toolStripMenuItemForbidUnsafeSettings
            //
            this.toolStripMenuItemForbidUnsafeSettings.CheckOnClick = true;
            this.toolStripMenuItemForbidUnsafeSettings.Name = "toolStripMenuItemForbidUnsafeSettings";
            this.toolStripMenuItemForbidUnsafeSettings.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItemForbidUnsafeSettings.Text = "Forbid unsafe settings";
            this.toolStripMenuItemForbidUnsafeSettings.CheckedChanged += new System.EventHandler(this.toolStripMenuItemForbidUnsafeSettings_CheckedChanged);
            //
            // toolStripMenuItemMinimizeToTrayOnClose
            //
            this.toolStripMenuItemMinimizeToTrayOnClose.CheckOnClick = true;
            this.toolStripMenuItemMinimizeToTrayOnClose.Name = "toolStripMenuItemMinimizeToTrayOnClose";
            this.toolStripMenuItemMinimizeToTrayOnClose.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItemMinimizeToTrayOnClose.Text = "Minimize to tray on close";
            this.toolStripMenuItemMinimizeToTrayOnClose.Click += new System.EventHandler(this.toolStripMenuItemMinimizeToTrayOnClose_Click);
            //
            // toolStripMenuItemAutoRefreshStats
            //
            this.toolStripMenuItemAutoRefreshStats.CheckOnClick = true;
            this.toolStripMenuItemAutoRefreshStats.Name = "toolStripMenuItemAutoRefreshStats";
            this.toolStripMenuItemAutoRefreshStats.Size = new System.Drawing.Size(207, 22);
            this.toolStripMenuItemAutoRefreshStats.Text = "Auto refresh stats";
            this.toolStripMenuItemAutoRefreshStats.Click += new System.EventHandler(this.toolStripMenuItemAutoRefreshStats_Click);
            //
            // checkBoxAutoTempControl
            //
            this.checkBoxAutoTempControl.AutoSize = true;
            this.checkBoxAutoTempControl.Location = new System.Drawing.Point(12, 195);
            this.checkBoxAutoTempControl.Name = "checkBoxAutoTempControl";
            this.checkBoxAutoTempControl.Size = new System.Drawing.Size(221, 17);
            this.checkBoxAutoTempControl.TabIndex = 11;
            this.checkBoxAutoTempControl.Text = "Turn on fan control when temperature exceeds";
            this.checkBoxAutoTempControl.UseVisualStyleBackColor = true;
            this.checkBoxAutoTempControl.CheckedChanged += new System.EventHandler(this.checkBoxAutoTempControl_CheckedChanged);
            //
            // labelTempThreshold
            //
            this.labelTempThreshold.AutoSize = true;
            this.labelTempThreshold.Location = new System.Drawing.Point(289, 196);
            this.labelTempThreshold.Name = "labelTempThreshold";
            this.labelTempThreshold.Size = new System.Drawing.Size(18, 13);
            this.labelTempThreshold.TabIndex = 12;
            this.labelTempThreshold.Text = "°C";
            //
            // numericUpDownTempThreshold
            //
            this.numericUpDownTempThreshold.Location = new System.Drawing.Point(239, 194);
            this.numericUpDownTempThreshold.Maximum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownTempThreshold.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDownTempThreshold.Name = "numericUpDownTempThreshold";
            this.numericUpDownTempThreshold.Size = new System.Drawing.Size(44, 20);
            this.numericUpDownTempThreshold.TabIndex = 13;
            this.numericUpDownTempThreshold.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numericUpDownTempThreshold.ValueChanged += new System.EventHandler(this.numericUpDownTempThreshold_ValueChanged);
            //
            // toolStripMenuItemCheckForUpdates
            //
            this.toolStripMenuItemCheckForUpdates.Name = "toolStripMenuItemCheckForUpdates";
            this.toolStripMenuItemCheckForUpdates.Size = new System.Drawing.Size(115, 20);
            this.toolStripMenuItemCheckForUpdates.Text = "Check for updates";
            this.toolStripMenuItemCheckForUpdates.Click += new System.EventHandler(this.toolStripMenuItemCheckForUpdates_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 230);
            this.Controls.Add(this.numericUpDownTempThreshold);
            this.Controls.Add(this.labelTempThreshold);
            this.Controls.Add(this.checkBoxAutoTempControl);
            this.Controls.Add(this.labelCPUTemp);
            this.Controls.Add(this.buttonRefreshCPUTemp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxTurnOn);
            this.Controls.Add(this.labelRPM);
            this.Controls.Add(this.buttonRefreshRPM);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBarFanSpeed);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Asus Fan Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFanSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTempThreshold)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarFanSpeed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonRefreshRPM;
        private System.Windows.Forms.Label labelRPM;
        private System.Windows.Forms.CheckBox checkBoxTurnOn;
        private System.Windows.Forms.Label labelCPUTemp;
        private System.Windows.Forms.Button buttonRefreshCPUTemp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTurnOffControlOnExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemForbidUnsafeSettings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCheckForUpdates;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMinimizeToTrayOnClose;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAutoRefreshStats;
        private System.Windows.Forms.CheckBox checkBoxAutoTempControl;
        private System.Windows.Forms.Label labelTempThreshold;
        private System.Windows.Forms.NumericUpDown numericUpDownTempThreshold;
    }
}
