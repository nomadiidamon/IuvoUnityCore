using IuvoUnity.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace IuvoUnity

{
    namespace _DataStructs
    {
        [System.Serializable]
        public class InputBuffer<T> : IDataStructBase
        {
            private Queue<BufferedInput<T>> _buffer;
            private int _maxSize;

            public InputBuffer(int size)
            {
                _maxSize = size;
                _buffer = new Queue<BufferedInput<T>>(size);
            }

            /// <summary>Adds a new input with the current time.</summary>
            public void Add(T input)
            {
                if (_buffer.Count >= _maxSize)
                    _buffer.Dequeue();

                _buffer.Enqueue(new BufferedInput<T>(input, Time.time));
            }

            /// <summary>Clears all inputs from the buffer.</summary>
            public void Clear() => _buffer.Clear();

            /// <summary>Returns all inputs as an array.</summary>
            public BufferedInput<T>[] ToArray() => _buffer.ToArray();

            /// <summary>Returns the most recently added input, or default if empty.</summary>
            public BufferedInput<T>? PeekLatest() => _buffer.Count > 0 ? _buffer.Last() : (BufferedInput<T>?)null;

            /// <summary>Returns the oldest input in the buffer, or default if empty.</summary>
            public BufferedInput<T>? PeekOldest() => _buffer.Count > 0 ? _buffer.Peek() : (BufferedInput<T>?)null;

            /// <summary>Checks if the buffer contains a value.</summary>
            public bool Contains(T input) => _buffer.Any(b => EqualityComparer<T>.Default.Equals(b.Value, input));

            /// <summary>Returns inputs newer than the given number of seconds ago.</summary>
            public IEnumerable<T> GetRecent(float withinSeconds)
            {
                float cutoff = Time.time - withinSeconds;
                return _buffer.Where(b => b.Timestamp >= cutoff).Select(b => b.Value);
            }

            /// <summary>Returns true if any input in the last N seconds matches the condition.</summary>
            public bool RecentAny(float withinSeconds, Func<T, bool> predicate)
            {
                float cutoff = Time.time - withinSeconds;
                return _buffer.Any(b => b.Timestamp >= cutoff && predicate(b.Value));
            }

            /// <summary>Returns how many buffered inputs there are.</summary>
            public int Count => _buffer.Count;

            /// <summary>Sets a new max size and trims excess if needed.</summary>
            public void SetMaxSize(int newSize)
            {
                _maxSize = newSize;
                while (_buffer.Count > _maxSize)
                    _buffer.Dequeue();
            }
        }

        [System.Serializable]
        public struct BufferedInput<T> : IDataStructBase
        {
            public T Value;
            public float Timestamp;

            public BufferedInput(T value, float timestamp)
            {
                Value = value;
                Timestamp = timestamp;
            }
        }
    }
}