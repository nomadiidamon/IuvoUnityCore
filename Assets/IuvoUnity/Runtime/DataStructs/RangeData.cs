using UnityEngine;
using IuvoUnity.BaseClasses;
using System.Collections.Generic;

namespace IuvoUnity
{

    namespace DataStructs
    {
        [System.Serializable]
        public abstract class Range<T> : IDataStructBase where T : struct
        {
            public abstract T Min { get; set; }
            public abstract T Max { get; set; }

            public abstract T RandomValue();

            public abstract T Clamp(T value);

            public bool Contains(T value)
            {
                if (Comparer<T>.Default.Compare(value, Min) >= 0 && Comparer<T>.Default.Compare(value, Max) <= 0)
                {
                    return true;
                }
                return false;
            }
            public override string ToString() => $"[{Min} .. {Max}]";
        }

        [System.Serializable]
        public class RangeF : Range<float>
        {
            public float min;
            public float max;

            public RangeF(float min, float max)
            {

                this.min = min;
                this.max = max;
            }

            public override float Min
            {
                get => min;
                set => min = value;
            }

            public override float Max
            {
                get => max;
                set => max = value;
            }

            public override float RandomValue()
            {
                return UnityEngine.Random.Range(min, max);
            }

            public override float Clamp(float value)
            {
                return Mathf.Clamp(value, min, max);
            }

            public float Midpoint => (min + max) * 0.5f;
            public float Lerp(float t) => Mathf.Lerp(min, max, t);
        }

        [System.Serializable]
        public class RangeInt : Range<int>
        {
            public int min;
            public int max;

            public RangeInt(int min, int max)
            {
                this.min = min;
                this.max = max;
            }

            public override int Min
            {
                get => min;
                set => min = value;
            }

            public override int Max
            {
                get => max;
                set => max = value;
            }

            public override int RandomValue()
            {
                return UnityEngine.Random.Range(min, max + 1); // Inclusive of max
            }

            public override int Clamp(int value)
            {
                return Mathf.Clamp(value, min, max);
            }

            public int Midpoint => (min + max) / 2;
        }

        [System.Serializable]
        public class RangeVector2 : Range<Vector2>
        {
            public Vector2 min;
            public Vector2 max;

            public RangeVector2(Vector2 min, Vector2 max)
            {
                this.min = min;
                this.max = max;
            }

            public override Vector2 Min
            {
                get => min;
                set => min = value;
            }

            public override Vector2 Max
            {
                get => max;
                set => max = value;
            }

            public override Vector2 RandomValue()
            {
                return new Vector2(
                    UnityEngine.Random.Range(min.x, max.x),
                    UnityEngine.Random.Range(min.y, max.y)
                );
            }

            public override Vector2 Clamp(Vector2 value)
            {
                return new Vector2(
                    Mathf.Clamp(value.x, min.x, max.x),
                    Mathf.Clamp(value.y, min.y, max.y)
                );
            }
        }

        [System.Serializable]
        public class RangeVector3 : Range<Vector3>
        {
            public Vector3 min;
            public Vector3 max;

            public RangeVector3(Vector3 min, Vector3 max)
            {
                this.min = min;
                this.max = max;
            }

            public override Vector3 Min
            {
                get => min;
                set => min = value;
            }

            public override Vector3 Max
            {
                get => max;
                set => max = value;
            }

            public override Vector3 RandomValue()
            {
                return new Vector3(
                    UnityEngine.Random.Range(min.x, max.x),
                    UnityEngine.Random.Range(min.y, max.y),
                    UnityEngine.Random.Range(min.z, max.z)
                );
            }

            public override Vector3 Clamp(Vector3 value)
            {
                return new Vector3(
                    Mathf.Clamp(value.x, min.x, max.x),
                    Mathf.Clamp(value.y, min.y, max.y),
                    Mathf.Clamp(value.z, min.z, max.z)
                );
            }
        }


    }
}