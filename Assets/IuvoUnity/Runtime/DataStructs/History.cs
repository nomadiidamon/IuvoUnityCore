using IuvoUnity.BaseClasses;
using System.Collections.Generic;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public class History<T> : IDataStructBase
        {
            private Stack<T> _past = new Stack<T>();
            private Stack<T> _future = new Stack<T>();

            public void Push(T state)
            {
                _past.Push(state);
                _future.Clear();
            }

            public T Undo() => _past.Count > 0 ? _past.Pop() : default;
            public void Redo(T state) => _future.Push(state);
        }

    }
}