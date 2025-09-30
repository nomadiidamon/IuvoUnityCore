using IuvoUnity.BaseClasses;
using System;

namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface ICloneable<T> : IuvoInterfaceBase
        {
            //Return a perfect copy of the object
            T Clone();
        }

    }
}