using IuvoUnity.BaseClasses;
using System.Collections.Generic;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public abstract class ClampedValue<T> : IDataStructBase where T : struct
        {
            public Range<T> Range;

            protected T _value;

            public virtual T Value
            {
                get => _value;
                set => _value = Range.Clamp(value);
            }

            protected ClampedValue(Range<T> range, T initialValue)
            {
                Range = range;
                _value = Range.Clamp(initialValue);
            }

            public bool IsAtMin => EqualityComparer<T>.Default.Equals(_value, Range.Min);
            public bool IsAtMax => EqualityComparer<T>.Default.Equals(_value, Range.Max);
            public bool IsWithinRange => Range.Contains(_value);

            public override string ToString() => $"[{Range.Min}...{_value}...{Range.Max}]";

        }

        [System.Serializable]
        public class ClampedFloat : ClampedValue<float>
        {
            public ClampedFloat(RangeF range, float initialValue) : base(range, initialValue) { }

        }

        [System.Serializable]
        public class ClampedInt : ClampedValue<int>
        {
            public ClampedInt(RangeInt range, int initialValue) : base(range, initialValue) { }

        }
    }
}
