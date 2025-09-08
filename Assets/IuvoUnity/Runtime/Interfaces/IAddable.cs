using IuvoUnity.BaseClasses;


namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface IAdd<T> : IuvoInterfaceBase where T : IAddable
        {
            public abstract void Add(T addable);
        }
        public interface IAddable : IuvoInterfaceBase
        {
            public abstract void OnAdd();
        }

        public interface IRemove<T> : IuvoInterfaceBase where T : IRemovable
        {
            public abstract void Remove(T removable);
        }
        public interface IRemovable : IuvoInterfaceBase
        {
            public abstract void OnRemove();
        }
    }
}
