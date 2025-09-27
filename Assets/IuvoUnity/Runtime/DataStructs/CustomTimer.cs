using IuvoUnity.BaseClasses;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public abstract class CustomTimer : MonoBehaviour, IDataStructBase
        {
            public MultiTimer timer;

            protected virtual void Awake()
            {
                timer = new MultiTimer();
                timer.Initialize();
            }

            public abstract void StartTimer();


            public abstract void PauseTimer();


            public abstract void ResumeTimer();


            public abstract void UpdateTimer();


            public abstract void ResetTimer();


            public virtual void OnDestroy()
            {
                timer.Dispose();
            }
        }

    }

}
