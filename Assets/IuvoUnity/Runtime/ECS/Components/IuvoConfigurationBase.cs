using IuvoUnity.Interfaces;
using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        namespace ECS
        {
            public abstract class IuvoConfigurationBase : IuvoComponentBase, IConfigurableEntity
            {

                // base class for the configurations of various objects
                public virtual void OnConfigure(IuvoEntity entity) { }
                public virtual void OnReconfigure(IuvoEntity entity) { }
            }
        }
    }
}
