using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface ICreator<T> : IuvoInterfaceBase where T : ICreatable
        {
            public abstract void Create(T creatable);
        }
        public interface ICreatable : IuvoInterfaceBase
        {
            public abstract void OnCreate();
        }


        public interface IDestroyer<T> : IuvoInterfaceBase where T : IDestructible
        {
            public abstract void Destroy(T destructible);
        }
        public interface IDestructible : IuvoInterfaceBase
        {
            public abstract void OnDestroy();
        }


        public interface IStartUp : IuvoInterfaceBase
        {
            public abstract void StartUp();
        }
        public interface IShutDown : IuvoInterfaceBase
        {
            public abstract void ShutDown();
        }
    }
}