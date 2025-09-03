using System;

namespace IuvoUnity
{
    namespace Interfaces
    {
            public interface IGenericCondition<T> : IStateACondition
            {
                T GetValue();
                bool Compare(T value);
            }
        
    }
}