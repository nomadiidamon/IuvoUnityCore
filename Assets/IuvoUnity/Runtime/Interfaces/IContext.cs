using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace Interfaces
    {
        interface IContext : IDataStructBase , IuvoInterfaceBase
        {
            public void Dispose();
        }
    }
}