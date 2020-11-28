using PIEHid32Net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DVRailDriver
{
    public partial class frmMain : Form
    {
        private UdpClient client;
        private PIEDevice Controller;
        private RailDriver RD;
        private Settings S;
        private LED Display;

        public frmMain()
        {
            InitializeComponent();
            Controller = Tools.GetFirstController();
            if (Controller == null)
            {
                MessageBox.Show("No RailDriver is connected to this system");
                Environment.Exit(1);
            }
            S = Tools.GetSettings(Program.AppPath);
            btnRun.Enabled = S.IsFullyCalibrated();
            Controller.suppressDuplicateReports = true;
            Controller.SetupInterface();
            RD = new RailDriver(Controller, S, false);
            Display = new LED(Controller, false);
            RD.DataUpdate += RD_DataUpdate;
        }

        private void RD_DataUpdate(object sender)
        {
            if (client != null)
            {
                lock (client)
                {
                    var Rev = Tools.GetReverserState(RD.Reverser, S.ReverserCentral, S.ReverserDeviation);
                    if (Rev > 0.0)
                    {
                        Rev = Wiggle(Rev);
                    }
                    else
                    {
                        Rev = Wiggle(Rev * -1.0) * -1.0;
                    }
                    SendLine("HORN=" + (RD.Horn == FlipSwitch.Idle ? "0" : "1"));
                    SendLine("THROTTLE=" + Wiggle(Math.Max(0, Tools.GetThrottleState(RD.Throttle, S.ThrottleStop, S.DynamicBrakeStop))));
                    SendLine("REVERSER=" + Rev);
                    SendLine("TRAINBRAKE=" + Wiggle(1.0 - Tools.GetAutoBrakeState(RD.AutoBrake, S.AutoBrakeSplit)));
                    if (RD.BailOff < 0.5)
                    {
                        SendLine("INDEPENDENTBRAKE=" + Wiggle(1.0 - RD.IndBrake));
                    }
                }
            }
        }

        private static double Wiggle(double value, double roundValue = 0.07)
        {
            if (value >= 1.0 - roundValue)
            {
                return 1.0;
            }
            if (value <= roundValue)
            {
                return 0.0;
            }
            return value;
        }

        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                using (var frm = new frmCalibrate(Controller))
                {
                    frm.ShowDialog();
                }
                S = Tools.GetSettings(Program.AppPath);
                btnRun.Enabled = S.IsFullyCalibrated();
                RD = new RailDriver(Controller, S, false);
                Display = new LED(Controller, false);
                RD.DataUpdate += RD_DataUpdate;
            }
            else
            {
                MessageBox.Show("Client already connected");
            }
        }

        private string[] SendLine(string Line)
        {
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
            var data = Encoding.UTF8.GetBytes(Line);
            client.Send(data, data.Length, new IPEndPoint(IPAddress.Loopback, 31337));
            data = client.Receive(ref remoteEP);
            return Encoding.UTF8.GetString(data).Split('\0');
        }

        private void SetStatus(string[] Lines)
        {
            var Settings = ParseLines(Lines);
            if (Settings.ContainsKey("SPEED"))
            {
                Display.SetNumber(Math.Round(Math.Abs(double.Parse(Settings["SPEED"]))));
            }
            else
            {
                Display.SetText("OFF");
            }
        }

        private static Dictionary<string, string> ParseLines(string[] Lines)
        {
            var Ret = new Dictionary<string, string>();
            foreach (var L in Lines)
            {
                var i = L.IndexOf('=');
                if (i >= 0 && i < L.Length - 1)
                {
                    Ret[L.Substring(0, i)] = L.Substring(i + 1);
                }
            }
            return Ret;
        }

        private void InfoLoop()
        {
            while (client != null)
            {
                lock (client)
                {
                    SetStatus(SendLine("INFO"));
                }
                Thread.Sleep(500);
            }
            Environment.Exit(0);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            Thread T = new Thread(InfoLoop)
            {
                Name = "Socket Info Handler",
                IsBackground = true
            };
            using (client = new UdpClient())
            {
                client.Client.Bind(new IPEndPoint(IPAddress.Loopback, 46322));
                T.Start();
                MessageBox.Show("Click OK to stop");
                client.Close();
            }
            client = null;
            T.Join();
        }
    }
}
