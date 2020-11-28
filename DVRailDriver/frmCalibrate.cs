using PIEHid32Net;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DVRailDriver
{
    public partial class frmCalibrate : Form
    {
        private readonly PIEDevice CurrentDevice;
        private LED Display;
        private RailDriver RD;

        private Settings S;

        public frmCalibrate(PIEDevice Device)
        {
            S = Tools.GetSettings(Program.AppPath);

            Application.ApplicationExit += delegate
            {
                if (Display != null)
                {
                    Display.Dispose();
                    Display = null;
                }
                if (RD != null)
                {
                    RD.Dispose();
                    RD = null;
                }
            };
            InitializeComponent();

            S.ControllerId = CurrentDevice.Vid;
            CurrentDevice = Device;
            CurrentDevice.suppressDuplicateReports = true;
            CurrentDevice.SetupInterface();
            Display = new LED(CurrentDevice, false);
            RD = new RailDriver(CurrentDevice, S, false);
            RD.DataUpdate += RD_DataUpdate;
            //Force calibration mode
            RD.Calibrate = true;
            Display = new LED(CurrentDevice, false);
        }

        private void RD_DataUpdate(object sender)
        {
            if (InvokeRequired)
            {
                if (!IsDisposed)
                {
                    Invoke((MethodInvoker)delegate { RD_DataUpdate(sender); });
                }
            }
            else
            {
                var Device = sender as RailDriver;
                //Track bars
                tbReverser.Value = (int)Math.Abs(Device.Reverser * 100.0);
                tbThrottle.Value = (int)(Device.Throttle * 100.0);
                tbAirBrake.Value = (int)(100.0 - Device.AutoBrake * 100.0);
                tbIndBrake.Value = (int)(100.0 - Device.IndBrake * 100.0);
                tbBailOff.Value = (int)(Device.BailOff >= 0.5 ? 100 : 0);
                tbWiper.Value = (int)(Device.Wiper * 100.0);
                tbLights.Value = (int)(Device.Lights * 100.0);

                lblRevPosition.Text = $"{Math.Round(Tools.GetReverserState(Device.Reverser, S.ReverserCentral, S.ReverserDeviation) * 100)}%";
                lblAutoBrakePos.Text = $"{Math.Round(Tools.GetAutoBrakeState(1.0 - Device.AutoBrake, S.AutoBrakeSplit) * 100)}%";
                lblIndPos.Text = $"{Math.Round(100.0 - Device.IndBrake * 100.0)}%";
                lblThrottlePos.Text = $"{Math.Round(Tools.GetThrottleState(Device.Throttle, S.ThrottleStop, S.DynamicBrakeStop) * 100)}%";
                lblBailoffStatus.Text = Device.BailOff >= 0.5 ? "Yes" : "No";
                SetWiperLabel(Tools.GetTripplePos(Device.Wiper));
                SetLightLabel(Tools.GetTripplePos(Device.Lights));
            }
        }

        private void SetWiperLabel(int TriPos)
        {
            lblWiperPos.Text = "Off|Slow|Full".Split('|')[TriPos - 1];
        }

        private void SetLightLabel(int TriPos)
        {
            lblLightsPos.Text = "Off|Dim|Full".Split('|')[TriPos - 1];
        }

        private void btnSetRevCenter_Click(object sender, EventArgs e)
        {
            Display.SetText("CAL");
            Box("Move the reverser to one end, then close this dialog.");
            Box("Move the reverser to the other end, then close this dialog.");
            Box("Move the reverser to where you want the neutral position to be, then close this dialog.");
            S.ReverserCentral = RD.Reverser;
            if (Math.Abs(S.ReverserCentral - 0.5) > 0.2)
            {
                //Warn about lever that's far away from the physical center
                Box(@"Your reverser is about 50% off from the computed center.
If the lever is really in the middle, something might be wrong with your controller.
This tool will try to compensate for this, but it might be a bit hard to operate the cutoff in a steam train.");
            }
            if (Box(@"OPTIONAL: Move the reverser fully back and forth a few times, then stop in the middle.
Don't try too hard to stop at the exact same position.
This calibration step creates a bit of wiggle room to center the reverser/cutoff easier in locomotives where the reverser doesn't has distinctive steps.

When done, click [OK].
If you don't want this feature, click [Cancel]", true))
            {
                S.ReverserDeviation = Math.Abs(RD.Reverser - S.ReverserCentral);
                if (S.ReverserDeviation > 0.3)
                {
                    Box(@"Deviation is over 30% in either direction. This causes 60% of the entire lever movement area to be considered neutral.
Close this dialog, then play around with the lever a bit. If you're unhappy, you can run the calibration again.");
                }
                else
                {
                    Box(@"Calibration is complete.
Close this dialog, then play around with the lever a bit. If you're unhappy, you can run the calibration again.

If you've done this correctly, you should be able to reliably stop at zero percent even when the lever is moved quickly.");
                }
            }
            else
            {
                S.ReverserDeviation = 0.0;
                Box("Calibration is complete.");
            }
            Display.SetText("");
        }

        private bool Box(string Message, bool CanCancel = false)
        {
            return MessageBox.Show(Message, Text, CanCancel ? MessageBoxButtons.OKCancel : MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK;
        }

        private void autoCal_Click(object sender, EventArgs e)
        {
            Display.SetText("CAL");
            Box("This lever is calibrated automatically. Just move it through the entire range a few times.");
            Display.SetText("");
        }

        private void btnCalibrateAutoBrake_Click(object sender, EventArgs e)
        {
            Display.SetText("CAL");
            Box("Move the auto brake to one end, then close this dialog.");
            Box("Move the auto brake to the other end, then close this dialog.");
            if (Box(@"Move the auto brake right on top of the notch that separates the emergency setting from the rest, then click [OK].
Click [Cancel] to disable this feature and use the full range in a regular fashion.", true))
            {
                S.AutoBrakeSplit = 1.0 - RD.AutoBrake;
                if (S.AutoBrakeSplit > 0.8)
                {
                    if (S.AutoBrakeSplit < 0.95)
                    {
                        Box("The automatic brake is now calibrated.");
                    }
                    else
                    {
                        Box(@"The split point seems a bit high.
Work the lever a bit and make sure it works the way you want it to.
You want to make sure that you can reliably set it to the emergency setting");
                    }
                }
                else
                {
                    Box(@"The calibrated value seems a bit low.
Work the lever a bit and make sure it works the way you want it to.
You do not want to accidentally set it to emergency");
                }
            }
            else
            {
                //The value doesn't matters, just has to be bigger than 1.
                S.AutoBrakeSplit = 1.5;
                Box("The automatic brake is now calibrated.");
            }
            Display.SetText("");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (S.IsFullyCalibrated())
            {
                Tools.SaveSettings(Program.AppPath, S);
                Box("Calibration data saved");
            }
            else
            {
                Box("Your device is not fully calibrated yet.");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (Box("You will have to calibrate again", true))
            {
                S = new Settings();
                S.ControllerId = CurrentDevice.Vid;
            }
        }

        private void btnCalThrottle_Click(object sender, EventArgs e)
        {
            Display.SetText("CAL");
            Box("Please move the throttle lever completely towards the end of the independent brake setting, then close this dialog.");
            Box("Please move the throttle lever completely towards the end of the throttle setting, then close this dialog.");
            Box("Please move the lever back until it hits the center pin but do not move it past it, then close this dialog.");
            S.ThrottleStop = RD.Throttle;
            Box("Please move the lever fully towards the independent brake setting, then back until it hits the center pin but do not move it past it, then close this dialog.");
            if (S.ThrottleStop <= RD.Throttle)
            {
                Box("The values are not possible. Please try again.");
            }
            else
            {
                S.DynamicBrakeStop = RD.Throttle;
                Box("The throttle lever is now calibrated");
            }
            Display.SetText("");
        }

        private void btnCalBailoff_Click(object sender, EventArgs e)
        {
            Display.SetText("CAL");
            Box(@"To calibrate the bailoff, push the independent brake to the side,
once in the released position and once in the fully applied position.");
            Display.SetText("");
        }
    }
}
