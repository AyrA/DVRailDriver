using PIEHid32Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DVRailDriver
{
    public enum FlipSwitch
    {
        Idle = 0,
        Up = 1,
        Down = -1,
    }

    [Flags]
    public enum Directions
    {
        Idle = 0,
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8
    }

    public class RailDriver : PIEDataHandler, IDisposable
    {
        public event GenericEventHandler DataUpdate = delegate { };

        public PIEDevice Controller { get; private set; }

        public bool Calibrate { get; set; }

        public double Reverser { get; private set; }
        public double Throttle { get; private set; }
        public double AutoBrake { get; private set; }
        public double IndBrake { get; private set; }
        public double BailOff { get; private set; }
        public double Wiper { get; private set; }
        public double Lights { get; private set; }

        public FlipSwitch Horn { get; private set; }
        public FlipSwitch EStop { get; private set; }
        public FlipSwitch Range { get; private set; }

        public bool Alert { get; private set; }
        public bool Sand { get; private set; }
        public bool P { get; private set; }
        public bool Bell { get; private set; }

        public int[] TopRow { get; private set; }
        public int[] BottomRow { get; private set; }
        public FlipSwitch Zoom { get; private set; }
        public Directions DPad { get; private set; }

        private readonly LeverLimit
            ReverserLimit, ThrottleLimit,
            AutoBrakeLimit, IndBrakeLimit,
            BailOffLimit, WiperLimit,
            LightsLimit;
        private readonly bool owner;
        private readonly LeverLimit[] Ordered;

        public RailDriver(PIEDevice Device, Settings S, bool OwnsDevice)
        {
            owner = OwnsDevice;
            Controller = Device ?? throw new ArgumentNullException(nameof(Device));
            ReverserLimit = S.ReverserLimit;
            AutoBrakeLimit = S.AutoBrakeLimit;
            IndBrakeLimit = S.IndBrakeLimit;
            WiperLimit = S.WiperLimit;
            LightsLimit = S.LightsLimit;
            ThrottleLimit = S.ThrottleLimit;
            BailOffLimit = S.BailOffLimit;
            //The order in which these values appear in the HID data stream
            Ordered = new LeverLimit[]
            {
                null, //The data stream starts at the second byte, not the first
                ReverserLimit, ThrottleLimit,
                AutoBrakeLimit, IndBrakeLimit,
                BailOffLimit, WiperLimit,
                LightsLimit
            };
            Calibrate = Ordered.Skip(1).Any(m => m.Max <= m.Min);
            if (OwnsDevice)
            {
                Device.SetupInterface();
            }
            Device.SetDataCallback(this);
        }

        public void Dispose()
        {
            lock (this)
            {
                if (Controller != null)
                {
                    if (owner)
                    {
                        Controller.CloseInterface();
                    }
                    Controller = null;
                }
            }
        }

        private void SetAnalogLeverData(byte[] data)
        {
            Reverser = ReverserLimit.Range(data[1], Calibrate);
            Throttle = ThrottleLimit.Range(data[2], Calibrate);

            AutoBrake = AutoBrakeLimit.Range(data[3], Calibrate);
            IndBrake = IndBrakeLimit.Range(data[4], Calibrate);
            BailOff = BailOffLimit.Range(data[5], Calibrate);

            Wiper = WiperLimit.Range(data[6], Calibrate);
            Lights = LightsLimit.Range(data[7], Calibrate);
        }

        private void SetToggleSwitchData(byte[] data)
        {
            if ((data[13] & 0x4) != 0)
            {
                Horn = FlipSwitch.Up;
            }
            else if ((data[13] & 0x8) != 0)
            {
                Horn = FlipSwitch.Down;
            }
            else
            {
                Horn = FlipSwitch.Idle;
            }

            if ((data[12] & 0x10) != 0)
            {
                EStop = FlipSwitch.Up;
            }
            else if ((data[12] & 0x20) != 0)
            {
                EStop = FlipSwitch.Down;
            }
            else
            {
                EStop = FlipSwitch.Idle;
            }

            if ((data[12] & 0x4) != 0)
            {
                Range = FlipSwitch.Up;
            }
            else if ((data[12] & 0x8) != 0)
            {
                Range = FlipSwitch.Down;
            }
            else
            {
                Range = FlipSwitch.Idle;
            }
        }

        private void SetCabSwitchData(byte[] data)
        {
            Alert = (data[12] & 0x40) != 0;
            Sand = (data[12] & 0x80) != 0;
            P = (data[12] & 0x1) != 0;
            Bell = (data[12] & 0x02) != 0;
        }

        private void SetControlSwitchData(byte[] data)
        {
            List<int> Switches = new List<int>();
            if ((data[8] & 0x01) != 0) { Switches.Add(1); }
            if ((data[8] & 0x02) != 0) { Switches.Add(2); }
            if ((data[8] & 0x04) != 0) { Switches.Add(3); }
            if ((data[8] & 0x08) != 0) { Switches.Add(4); }
            if ((data[8] & 0x10) != 0) { Switches.Add(5); }
            if ((data[8] & 0x20) != 0) { Switches.Add(6); }
            if ((data[8] & 0x40) != 0) { Switches.Add(7); }
            if ((data[8] & 0x80) != 0) { Switches.Add(8); }

            if ((data[9] & 0x01) != 0) { Switches.Add(9); }
            if ((data[9] & 0x02) != 0) { Switches.Add(10); }
            if ((data[9] & 0x04) != 0) { Switches.Add(11); }
            if ((data[9] & 0x08) != 0) { Switches.Add(12); }
            if ((data[9] & 0x10) != 0) { Switches.Add(13); }
            if ((data[9] & 0x20) != 0) { Switches.Add(14); }

            TopRow = Switches.ToArray();
            Switches.Clear();

            if ((data[9] & 0x40) != 0) { Switches.Add(1); }
            if ((data[9] & 0x80) != 0) { Switches.Add(2); }

            if ((data[10] & 0x01) != 0) { Switches.Add(3); }
            if ((data[10] & 0x02) != 0) { Switches.Add(4); }
            if ((data[10] & 0x04) != 0) { Switches.Add(5); }
            if ((data[10] & 0x08) != 0) { Switches.Add(6); }
            if ((data[10] & 0x10) != 0) { Switches.Add(7); }
            if ((data[10] & 0x20) != 0) { Switches.Add(8); }
            if ((data[10] & 0x40) != 0) { Switches.Add(9); }
            if ((data[10] & 0x80) != 0) { Switches.Add(10); }

            if ((data[11] & 0x01) != 0) { Switches.Add(11); }
            if ((data[11] & 0x02) != 0) { Switches.Add(12); }
            if ((data[11] & 0x04) != 0) { Switches.Add(13); }
            if ((data[11] & 0x08) != 0) { Switches.Add(14); }

            BottomRow = Switches.ToArray();

            if ((data[11] & 0x10) != 0)
            {
                Zoom = FlipSwitch.Up;
            }
            else if ((data[11] & 0x20) != 0)
            {
                Zoom = FlipSwitch.Down;
            }
            else
            {
                Zoom = FlipSwitch.Idle;
            }

            DPad = Directions.Idle;
            if ((data[11] & 0x40) != 0)
            {
                DPad |= Directions.Up;
            }
            if ((data[11] & 0x80) != 0)
            {
                DPad |= Directions.Right;
            }
            if ((data[12] & 0x01) != 0)
            {
                DPad |= Directions.Down;
            }
            if ((data[12] & 0x02) != 0)
            {
                DPad |= Directions.Left;
            }
        }

        public void HandlePIEHidData(byte[] data, PIEDevice source, int error)
        {
            if (error == 0)
            {
                Tools.PrintHex(data);

                SetAnalogLeverData(data);
                SetToggleSwitchData(data);
                SetCabSwitchData(data);
                SetControlSwitchData(data);

                DataUpdate(this);
            }
        }
    }
}
