using IuvoUnity._ECS;
using System;
using System.Collections.Generic;
using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace _BaseClasses
    {
        namespace _ECS
        {

            public class ComponentFilter
            {
                public IEnumerable<Type> archetype;

                public static IEnumerable<Type> GetAllComponentsEnumerable(IuvoEntity entity)
                {
                    return (IEnumerable<Type>)entity._ComponentManager.GetAllComponents();
                }

                public static Type[] GetAllComponentsArray(IuvoEntity entity)
                {
                    List<IuvoComponentBase> result = new List<IuvoComponentBase>();
                    result = entity._ComponentManager.GetAllComponents();

                    Type[] archetype = new Type[result.Count];
                    for (int i = 0; i < result.Count; i++)
                    {
                        archetype[i] = result[i].GetType();
                    }
                    return archetype;
                }
            }




            public class IuvoEntityRegistry
            {
                public static int _myRegistryID { get; set; } = 0;
                private static int _nextEntityID = 0;
                private static int _nextTimerID = 0;
                public static Dictionary<int, IuvoEntity> _myRegisteredEntities = new Dictionary<int, IuvoEntity>();
                public static Dictionary<int, IuvoEntity> _myRegisteredTimers = new Dictionary<int, IuvoEntity>();



                public static IuvoEntity CreateEntity(bool debugOn = true)
                {
                    var entity = IuvoEntity.CreateFromRegistry();
                    // always double check that the registry doesnt have that ID
                    while (_myRegisteredEntities.ContainsKey(_nextEntityID))
                    {
                        _nextEntityID++;
                    }

                    // assign the id if checks passed
                    entity._ID = _nextEntityID;
                    // always increment the id...
                    _nextEntityID++;

                    // Create the component manager and assign its entity reference
                    entity._ComponentManager = new ComponentManager(entity);

                    // Register in entity database
                    _myRegisteredEntities.Add(entity._ID, entity);

                    //// Add other default components... 
                    //IuvoDebug.Debugger debugger = new IuvoDebug.Debugger();         // default # 1
                    //entity.AddComponent<IuvoDebug.Debugger>(debugger);

                    // Maps this entity to the creating registry
                    IuvoRegistryID identification = new IuvoRegistryID();           // default # 2
                    identification._id = _myRegistryID;
                    entity.AddComponent(identification);

                    // do initialization of default components  
                    InitializeDefaultComponents(entity);


                    // ...and finally return the new entity
                    return entity;
                }

                public static void DestroyEntity(int entityId)
                {
                    if (_myRegisteredEntities.TryGetValue(entityId, out var entity))
                    {
                        // cleanup default components
                        BreakdownRemovableComponents(entity);


                        // Finally destroy the held components and erase knowledge of the entity
                        entity._ComponentManager.ClearComponentManager(entity);
                        _myRegisteredEntities.Remove(entityId);
                    }
                }

                public static IuvoEntity CreateTimerEntity(bool debugOn = true, IuvoEntity parent = null)
                {
                    var timer = CreateEntity(debugOn);

                    while (_myRegisteredTimers.ContainsKey(_nextTimerID))
                    {
                        _nextTimerID++;
                    }

                    // Maps this entity as a timer
                    IuvoWorldID worldID = new IuvoWorldID();

                    IuvoTimerID iuvoTimerID = new IuvoTimerID();                // default # 1
                    iuvoTimerID._id = _nextTimerID;
                    worldID._timerID = iuvoTimerID;

                    while (_myRegisteredEntities.ContainsKey(_nextEntityID))
                    {
                        _nextEntityID++;
                    }

                    if (parent == null)
                    {
                        worldID._entity = timer;
                    }
                    else
                    {
                        worldID._entity = parent;
                    }
                    timer._ComponentManager.TryGetComponent<IuvoRegistryID>(out var registryID);
                    worldID._registryID = registryID;

                    timer._ComponentManager.AddComponent<IuvoWorldID>(worldID);

                    // do initialization of default components  
                    InitializeDefaultComponents(timer);


                    // ...and finally return the new entity
                    return timer;
                }

                public static void InitializeDefaultComponents(IuvoEntity entity)
                {
                    List<IuvoComponentBase> comps = entity._ComponentManager.GetAllComponents();
                    for (int i = 0; i < comps.Count; i++)
                    {

                    }
                }

                public static void BreakdownRemovableComponents(IuvoEntity entity)
                {
                    List<IuvoComponentBase> comps = entity._ComponentManager.GetAllComponents();
                    for (int i = 0; i < comps.Count; i++)
                    {

                    }
                }

                public static bool FindIuvoEntityInRegister(IuvoEntity entity, out bool isTimer)
                {
                    if (_myRegisteredEntities.ContainsKey(entity._ID))
                    {
                        return isTimer = false;
                    }
                    else if (_myRegisteredTimers.ContainsKey(entity._ID))
                    {
                        return isTimer = true;
                    }
                    else
                    {
                        return isTimer = false;
                    }
                }

            }

        }
    }


}