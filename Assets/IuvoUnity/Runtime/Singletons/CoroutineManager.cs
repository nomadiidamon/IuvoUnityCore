using System.Collections;
using UnityEngine;

using System.Collections.Generic;
namespace IuvoUnity
{
    public class CoroutineManager : MonoBehaviour
    {
        private static CoroutineManager _instance;
        public static CoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject obj = new GameObject("CoroutineManager");
                    _instance = obj.AddComponent<CoroutineManager>();
                    DontDestroyOnLoad(obj);
                }
                return _instance;
            }
        }

        public static Dictionary<WaitForSeconds, float> waitForDictionary = new Dictionary<WaitForSeconds, float>();

        private void Awake()
        {
            
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