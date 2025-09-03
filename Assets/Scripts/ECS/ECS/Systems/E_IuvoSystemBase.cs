using System.Collections.Generic;
using System;
using IuvoUnity.BaseClasses;


namespace IuvoUnity
{
    namespace _BaseClasses
    {
        namespace _ECS
        {
            public abstract class IuvoSystem
            {
                protected IuvoEntityRegistry registry;
                protected List<IuvoEntity> applicableEntities = new List<IuvoEntity>();

                // Each system declares which components it needs to operate
                protected abstract Type[] RequiredComponents { get; }

                // Optional initialization logic for the system
                public virtual void Initialize(IuvoEntityRegistry entityRegistry)
                {
                    registry = entityRegistry;

                    // Auto-register for applicable entities
                    RefreshEntityList(entityRegistry);
                }

                // The main logic that runs each frame or tick
                public abstract void Update(float deltaTime);

                // Called after all updates, good for deferred cleanup
                public virtual void LateUpdate(float deltaTime) { }


                // Refreshes the list of entities that match the system's requirements
                public void RefreshEntityList(IuvoEntityRegistry entityRegistry)
                {

                }

                protected bool EntityMatchesRequirements(IuvoEntity entity)
                {
                    foreach (var type in RequiredComponents)
                    {
                        if (!entity._ComponentManager.TryGetComponent<IuvoComponentBase>(out var thing))
                            return false;
                    }
                    return true;
                }

                // Optional: cleanup logic
                public virtual void Shutdown()
                {
                    applicableEntities.Clear();
                }
            }
        }
    }
}


