using System;

namespace IuvoUnity
{
    namespace Interfaces
    {

        namespace RPG
        {
            public interface IStunnable
            {

                void Stun(float time);
                bool isStunned { get; set; }
            }
        }
    }
}