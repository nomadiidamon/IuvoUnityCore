using UnityEngine;
using IuvoUnity.Debug;

namespace IuvoUnity
{
    namespace RPG
    {
        public class Stat : ScriptableObject
        {
            [SerializeField]
            protected string statName = "New Stat";
            public string GetStatName() => statName;
            
            public void SetStatName(string newName)
            {
                if (!string.IsNullOrEmpty(newName))
                {
                    statName = newName;
                }
                else
                {
                    IuvoDebug.DebugLogWarning("Stat name cannot be empty.");
                }
            }

            public virtual void OnEnable()
            {
                // This method can be overridden in derived classes for initialization
                IuvoDebug.DebugLog($"Stat {statName} enabled.");
            }

            public virtual void OnDisable()
            {
                // This method can be overridden in derived classes for cleanup
                IuvoDebug.DebugLog($"Stat {statName} disabled.");
            }
        }
    }
}