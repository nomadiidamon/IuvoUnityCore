using System.Collections.Generic;
using IuvoUnity.Interfaces;

namespace IuvoUnity
{
    namespace Interfaces
    {
        namespace RPG
        {
            public interface IAbility
            {
                string AbilityName { get; }
                List<IStateACondition> Conditions { get; }

                void Activate();
                void Deactivate();
                bool CanActivate();
            }

        }
    }
}