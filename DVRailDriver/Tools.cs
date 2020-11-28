using PIEHid32Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace DVRailDriver
{
    /// <summary>
    /// Generic event handler that takes only the sender as parameter
    /// </summary>
    /// <param name="sender">Event sender</param>
    public delegate void GenericEventHandler(object sender);

    public static class Tools
    {
        public const string SETTINGS_FILE = "settingx.xml";

        public static PIEDevice GetFirstController(int DevId = 0)
        {
             var devices = PIEDevice
                .EnumeratePIE()
                .Where(m => m.HidUsagePage == 0xC && m.Pid == 210)
                .ToArray();
            if (DevId != 0)
            {
                return devices.FirstOrDefault(m => m.Vid == DevId);
            }
            return devices.FirstOrDefault();
        }

        public static Settings GetSettings(string AppPath)
        {
            try
            {
                var Loader = new XmlSerializer(typeof(Settings));
                using (var FS = File.OpenRead(Path.Combine(AppPath, SETTINGS_FILE)))
                {
                    return (Settings)Loader.Deserialize(FS);
                }
            }
            catch
            {
                return new Settings();
            }
        }

        public static void SaveSettings(string AppPath, Settings S)
        {
            var Writer = new XmlSerializer(typeof(Settings));
            using (var FS = File.Create(Path.Combine(AppPath, SETTINGS_FILE)))
            {
                Writer.Serialize(FS, S);
            }
        }


        /// <summary>
        /// Dumps hex data onto the debug output
        /// </summary>
        /// <param name="data">HEX data</param>
        public static void PrintHex(IEnumerable<byte> data)
        {
            Debug.Print(string.Join(" ", data.Select(m => m.ToString("X2"))));
        }

        public static double GetThrottleState(double RawValue, double ThrottleStop, double BrakeStop)
        {
            if (RawValue >= ThrottleStop)
            {
                //Throttle
                return (RawValue - ThrottleStop) / (1.0 - ThrottleStop);
            }
            if (RawValue <= BrakeStop)
            {
                //Brake
                return RawValue / BrakeStop -1.0;
            }
            //Dead zone
            return 0.0;
        }

        /// <summary>
        /// Converts a raw reverser value into the range from -1 to +1 as needed by DV
        /// </summary>
        /// <param name="RawValue">Raw reverser value</param>
        /// <param name="Neutral">Center point</param>
        /// <param name="NeutralDiff">Allowed deviation from center</param>
        /// <returns>DV conform reverser state.</returns>
        public static double GetReverserState(double RawValue, double Neutral, double NeutralDiff)
        {
            //Neutral plane
            var Min = Neutral - NeutralDiff;
            var Max = Neutral + NeutralDiff;
            //Force to zero if inside of neutral plane
            if (RawValue >= Min && RawValue <= Max)
            {
                return 0.0;
            }
            //Reverser is in "reverse" half
            if (RawValue > Max)
            {
                return (RawValue - Max) / (1.0 - Max) * -1.0;
            }
            //Reverser is in "forwards" half
            return ((RawValue / Min) - 1.0) * -1.0;
        }

        /// <summary>
        /// Converts a raw auto brake value into separation for emergency brake
        /// </summary>
        /// <param name="RawValue">Raw value</param>
        /// <param name="MaxRegular">Limit for E-Brake</param>
        /// <returns>0-0.9 for regular, 1.0 for E-Brake</returns>
        public static double GetAutoBrakeState(double RawValue, double MaxRegular)
        {
            if (RawValue > MaxRegular)
            {
                return 1.0;
            }
            return MaxRegular <= 1.0 ? Math.Min(RawValue / MaxRegular, 0.90) : RawValue;
        }

        /// <summary>
        /// Converts a raw analog value into a tristate switch value in the range 1-3
        /// </summary>
        /// <param name="RawValue">Raw lever value</param>
        /// <returns>Tristate value from 1 to 3</returns>
        public static int GetTripplePos(double RawValue)
        {
            if (RawValue <= 0.33)
            {
                return 1;
            }
            return RawValue <= 0.66 ? 2 : 3;
        }
    }
}
