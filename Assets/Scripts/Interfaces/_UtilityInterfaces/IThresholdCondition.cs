using UnityEngine;

namespace IuvoUnity
{
    namespace Interfaces
    {
            public interface IThresholdCondition : IStateACondition
            {
                float GetThresholdValue();
                float GetCurrentValue();
            }

        
    }
}