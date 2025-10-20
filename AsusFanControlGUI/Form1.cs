using AsusFanControl;
using System;
using System.Windows.Forms;

namespace AsusFanControlGUI
{
    public partial class Form1 : Form
    {
        AsusControl asusControl = new AsusControl();
        int fanSpeed = 0;
        Timer timer;
        NotifyIcon trayIcon;
        bool autoControlActive = false;
        bool programmaticChange = false;

        public Form1()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            toolStripMenuItemTurnOffControlOnExit.Checked = Properties.Settings.Default.turnOffControlOnExit;
            toolStripMenuItemForbidUnsafeSettings.Checked = Properties.Settings.Default.forbidUnsafeSettings;
            toolStripMenuItemMinimizeToTrayOnClose.Checked = Properties.Settings.Default.minimizeToTrayOnClose;
            toolStripMenuItemAutoRefreshStats.Checked = Properties.Settings.Default.autoRefreshStats;
            trackBarFanSpeed.Value = Properties.Settings.Default.fanSpeed;
            checkBoxAutoTempControl.Checked = Properties.Settings.Default.autoTempControl;
            numericUpDownTempThreshold.Value = Properties.Settings.Default.tempThreshold;

            // Check if launched with minimize argument OR running as SYSTEM user (via PsExec)
            string[] args = Environment.GetCommandLineArgs();
            bool shouldMinimize = false;

            // Check for explicit minimize argument
            if (args.Length > 1 && args[1] == "/minimize")
            {
                shouldMinimize = true;
            }

            // Check if running as SYSTEM user (PsExec launches as SYSTEM)
            try
            {
                string currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                if (currentUser.Equals("NT AUTHORITY\\SYSTEM", StringComparison.OrdinalIgnoreCase))
                {
                    shouldMinimize = true;
                }
            }
            catch
            {
                // If we can't get user info, assume we should minimize for background operation
                shouldMinimize = true;
            }

            if (shouldMinimize)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Visible = false;
                CreateTrayIcon();
                trayIcon.Visible = true;
            }
        }

        private void OnProcessExit(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.turnOffControlOnExit)
                asusControl.SetFanSpeeds(0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timerRefreshStats();

            // If starting minimized and auto temp control is enabled, ensure refresh is on
            if (this.WindowState == FormWindowState.Minimized && checkBoxAutoTempControl.Checked)
            {
                if (!toolStripMenuItemAutoRefreshStats.Checked)
                {
                    toolStripMenuItemAutoRefreshStats.Checked = true;
                    toolStripMenuItemAutoRefreshStats_Click(null, null);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.minimizeToTrayOnClose && Visible)
            {
                CreateTrayIcon();
                trayIcon.Visible = true;
                e.Cancel = true;
                Hide();
            }
        }

        private void CreateTrayIcon()
        {
            if(trayIcon == null)
            {
                trayIcon = new NotifyIcon()
                {
                    Icon = Icon,
                    ContextMenu = new ContextMenu(new MenuItem[] {
                        new MenuItem("Show", (s1, e1) =>
                        {
                            trayIcon.Visible = false;
                            Show();
                            this.WindowState = FormWindowState.Normal;
                            this.ShowInTaskbar = true;
                        }),
                        new MenuItem("Exit", (s1, e1) =>
                        {
                            trayIcon.Visible = false;
                            Application.Exit();
                        }),
                    }),
                };

                trayIcon.MouseClick += (s1, e1) =>
                {
                    if (e1.Button != MouseButtons.Left)
                        return;

                    trayIcon.Visible = false;
                    Show();
                    this.WindowState = FormWindowState.Normal;
                    this.ShowInTaskbar = true;
                };
            }
        }

        private void timerRefreshStats()
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }

            if (!Properties.Settings.Default.autoRefreshStats)
                return;

            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Start();
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            buttonRefreshRPM_Click(sender, e);
            buttonRefreshCPUTemp_Click(sender, e);

            // Check for automatic temperature control
            if (checkBoxAutoTempControl.Checked)
            {
                var currentTemp = asusControl.Thermal_Read_Cpu_Temperature();
                var threshold = (ulong)numericUpDownTempThreshold.Value;

                if (currentTemp >= threshold && !autoControlActive)
                {
                    // Temperature exceeded threshold, turn on fan control
                    autoControlActive = true;
                    programmaticChange = true;
                    checkBoxTurnOn.Checked = true;
                    programmaticChange = false;
                }
                else if (currentTemp < threshold - 5 && autoControlActive) // 5 degree hysteresis
                {
                    // Temperature dropped sufficiently below threshold, turn off fan control
                    autoControlActive = false;
                    programmaticChange = true;
                    checkBoxTurnOn.Checked = false;
                    programmaticChange = false;
                }
            }
            else
            {
                autoControlActive = false;
            }
        }

        private void toolStripMenuItemTurnOffControlOnExit_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.turnOffControlOnExit = toolStripMenuItemTurnOffControlOnExit.Checked;
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItemForbidUnsafeSettings_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.forbidUnsafeSettings = toolStripMenuItemForbidUnsafeSettings.Checked;
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItemMinimizeToTrayOnClose_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.minimizeToTrayOnClose = toolStripMenuItemMinimizeToTrayOnClose.Checked;
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItemAutoRefreshStats_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoRefreshStats = toolStripMenuItemAutoRefreshStats.Checked;
            Properties.Settings.Default.Save();

            timerRefreshStats();
        }

        private void toolStripMenuItemCheckForUpdates_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Karmel0x/AsusFanControl/releases");
        }

        private void setFanSpeed()
        {
            var value = trackBarFanSpeed.Value;
            Properties.Settings.Default.fanSpeed = value;
            Properties.Settings.Default.Save();

            if (!checkBoxTurnOn.Checked)
                value = 0;

            if (value == 0)
                labelValue.Text = "turned off";
            else
                labelValue.Text = value.ToString();

            if (fanSpeed == value)
                return;

            fanSpeed = value;

            asusControl.SetFanSpeeds(value);
        }

        private void checkBoxTurnOn_CheckedChanged(object sender, EventArgs e)
        {
            // If this is a programmatic change from auto control, don't interfere
            if (programmaticChange)
            {
                setFanSpeed();
                return;
            }

            // If manual control is used while auto control is active, disable auto control
            if (autoControlActive)
            {
                autoControlActive = false;
            }
            setFanSpeed();
        }

        private void trackBarFanSpeed_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.forbidUnsafeSettings)
            {
                if (trackBarFanSpeed.Value < 40)
                    trackBarFanSpeed.Value = 40;
                else if (trackBarFanSpeed.Value > 99)
                    trackBarFanSpeed.Value = 99;
            }

            setFanSpeed();
        }

        private void trackBarFanSpeed_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right)
                return;

            trackBarFanSpeed_MouseCaptureChanged(sender, e);
        }

        private void buttonRefreshRPM_Click(object sender, EventArgs e)
        {
            labelRPM.Text = string.Join(" ", asusControl.GetFanSpeeds());
        }

        private void buttonRefreshCPUTemp_Click(object sender, EventArgs e)
        {
            labelCPUTemp.Text = $"{asusControl.Thermal_Read_Cpu_Temperature()}";
        }

        private void checkBoxAutoTempControl_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoTempControl = checkBoxAutoTempControl.Checked;
            Properties.Settings.Default.Save();

            // Enable/disable the threshold input based on checkbox state
            numericUpDownTempThreshold.Enabled = checkBoxAutoTempControl.Checked;

            if (checkBoxAutoTempControl.Checked)
            {
                // Ensure auto refresh is enabled for temperature monitoring
                if (!toolStripMenuItemAutoRefreshStats.Checked)
                {
                    toolStripMenuItemAutoRefreshStats.Checked = true;
                    toolStripMenuItemAutoRefreshStats_Click(sender, e);
                }
            }
            else
            {
                // Reset auto control state
                autoControlActive = false;
            }
        }

        private void numericUpDownTempThreshold_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.tempThreshold = (int)numericUpDownTempThreshold.Value;
            Properties.Settings.Default.Save();
        }

    }
}
