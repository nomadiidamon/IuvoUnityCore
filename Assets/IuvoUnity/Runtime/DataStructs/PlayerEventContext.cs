using IuvoUnity.Interfaces;
using IuvoUnity.Events;
using System.Collections.Generic;
using UnityEngine.Events;

namespace IuvoUnity
{
    namespace DataStructs
    {
        public enum ContextKey_EVENT { OnDamageTaken, OnDeath, OnRevive, OnItemPickup, OnObjectiveComplete, OnInteract, OnPause }

        public class EventContext : IContext
        {
            public readonly Dictionary<ContextKey_EVENT, FlexibleEvent> events = new Dictionary<ContextKey_EVENT, FlexibleEvent>();

            public EventContext()
            {
                foreach (ContextKey_EVENT key in System.Enum.GetValues(typeof(ContextKey_EVENT)))
                {
                    events[key] = new FlexibleEvent();
                }
            }

            public bool TryGetEvent(ContextKey_EVENT key, out FlexibleEvent flexibleEvent)
            {
                if (events.TryGetValue(key, out flexibleEvent))
                {
                    return true;
                }

                UnityEngine.Debug.LogError($"Event {key} not found in PlayerEventContext.");
                return false;
            }

            public void Clear(ContextKey_EVENT key)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    flexibleEvent.RemoveAllFlexibleEventListeners();
                }
            }

            public void ClearAll()
            {
                foreach (var key in events.Keys)
                {
                    Clear(key);
                }
            }

            public void AddListener(ContextKey_EVENT key, System.Action listener)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    flexibleEvent.AddListener(listener);
                }
            }

            public void RemoveListener(ContextKey_EVENT key, System.Action listener)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    flexibleEvent.RemoveListener(listener);
                }
            }

            public void AddUnityListener(ContextKey_EVENT key, UnityAction listener)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    flexibleEvent.AddUnityListener(listener);
                }
            }

            public void RemoveUnityListener(ContextKey_EVENT key, UnityAction listener)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    flexibleEvent.RemoveUnityListener(listener);
                }
            }

            public FlexibleEvent GetEvent(ContextKey_EVENT key)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    return flexibleEvent;
                }

                return null;
            }

            public bool HasEvent(ContextKey_EVENT key)
            {
                return events.ContainsKey(key);
            }

            public void Dispose()
            {
                ClearAll();
            }
        }
    }
}