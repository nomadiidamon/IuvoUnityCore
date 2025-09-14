using IuvoUnity.BaseClasses;


namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface IConfigure<T> : IuvoInterfaceBase where T : IConfigurable
        {
            public abstract void Configure(T configurable);
        }
        public interface IConfigurable : IuvoInterfaceBase
        {
            public bool Configured { get; set; }
            public abstract void OnConfigure();
        }


        public interface IReconfigure<T> : IuvoInterfaceBase
        {
            public abstract void Reconfigure(T reconfigurable);
        }
        public interface IReconfigurable : IuvoInterfaceBase
        {
            public bool Reconfigured { get; set; }
            public abstract void OnReconfigure();
        }
    }
}


