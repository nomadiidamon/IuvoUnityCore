using UnityEngine;
using IuvoUnity.BaseClasses;
using System.Collections.Generic;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public abstract class ClampedData<T> : IDataStructBase where T : struct
        {
            public Range<T> Range;

            protected T _value;

            public virtual T Value
            {
                get => _value;
                set => _value = Range.Clamp(value);
            }

            protected ClampedData(Range<T> range, T initialValue)
            {
                Range = range;
                _value = Range.Clamp(initialValue);
            }

            public virtual bool IsAtMin => EqualityComparer<T>.Default.Equals(_value, Range.Min);
            public virtual bool IsAtMax => EqualityComparer<T>.Default.Equals(_value, Range.Max);
            public virtual bool IsWithinRange => Range.Contains(_value);
            public virtual void SetToMin() => _value = Range.Min;
            public virtual void SetToMax() => _value = Range.Max;
            public virtual void SetToRandom() => _value = Range.RandomValue();
            public abstract void Increment(T amount);
            public abstract void Decrement(T amount);
            public virtual void Clamp() => _value = Range.Clamp(_value);

            public override string ToString() => $"[{Range.Min}...{_value}...{Range.Max}]";

        }

        [System.Serializable]
        public class ClampedFloat : ClampedData<float>
        {
            public ClampedFloat(RangeF range, float initialValue) : base(range, initialValue) { }

            public override bool IsAtMin
            {
                get
                {
                    // Use a tolerance for floating-point comparison
                    float tolerance = 0.0001f;
                    return Mathf.Abs(_value - Range.Min) < tolerance;
                }
            }
            public override bool IsAtMax
            {
                get
                {
                    // Use a tolerance for floating-point comparison
                    float tolerance = 0.0001f;
                    return Mathf.Abs(_value - Range.Max) < tolerance;
                }
            }
            public override bool IsWithinRange
            {
                get
                {
                    // Use a tolerance for floating-point comparison
                    float tolerance = 0.0001f;
                    return (_value > Range.Min - tolerance) && (_value < Range.Max + tolerance);
                }
            }
            public override void Increment(float amount)
            {
                Value += amount;
            }
            public override void Decrement(float amount)
            {
                Value -= amount;
            }

        }

        [System.Serializable]
        public class ClampedInt : ClampedData<int>
        {
            public ClampedInt(RangeInt range, int initialValue) : base(range, initialValue) { }
            public override bool IsAtMin => _value == Range.Min;
            public override bool IsAtMax => _value == Range.Max;
            public override bool IsWithinRange => Range.Contains(_value);
            public override void Increment(int amount)
            {
                Value += amount;
            }
            public override void Decrement(int amount)
            {
                Value -= amount;
            }

        }

        [System.Serializable]
        public class ClampedVector2 : ClampedData<Vector2>
        {
            public ClampedVector2(Vector2 rangeMin, Vector2 rangeMax, Vector2 initialValue) : base(new RangeVector2(rangeMin, rangeMax), initialValue) { }
            public override void Increment(Vector2 amount)
            {
                Value += amount;
            }
            public override void Decrement(Vector2 amount)
            {
                Value -= amount;
            }
            public override bool IsAtMin => Mathf.Approximately(_value.x, Range.Min.x) && Mathf.Approximately(_value.y, Range.Min.y);
            public override bool IsAtMax => Mathf.Approximately(_value.x, Range.Max.x) && Mathf.Approximately(_value.y, Range.Max.y);
            public override bool IsWithinRange => Range.Contains(_value);
        }

        [System.Serializable]
        public class ClampedVector3 : ClampedData<Vector3>
        {
            public ClampedVector3(Vector3 rangeMin, Vector3 rangeMax, Vector3 initialValue) : base(new RangeVector3(rangeMin, rangeMax), initialValue) { }
            public override void Increment(Vector3 amount)
            {
                Value += amount;
            }
            public override void Decrement(Vector3 amount)
            {
                Value -= amount;
            }
            public override bool IsAtMin => Mathf.Approximately(_value.x, Range.Min.x) && Mathf.Approximately(_value.y, Range.Min.y) && Mathf.Approximately(_value.z, Range.Min.z);
            public override bool IsAtMax => Mathf.Approximately(_value.x, Range.Max.x) && Mathf.Approximately(_value.y, Range.Max.y) && Mathf.Approximately(_value.z, Range.Max.z);
            public override bool IsWithinRange => Range.Contains(_value);
        }
    }
}
