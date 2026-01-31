using System.Collections.Generic;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        namespace ECS
        {

            public class IuvoEntity
            {
                public int _ID { get; set; }
                public ComponentManager _ComponentManager;
                private IuvoEntity()
                {
                    _ID = 0;
                    _ComponentManager = new ComponentManager(this);
                }

                internal static IuvoEntity CreateFromRegistry()
                {
                    return new IuvoEntity();
                }

                public void AddComponent<T>(T component) where T : IuvoComponentBase
                {
                    _ComponentManager.AddComponent<T>(component);
                }

#nullable enable
                public T? GetComponent<T>() where T : IuvoComponentBase
                {
                    return _ComponentManager.GetComponent<T>();
                }
#nullable disable
                public bool HasComponent<T>() where T : IuvoComponentBase
                {
                    return _ComponentManager.HasComponent<T>();
                }
                public void RemoveComponent<T>() where T : IuvoComponentBase
                {
                    _ComponentManager.RemoveComponent<T>();
                }
                public void GetAllComponents(out List<IuvoComponentBase> components)
                {
                    components = _ComponentManager.GetAllComponents();
                }
            }


        }
    }
}