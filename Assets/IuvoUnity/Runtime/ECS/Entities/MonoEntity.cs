using UnityEngine;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        namespace ECS
        {
            public class MonoEntity : MonoBehaviour
            {
                public IuvoEntity _Entity { get; private set; }

                public void Initialize(IuvoEntity entity)
                {
                    _Entity = entity;
                }
            }

        }


    }
}