using PIEHid32Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace DVRailDriver
{
    [Flags]
    public enum Segment : byte
    {
        None = 0,
        Top = 1,
        Middle = 64,
        Bottom = 8,
        TopLeft = 32,
        BottomLeft = 16,
        TopRight = 2,
        BottomRight = 4,
        Dot = 128,
        Left = TopLeft | BottomLeft,
        Center = Top | Middle | Bottom,
        Right = TopRight | BottomRight,
        All = Left | Center | Right
    }

    public class LED : IDisposable
    {
        public event GenericEventHandler MarqueeEnd = delegate { };

        private static readonly Dictionary<char, Segment> CharMap;
        private static readonly List<Segment> LoadAnimation;

        private int interval = 0;
        public PIEDevice dev { get; private set; }
        public int MarqueeDelay
        {
            get
            {
                return interval;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                interval = value / 100 * 100;
            }
        }
        public bool MarqueeRepeat { get; set; }

        private readonly bool DeviceOwner;
        private Segment[] MarqueeCode;
        private int MarqueeOffset;
        private bool UseLoader = false;

        static LED()
        {
            LoadAnimation = new List<Segment>();
            #region Fill Loader
            LoadAnimation.Add(Segment.Top);
            LoadAnimation.Add(Segment.TopRight);
            LoadAnimation.Add(Segment.Middle);
            LoadAnimation.Add(Segment.BottomLeft);
            LoadAnimation.Add(Segment.Bottom);
            LoadAnimation.Add(Segment.BottomRight);
            LoadAnimation.Add(Segment.Middle);
            LoadAnimation.Add(Segment.TopLeft);
            #endregion
            CharMap = new Dictionary<char, Segment>();
            #region Fill Map
            CharMap['!'] = Segment.TopRight | Segment.Dot;
            CharMap['"'] = Segment.TopLeft | Segment.TopRight;
            CharMap['\''] = Segment.TopLeft;
            CharMap['('] = Segment.Top | Segment.Bottom | Segment.Left;
            CharMap[')'] = Segment.Top | Segment.Bottom | Segment.Right;
            CharMap[','] = Segment.Dot;
            CharMap['-'] = Segment.Middle;
            CharMap['.'] = Segment.Dot;
            CharMap['0'] = Segment.Top | Segment.Bottom | Segment.Left | Segment.Right;
            CharMap['1'] = Segment.Right;
            CharMap['2'] = Segment.Center | Segment.TopRight | Segment.BottomLeft;
            CharMap['3'] = Segment.Center | Segment.Right;
            CharMap['4'] = Segment.TopLeft | Segment.Middle | Segment.Right;
            CharMap['5'] = Segment.Center | Segment.TopLeft | Segment.BottomRight;
            CharMap['6'] = Segment.Center | Segment.Left | Segment.BottomRight;
            CharMap['7'] = Segment.Top | Segment.Right;
            CharMap['8'] = Segment.All;
            CharMap['9'] = Segment.Center | Segment.Right | Segment.TopLeft;
            CharMap['='] = Segment.Bottom | Segment.Middle;
            CharMap['?'] = Segment.Top | Segment.TopRight | Segment.Dot;
            CharMap['A'] = Segment.Left | Segment.Right | Segment.Top | Segment.Middle;
            CharMap['B'] = Segment.Left | Segment.Middle | Segment.BottomRight | Segment.Bottom;
            CharMap['C'] = CharMap['('];
            CharMap['D'] = Segment.Right | Segment.BottomLeft | Segment.Middle | Segment.Bottom;
            CharMap['E'] = Segment.Center | Segment.Left;
            CharMap['F'] = Segment.Left | Segment.Middle | Segment.Top;
            CharMap['G'] = Segment.Left | Segment.Top | Segment.Bottom | Segment.BottomRight;
            CharMap['H'] = Segment.Left | Segment.Middle | Segment.Right;
            CharMap['I'] = Segment.Left;
            CharMap['J'] = Segment.Right | Segment.Bottom;
            CharMap['L'] = Segment.Left | Segment.Bottom;
            CharMap['N'] = Segment.BottomLeft | Segment.BottomRight | Segment.Middle;
            CharMap['O'] = Segment.Bottom | Segment.Middle | Segment.BottomLeft | Segment.BottomRight;
            CharMap['P'] = Segment.Left | Segment.Top | Segment.Middle | Segment.TopRight;
            CharMap['Q'] = Segment.TopLeft | Segment.Top | Segment.Middle | Segment.Right;
            CharMap['R'] = Segment.BottomLeft | Segment.Middle;
            CharMap['S'] = CharMap['5'];
            CharMap['T'] = Segment.Left | Segment.Bottom;
            CharMap['U'] = Segment.None;
            CharMap['Y'] = Segment.Right | Segment.Middle | Segment.TopLeft | Segment.Bottom;
            CharMap['Z'] = CharMap['2'];
            CharMap['_'] = Segment.Bottom;
            #endregion
        }

        public LED(PIEDevice Device, bool OwnsDevice)
        {
            MarqueeDelay = 500;
            dev = Device;
            DeviceOwner = OwnsDevice;
            if (dev == null)
            {
                throw new ArgumentNullException(nameof(Device));
            }
            if (OwnsDevice)
            {
                dev.SetupInterface();
            }
            new Thread(MarqueeThread) { IsBackground = true }.Start();
        }

        public void ClearMarquee()
        {
            lock (this)
            {
                MarqueeCode = null;
                MarqueeRepeat = false;
                MarqueeOffset = 0;
                UseLoader = false;
            }
        }

        public void SetMarquee(string Text, bool Repeat)
        {
            SetMarquee(MergeDot(Text.Select(GetLEDCode)), Repeat);
        }

        public void SetMarquee(IEnumerable<Segment> Codes, bool Repeat)
        {
            lock (this)
            {
                var Text = Codes.ToArray();
                ClearMarquee();
                if (Text.Length > 3)
                {
                    //Prefix with two spaces so it comes in from the right
                    //rather than starting with an already full display
                    MarqueeCode = new Segment[] { Segment.None, Segment.None }.Concat(Text).ToArray();
                    MarqueeOffset = 0;
                    MarqueeRepeat = Repeat;
                }
                else
                {
                    if (Text.Length > 0)
                    {
                        //Pad to 3 elements
                        Text = Text
                            .Concat(new Segment[] { Segment.None, Segment.None })
                            .Take(3)
                            .ToArray();
                        SetLED(Text[0], Text[1], Text[2]);
                    }
                }
            }
        }

        public static Segment GetLEDCode(char c)
        {

            return CharMap.ContainsKey(c) ? CharMap[c] : Segment.None;
        }

        public bool ClearDisplay()
        {
            return SetLED(Segment.None, Segment.None, Segment.None);
        }

        public bool SetLED(Segment First, Segment Center, Segment Last)
        {
            int result;
            var data = new byte[dev.WriteLength];
            data[1] = 134;
            data[2] = (byte)Last;
            data[3] = (byte)Center;
            data[4] = (byte)First;
            do
            {
                result = dev.WriteData(data);
            } while (result == 404);
            return result == 0;
        }

        public bool SetText(string Text)
        {
            Debug.Print("Set LED: " + Text);
            if (string.IsNullOrEmpty(Text))
            {
                return ClearDisplay();
            }
            var Codes = MergeDot(Text
                .Select(GetLEDCode))
                .Concat(new Segment[] { Segment.None, Segment.None, Segment.None })
                .ToArray();
            return SetLED(Codes[0], Codes[1], Codes[2]);
        }

        public bool SetNumber(double d)
        {
            if (d < -99.0)
            {
                return SetNumber(-99);
            }
            if (d > 999.0)
            {
                return SetNumber(999);
            }
            if (double.IsNaN(d))
            {
                return SetText("NAN");
            }
            if (d <= -10)
            {
                return SetText(Math.Round(d).ToString());
            }
            if (d >= 100)
            {
                return SetText(Math.Round(d).ToString());
            }
            return SetText(Math.Round(d, Math.Abs(d) < 10.0 ? 2 : 1).ToString().PadLeft(3));
        }

        public void Loader(bool Repeat)
        {
            MarqueeDelay = 100;
            MarqueeCode = LoadAnimation.ToArray();
            MarqueeOffset = 0;
            MarqueeRepeat = Repeat;
            UseLoader = true;
        }

        public void EndLoader()
        {
            MarqueeCode = null;
            UseLoader = false;
            ClearDisplay();
        }

        public void Dispose()
        {
            lock (this)
            {
                if (dev != null)
                {
                    ClearDisplay();
                    if (DeviceOwner)
                    {
                        dev.CloseInterface();
                    }
                    dev = null;
                }
            }
        }

        private Segment[] MergeDot(IEnumerable<Segment> Segments)
        {
            var Ret = new List<Segment>();
            foreach (var S in Segments)
            {
                //Add the new character if either of these conditions are true:
                //- List is empty
                //- Character is not a sole dot
                //- Last entry in list already has a dot
                if (Ret.Count == 0 || S != Segment.Dot || Ret[Ret.Count - 1].HasFlag(Segment.Dot))
                {
                    Ret.Add(S);
                }
                else
                {
                    Ret[Ret.Count - 1] |= Segment.Dot;
                }
            }
            return Ret.ToArray();
        }

        private void MarqueeThread()
        {
            PIEDevice Device;
            while (dev != null)
            {
                Device = dev;
                if (Device == null)
                {
                    Debug.Print("Marquee Thread exited");
                    return;
                }
                var Text = MarqueeCode;
                if (Text != null)
                {
                    if (UseLoader)
                    {
                        SetLED(Text[MarqueeOffset], Text[MarqueeOffset], Text[MarqueeOffset]);
                    }
                    else
                    {
                        //Get up to 3 characters from the current marquee position
                        var Letters = Text
                            .Skip(MarqueeOffset)
                            .Take(3)
                            .ToArray();
                        //If less than 3 available, get as many as needed from the start
                        if (Letters.Length < 3)
                        {
                            Letters = Letters.Concat(Text.Take(3 - Letters.Length)).ToArray();
                        }
                        SetLED(Letters[0], Letters[1], Letters[2]);
                    }
                    if (++MarqueeOffset >= Text.Length)
                    {
                        if (MarqueeRepeat)
                        {
                            MarqueeOffset = 0;
                        }
                        else
                        {
                            MarqueeCode = null;
                        }
                        //Do not block the current thread with this event
                        new Thread(delegate ()
                        {
                            MarqueeEnd(this);
                        }).Start();
                    }
                }
                int delay = interval / 100;
                for (var i = 0; i < delay && dev != null; i++)
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
