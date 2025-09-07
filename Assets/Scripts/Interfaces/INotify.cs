using IuvoUnity.BaseClasses;


namespace IuvoUnity
{
    namespace Interfaces
    {

        public interface IChange : IuvoInterfaceBase
        {
            public abstract void Change();
            public abstract void OnChange();
        }

        public interface INotifyOfChange : IuvoInterfaceBase
        {
            public abstract void OnNotifyOfChange<T>() where T : IChange;
        }

        public interface INotifiable : IuvoInterfaceBase, INotifyOfChange, IChange
        {
            public abstract void NotifyOfChange<T>();
        }

    }
}

