using System.Collections.Generic;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        namespace ECS
        {
            public class IuvoSystemRegistry
            {
                private static readonly List<IuvoSystem> systems = new List<IuvoSystem>();

                public static void RegisterSystem(IuvoSystem system)
                {
                    systems.Add(system);
  
                }

                public static void UnregisterSystem(IuvoSystem system)
                {
                    systems.Remove(system);
                }

                public static void UpdateAll(float deltaTime)
                {
                    foreach (var system in systems)
                    {
                        system.Update(deltaTime);
                    }

                    foreach (var system in systems)
                    {
                        system.LateUpdate(deltaTime);
                    }
                }
            }

        }
        
    }
}