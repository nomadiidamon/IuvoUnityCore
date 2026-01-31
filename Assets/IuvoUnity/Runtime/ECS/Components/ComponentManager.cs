using System;
using System.Collections.Generic;
using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        namespace ECS
        {

            public class ComponentManager
            {
                public IuvoEntity _myIuvoEntity { get; private set; }

                public ComponentManager(IuvoEntity myIuvoEntity)
                {
                    _myIuvoEntity = myIuvoEntity;
                }

                private Dictionary<Type, IuvoComponentBase> _components = new Dictionary<Type, IuvoComponentBase>();

                public void AddComponent<T>(T component) where T : IuvoComponentBase
                {
                    _components[typeof(T)] = component;
                    //component.OnAdd(_myIuvoEntity);
                }
#nullable enable
                public T? GetComponent<T>() where T : IuvoComponentBase
                {
                    if (_components.TryGetValue(typeof(T), out var comp))
                        return comp as T;
                    return null;
                }
#nullable disable
                public bool TryGetComponent<T>(out T component) where T : IuvoComponentBase
                {
                    if (_components.TryGetValue(typeof(T), out var comp))
                    {
                        component = comp as T;
                        return true;
                    }
                    component = null;
                    return false;
                }

                public bool HasComponent<T>() where T : IuvoComponentBase
                {
                    return _components.ContainsKey(typeof(T));
                }
                public void RemoveComponent<T>() where T : IuvoComponentBase
                {
                    if (_components.TryGetValue(typeof(T), out var component))
                    {
                        //component.OnRemove(_myIuvoEntity);
                        _components.Remove(typeof(T));
                    }
                }
                public bool TryToRemoveComponent<T>() where T : IuvoComponentBase
                {
                    if (!HasComponent<T>()) return false;
                    return _components.Remove(typeof(T));
                }
                public List<IuvoComponentBase> GetAllComponents()
                {
                    List<IuvoComponentBase> components = new List<IuvoComponentBase>();
                    foreach (var component in _components.Values)
                    {
                        components.Add(component);
                    }
                    return components;
                }

                public void ClearComponentManager(IuvoEntity entity)
                {
                    foreach (var component in _components.Values)
                    {
                        //component.OnRemove(entity);
                    }
                    _components.Clear();
                }

            }
        }
    }
}

