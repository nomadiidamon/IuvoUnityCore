using IuvoUnity._BaseClasses._ECS;
using IuvoUnity.BaseClasses;
using System.Collections.Generic;

namespace IuvoUnity
{
    namespace Interfaces
    {
        namespace RPG
        {
            public interface IDealDamage
            {
                public List<DamageComponent> totalDamage { get; set; }
                void DealDamage(IuvoEntity damagable);
            }
        }
    }
}
