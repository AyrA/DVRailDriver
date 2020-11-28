using System;
using System.Linq;

namespace DVRailDriver
{
    [Serializable]
    public class Settings
    {
        /// <summary>
        /// Neutral point of the reverser lever
        /// </summary>
        public double ReverserCentral { get; set; }

        /// <summary>
        /// Extension of the neutral plane in both directions
        /// </summary>
        public double ReverserDeviation { get; set; }

        /// <summary>
        /// Point where brake power changes to emergency
        /// </summary>
        public double AutoBrakeSplit { get; set; }

        /// <summary>
        /// Zero point for the throttle
        /// </summary>
        public double ThrottleStop { get; set; }

        /// <summary>
        /// Zero point for the dynamic brake
        /// </summary>
        public double DynamicBrakeStop { get; set; }

        /// <summary>
        /// ID of the RailDriver controller
        /// </summary>
        public int ControllerId { get; set; }

        public LeverLimit ReverserLimit { get; set; }
        public LeverLimit ThrottleLimit { get; set; }
        public LeverLimit AutoBrakeLimit { get; set; }
        public LeverLimit IndBrakeLimit { get; set; }
        public LeverLimit BailOffLimit { get; set; }
        public LeverLimit WiperLimit { get; set; }
        public LeverLimit LightsLimit { get; set; }

        public bool IsFullyCalibrated() {
            var Limits = new LeverLimit[]
            {
                ReverserLimit, ThrottleLimit,
                AutoBrakeLimit, IndBrakeLimit,
                BailOffLimit, WiperLimit,
                LightsLimit
            };
            return Limits.All(m => m.Max > m.Min);
        }

        public Settings()
        {
            ReverserCentral = 0.5;
            ReverserDeviation = 0.1;
            AutoBrakeSplit = 0.9;
            ThrottleStop = 0.6;
            DynamicBrakeStop = 0.4;
            ReverserLimit = new LeverLimit();
            ThrottleLimit = new LeverLimit();
            AutoBrakeLimit = new LeverLimit();
            IndBrakeLimit = new LeverLimit();
            BailOffLimit = new LeverLimit();
            WiperLimit = new LeverLimit();
            LightsLimit = new LeverLimit();
        }
    }
}
