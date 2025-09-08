using IuvoUnity._BaseClasses._ECS;
using IuvoUnity.Interfaces;
using IuvoUnity.BaseClasses;


namespace IuvoUnity
{

    namespace _ECS
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