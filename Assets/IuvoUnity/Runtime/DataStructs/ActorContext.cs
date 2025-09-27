using IuvoUnity.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {
        public enum ContextKey_ACTOR
        { Targets, Players, Allies, Enemies, Cameras, Interactables, RandomObjects }

        public class ActorContext : IContext
        {
            private Dictionary<ContextKey_ACTOR, HashSet<GameObject>> actors = new();

            public ActorContext()
            {
                foreach (ContextKey_ACTOR key in System.Enum.GetValues(typeof(ContextKey_ACTOR)))
                {
                    actors[key] = new HashSet<GameObject>();
                }
            }

            // Generic Methods

            private IEnumerable<GameObject> Get(ContextKey_ACTOR key) => actors[key];

            private GameObject GetFirst(ContextKey_ACTOR key)
            {
                foreach (var obj in actors[key])
                    return obj;
                return null;
            }

            private void Set(ContextKey_ACTOR key, IEnumerable<GameObject> objs)
            {
                actors[key].Clear();
                foreach (var obj in objs)
                {
                    if (obj != null)
                        actors[key].Add(obj);
                }
            }

            private void Add(ContextKey_ACTOR key, GameObject obj)
            {
                if (obj != null)
                    actors[key].Add(obj);
            }

            private void Remove(ContextKey_ACTOR key, GameObject obj)
            {
                if (obj != null)
                    actors[key].Remove(obj);
            }

            private bool Has(ContextKey_ACTOR key) => actors[key].Count > 0;

            private bool Contains(ContextKey_ACTOR key, GameObject obj)
            {
                return obj != null && actors[key].Contains(obj);
            }

            private void Clear(ContextKey_ACTOR key)
            {
                actors[key].Clear();
            }

            private void ClearAll()
            {
                foreach (var key in actors.Keys)
                    Clear(key);
            }

            public void Dispose()
            {
                ClearAll();
            }

            // === Category-Specific Helper Methods ===

            // Targets
            public IEnumerable<GameObject> GetTargets() => Get(ContextKey_ACTOR.Targets);
            public int GetTargetCount() => GetTargets() is ICollection<GameObject> coll ? coll.Count : 0;
            public GameObject GetFirstTarget() => GetFirst(ContextKey_ACTOR.Targets);
            public void SetTargets(IEnumerable<GameObject> objs) => Set(ContextKey_ACTOR.Targets, objs);
            public void AddTarget(GameObject obj) => Add(ContextKey_ACTOR.Targets, obj);
            public void RemoveTarget(GameObject obj) => Remove(ContextKey_ACTOR.Targets, obj);
            public void ClearTargets() => Clear(ContextKey_ACTOR.Targets);
            public bool HasTargets() => Has(ContextKey_ACTOR.Targets);
            public bool ContainsTarget(GameObject obj) => Contains(ContextKey_ACTOR.Targets, obj);

            // Players
            public IEnumerable<GameObject> GetPlayers() => Get(ContextKey_ACTOR.Players);
            public int GetPlayerCount() => GetPlayers() is ICollection<GameObject> coll ? coll.Count : 0;
            public GameObject GetFirstPlayer() => GetFirst(ContextKey_ACTOR.Players);
            public void SetPlayers(IEnumerable<GameObject> objs) => Set(ContextKey_ACTOR.Players, objs);
            public void AddPlayer(GameObject obj) => Add(ContextKey_ACTOR.Players, obj);
            public void RemovePlayer(GameObject obj) => Remove(ContextKey_ACTOR.Players, obj);
            public void ClearPlayers() => Clear(ContextKey_ACTOR.Players);
            public bool HasPlayers() => Has(ContextKey_ACTOR.Players);
            public bool ContainsPlayer(GameObject obj) => Contains(ContextKey_ACTOR.Players, obj);

            // Allies
            public IEnumerable<GameObject> GetAllies() => Get(ContextKey_ACTOR.Allies);
            public int GetAllyCount() => GetAllies() is ICollection<GameObject> coll ? coll.Count : 0;
            public GameObject GetFirstAlly() => GetFirst(ContextKey_ACTOR.Allies);
            public void SetAllies(IEnumerable<GameObject> objs) => Set(ContextKey_ACTOR.Allies, objs);
            public void AddAlly(GameObject obj) => Add(ContextKey_ACTOR.Allies, obj);
            public void RemoveAlly(GameObject obj) => Remove(ContextKey_ACTOR.Allies, obj);
            public void ClearAllies() => Clear(ContextKey_ACTOR.Allies);
            public bool HasAllies() => Has(ContextKey_ACTOR.Allies);
            public bool ContainsAlly(GameObject obj) => Contains(ContextKey_ACTOR.Allies, obj);

            // Enemies
            public IEnumerable<GameObject> GetEnemies() => Get(ContextKey_ACTOR.Enemies);
            public int GetEnemyCount() => GetEnemies() is ICollection<GameObject> coll ? coll.Count : 0;
            public GameObject GetFirstEnemy() => GetFirst(ContextKey_ACTOR.Enemies);
            public void SetEnemies(IEnumerable<GameObject> objs) => Set(ContextKey_ACTOR.Enemies, objs);
            public void AddEnemy(GameObject obj) => Add(ContextKey_ACTOR.Enemies, obj);
            public void RemoveEnemy(GameObject obj) => Remove(ContextKey_ACTOR.Enemies, obj);
            public void ClearEnemies() => Clear(ContextKey_ACTOR.Enemies);
            public bool HasEnemies() => Has(ContextKey_ACTOR.Enemies);
            public bool ContainsEnemy(GameObject obj) => Contains(ContextKey_ACTOR.Enemies, obj);

            // Cameras
            public IEnumerable<GameObject> GetCameras() => Get(ContextKey_ACTOR.Cameras);
            public int GetCameraCount() => GetCameras() is ICollection<GameObject> coll ? coll.Count : 0;
            public GameObject GetFirstCamera() => GetFirst(ContextKey_ACTOR.Cameras);
            public void SetCameras(IEnumerable<GameObject> objs) => Set(ContextKey_ACTOR.Cameras, objs);
            public void AddCamera(GameObject obj) => Add(ContextKey_ACTOR.Cameras, obj);
            public void RemoveCamera(GameObject obj) => Remove(ContextKey_ACTOR.Cameras, obj);
            public void ClearCameras() => Clear(ContextKey_ACTOR.Cameras);
            public bool HasCameras() => Has(ContextKey_ACTOR.Cameras);
            public bool ContainsCamera(GameObject obj) => Contains(ContextKey_ACTOR.Cameras, obj);

            // Interactables
            public IEnumerable<GameObject> GetInteractables() => Get(ContextKey_ACTOR.Interactables);
            public int GetInteractableCount() => GetInteractables() is ICollection<GameObject> coll ? coll.Count : 0;
            public GameObject GetFirstInteractable() => GetFirst(ContextKey_ACTOR.Interactables);
            public void SetInteractables(IEnumerable<GameObject> objs) => Set(ContextKey_ACTOR.Interactables, objs);
            public void AddInteractable(GameObject obj) => Add(ContextKey_ACTOR.Interactables, obj);
            public void RemoveInteractable(GameObject obj) => Remove(ContextKey_ACTOR.Interactables, obj);
            public void ClearInteractables() => Clear(ContextKey_ACTOR.Interactables);
            public bool HasInteractables() => Has(ContextKey_ACTOR.Interactables);
            public bool ContainsInteractable(GameObject obj) => Contains(ContextKey_ACTOR.Interactables, obj);

            // RandomObjects
            public IEnumerable<GameObject> GetRandomObjects() => Get(ContextKey_ACTOR.RandomObjects);
            public int GetRandomObjectCount() => GetRandomObjects() is ICollection<GameObject> coll ? coll.Count : 0;
            public GameObject GetFirstRandomObject() => GetFirst(ContextKey_ACTOR.RandomObjects);
            public void SetRandomObjects(IEnumerable<GameObject> objs) => Set(ContextKey_ACTOR.RandomObjects, objs);
            public void AddRandomObject(GameObject obj) => Add(ContextKey_ACTOR.RandomObjects, obj);
            public void RemoveRandomObject(GameObject obj) => Remove(ContextKey_ACTOR.RandomObjects, obj);
            public void ClearRandomObjects() => Clear(ContextKey_ACTOR.RandomObjects);
            public bool HasRandomObjects() => Has(ContextKey_ACTOR.RandomObjects);
            public bool ContainsRandomObject(GameObject obj) => Contains(ContextKey_ACTOR.RandomObjects, obj);
        }

    }

}