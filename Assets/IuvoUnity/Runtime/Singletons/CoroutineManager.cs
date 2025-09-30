using System.Collections.Generic;
using System.Collections;
using UnityEngine;


namespace IuvoUnity
{
    namespace Singletons
    {
        public class CoroutineManager : PersistentSingleton<CoroutineManager>
        {
   
            public static Dictionary<WaitForSeconds, float> waitForDictionary = new Dictionary<WaitForSeconds, float>();

            protected override void Awake()
            {
                base.Awake();
            }

            void Start()
            {

            }

            public static void RunCoroutine(IEnumerator coroutine)
            {
                Instance.StartCoroutine(coroutine);
            }

            public static WaitForSeconds GetWaitForSeconds(float seconds)
            {
                foreach (var kvp in waitForDictionary)
                {
                    if (Mathf.Approximately(kvp.Value, seconds))
                    {
                        return kvp.Key;
                    }
                }
                WaitForSeconds waitFor = new WaitForSeconds(seconds);
                waitForDictionary.Add(waitFor, seconds);
                return waitFor;
            }

        }
    }
}