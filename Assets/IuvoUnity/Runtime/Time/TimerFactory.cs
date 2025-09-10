using IuvoUnity.DataStructs;
using UnityEngine;

namespace IuvoUnity
{
    namespace IuvoTime
    {
        public class TimerFactory
        {

            
            public void Create(Timer creatable)
            {
                creatable.OnCreate();
            }

            public void Destroy(Timer destructible)
            {
                destructible.OnDestroy();
            }

            public Timer Create(TimerConfiguration configuration)
            {
                // Implementation for creating a timer based on the configuration
                GameObject timerObject = new GameObject("Timer");
                Timer timer = timerObject.AddComponent<Timer>();


                return timer;
            }
        }

    }
}