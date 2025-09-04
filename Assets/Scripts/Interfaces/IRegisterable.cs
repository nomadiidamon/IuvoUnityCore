using IuvoUnity.BaseClasses;
using UnityEngine;


namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface IRegister<T> : IuvoInterfaceBase
        {
            public void Register(T registerable);
        }
        public interface IRegisterable : IuvoInterfaceBase
        {
            void OnRegister();
        }


        public interface IUnregister<T> : IuvoInterfaceBase
        {
            public void Unregister(TagHandle unregisterable);
        }
        public interface IUnregisterable : IuvoInterfaceBase
        {
            void OnUnregister();
        }
    }
}

