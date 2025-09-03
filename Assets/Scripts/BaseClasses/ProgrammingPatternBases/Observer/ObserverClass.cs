using UnityEngine;
using IuvoUnity.Interfaces;

namespace IuvoUnity
{
    namespace _BaseClasses
    {
        namespace _ProgrammingPatterns
        {
            public class ObserverClass : MonoBehaviour, IObserver
            {

                public void Start()
                {
                }

                public void Update()
                {

                }

                public void Observe(IObservable subject)
                {

                }

                public virtual bool IsConditionMet(IObservable subject)
                {
                    // noop
                    return true;
                }

                public virtual void OnNotify(IObservable subject)
                {
                    // noop
                }
            }
        }
    }
}