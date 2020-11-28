using System;
using System.Xml.Serialization;

namespace DVRailDriver
{
    [Serializable]
    public class LeverLimit
    {
        private byte _min, _max;

        [XmlAttribute]
        public byte Min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
                if (_min > _max)
                {
                    _max = _min;
                }
            }
        }

        [XmlAttribute]
        public byte Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
                if (_min > _max)
                {
                    _min = _max;
                }
            }
        }

        public LeverLimit()
        {
            _min = byte.MaxValue;
            _max = byte.MinValue;
        }

        public double Range(byte Value, bool Update)
        {
            Value = Evaluate(Value, Update);
            if (Min >= Max)
            {
                return 0.0;
            }
            return (double)(Value - Min) / (Max - Min);
        }

        public byte Evaluate(byte Value, bool Update)
        {
            if (Value >= Min && Value <= Max)
            {
                return Value;
            }
            if (Value < Min)
            {
                return Update ? Min = Value : Min;
            }
            if (Value > Max)
            {
                return Update ? Max = Value : Max;
            }
            return Value;
        }
    }
}
