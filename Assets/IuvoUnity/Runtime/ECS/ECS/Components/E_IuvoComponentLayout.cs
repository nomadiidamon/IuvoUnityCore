using IuvoUnity.BaseClasses.ECS;
using IuvoUnity.Interfaces;
using IuvoUnity.BaseClasses;


namespace IuvoUnity
{

    namespace ECS
    {
        public abstract class IuvoComponentLayout : IuvoComponentBase, IAddableEntity, IInitializeEntity, IUpdatableEntity
        {
            public virtual void Initialize(IuvoEntity entity) { }
            public virtual void OnAdd(IuvoEntity entity) { }
            public virtual void OnRemove(IuvoEntity entity) { }
            public abstract void Update(IuvoEntity entity);
        }
    }
}