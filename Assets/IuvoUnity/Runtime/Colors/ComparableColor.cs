
using System;
using UnityEngine;

namespace IuvoUnity
{
    namespace Colors
    {
        [System.Serializable]
        public class ComparableColor : IComparable<ComparableColor>
        {
            public Color color;
            public ColorComparisonMode mode = ColorComparisonMode.Brightness;

            public enum ColorComparisonMode
            {
                Brightness,
                Grayscale,
                Red,
                Green,
                Blue,
                Alpha
            }


            public ComparableColor(Color color, ColorComparisonMode compMode = ColorComparisonMode.Brightness)
            {
                this.color = color;
                this.mode = compMode;
            }

            public ComparableColor(Color32 color, ColorComparisonMode compMode = ColorComparisonMode.Brightness)
            {
                this.color = (Color)color;
                this.mode = compMode;
            }

            private float GetComparableValue()
            {
                switch (mode)
                {
                    case ColorComparisonMode.Brightness:
                        return (color.r + color.g + color.b) / 3f;
                    case ColorComparisonMode.Grayscale:
                        return color.grayscale;
                    case ColorComparisonMode.Red:
                        return color.r;
                    case ColorComparisonMode.Green:
                        return color.g;
                    case ColorComparisonMode.Blue:
                        return color.b;
                    case ColorComparisonMode.Alpha:
                        return color.a;
                    default:
                        return 0f;
                }
            }

            public int CompareTo(ComparableColor other)
            {
                if (other == null) return 1;
                float aVal = this.GetComparableValue();
                float bVal = other.GetComparableValue();
                return aVal.CompareTo(bVal);
            }

            public static bool operator >(ComparableColor a, ComparableColor b)
            {

                return a.GetComparableValue() > b.GetComparableValue();
            }

            public static bool operator <(ComparableColor a, ComparableColor b)
            {

                return a.GetComparableValue() < b.GetComparableValue();
            }

            public static bool operator >=(ComparableColor a, ComparableColor b)
            {

                return a.GetComparableValue() >= b.GetComparableValue();
            }

            public static bool operator <=(ComparableColor a, ComparableColor b)
            {

                return a.GetComparableValue() <= b.GetComparableValue();
            }

            public static bool operator ==(ComparableColor a, ComparableColor b)
            {
                if (ReferenceEquals(a, b)) return true;
                if (a is null || b is null) return false;
                return Mathf.Approximately(a.GetComparableValue(), b.GetComparableValue());
            }

            public static bool operator !=(ComparableColor a, ComparableColor b)
            {
                return !(a == b);
            }

            public override bool Equals(object obj)
            {
                if (obj is ComparableColor other)
                    return this == other;
                return false;
            }

            public override int GetHashCode()
            {
                return color.GetHashCode();
            }
        }


    }
}
