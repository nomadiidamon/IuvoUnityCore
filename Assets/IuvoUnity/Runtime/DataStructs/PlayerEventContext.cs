using IuvoUnity.Interfaces;
using System.Collections.Generic;
using UnityEngine.Events;

namespace IuvoUnity
{
    namespace DataStructs
    {
        public enum ContextKey_PLAYER_EVENT { OnDamageTaken, OnDeath, OnRevive, OnItemPickup, OnObjectiveComplete, OnInteract, OnPause }

        public class PlayerEventContext : IContext
        {
            public readonly Dictionary<ContextKey_PLAYER_EVENT, FlexibleEvent> events = new Dictionary<ContextKey_PLAYER_EVENT, FlexibleEvent>();

            public PlayerEventContext()
            {
                foreach (ContextKey_PLAYER_EVENT key in System.Enum.GetValues(typeof(ContextKey_PLAYER_EVENT)))
                {
                    events[key] = new FlexibleEvent();
                }
            }

            public bool TryGetEvent(ContextKey_PLAYER_EVENT key, out FlexibleEvent flexibleEvent)
            {
                if (events.TryGetValue(key, out flexibleEvent))
                {
                    return true;
                }

                UnityEngine.Debug.LogError($"Event {key} not found in PlayerEventContext.");
                return false;
            }

            public void Clear(ContextKey_PLAYER_EVENT key)
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

            public void AddListener(ContextKey_PLAYER_EVENT key, System.Action listener)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    flexibleEvent.AddListener(listener);
                }
            }

            public void RemoveListener(ContextKey_PLAYER_EVENT key, System.Action listener)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    flexibleEvent.RemoveListener(listener);
                }
            }

            public void AddUnityListener(ContextKey_PLAYER_EVENT key, UnityAction listener)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    flexibleEvent.AddUnityListener(listener);
                }
            }

            public void RemoveUnityListener(ContextKey_PLAYER_EVENT key, UnityAction listener)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    flexibleEvent.RemoveUnityListener(listener);
                }
            }

            public FlexibleEvent GetEvent(ContextKey_PLAYER_EVENT key)
            {
                if (TryGetEvent(key, out var flexibleEvent))
                {
                    return flexibleEvent;
                }

                return null;
            }

            public bool HasEvent(ContextKey_PLAYER_EVENT key)
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