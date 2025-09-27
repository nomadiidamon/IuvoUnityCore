using UnityEngine;
using IuvoUnity.Interfaces;
using System.Collections.Generic;

namespace IuvoUnity
{
    namespace DataStructs
    {
        public enum ContextKey_PLAYER_TRANSFORM { Transform, Position, Destination, Velocity, Direction, LookTarget, LastDirection, InputDirection }

        public class TransformContext : IContext
        {
            // Internal dictionary
            private readonly Dictionary<ContextKey_PLAYER_TRANSFORM, object> _data = new();

            public TransformContext()
            {
                // Initialize all keys with default values
                foreach (ContextKey_PLAYER_TRANSFORM key in System.Enum.GetValues(typeof(ContextKey_PLAYER_TRANSFORM)))
                {
                    _data[key] = default;
                }
            }
            public void Dispose()
            {
                _data.Clear();
            }

            // --- Private Generic Set/Get ---
            private void Set<T>(ContextKey_PLAYER_TRANSFORM key, T value)
            {
                _data[key] = value;
            }
            private T Get<T>(ContextKey_PLAYER_TRANSFORM key)
            {
                if (_data.TryGetValue(key, out var value) && value is T cast)
                    return cast;

                return default;
            }
            private bool TryGet<T>(ContextKey_PLAYER_TRANSFORM key, out T value)
            {
                if (_data.TryGetValue(key, out var obj) && obj is T castValue)
                {
                    value = castValue;
                    return true;
                }

                value = default;
                return false;
            }

            // --- Public Set / Get / TryGet for each key ---

            public void SetTransform(Transform transform) => Set(ContextKey_PLAYER_TRANSFORM.Transform, transform);
            public Transform GetTransform() => Get<Transform>(ContextKey_PLAYER_TRANSFORM.Transform);
            public bool TryGetTransform(out Transform transform) => TryGet(ContextKey_PLAYER_TRANSFORM.Transform, out transform);

            public void SetPosition(Vector3 position) => Set(ContextKey_PLAYER_TRANSFORM.Position, position);
            public Vector3 GetPosition() => Get<Vector3>(ContextKey_PLAYER_TRANSFORM.Position);
            public bool TryGetPosition(out Vector3 position) => TryGet(ContextKey_PLAYER_TRANSFORM.Position, out position);

            public void SetDestination(Vector3 destination) => Set(ContextKey_PLAYER_TRANSFORM.Destination, destination);
            public Vector3 GetDestination() => Get<Vector3>(ContextKey_PLAYER_TRANSFORM.Destination);
            public bool TryGetDestination(out Vector3 destination) => TryGet(ContextKey_PLAYER_TRANSFORM.Destination, out destination);

            public void SetVelocity(Vector3 velocity) => Set(ContextKey_PLAYER_TRANSFORM.Velocity, velocity);
            public Vector3 GetVelocity() => Get<Vector3>(ContextKey_PLAYER_TRANSFORM.Velocity);
            public bool TryGetVelocity(out Vector3 velocity) => TryGet(ContextKey_PLAYER_TRANSFORM.Velocity, out velocity);

            public void SetDirection(Vector3 direction) => Set(ContextKey_PLAYER_TRANSFORM.Direction, direction);
            public Vector3 GetDirection() => Get<Vector3>(ContextKey_PLAYER_TRANSFORM.Direction);
            public bool TryGetDirection(out Vector3 direction) => TryGet(ContextKey_PLAYER_TRANSFORM.Direction, out direction);

            public void SetLookTarget(Vector3 lookTarget) => Set(ContextKey_PLAYER_TRANSFORM.LookTarget, lookTarget);
            public Vector3 GetLookTarget() => Get<Vector3>(ContextKey_PLAYER_TRANSFORM.LookTarget);
            public bool TryGetLookTarget(out Vector3 lookTarget) => TryGet(ContextKey_PLAYER_TRANSFORM.LookTarget, out lookTarget);

            public void SetLastDirection(Vector3 lastDirection) => Set(ContextKey_PLAYER_TRANSFORM.LastDirection, lastDirection);
            public Vector3 GetLastDirection() => Get<Vector3>(ContextKey_PLAYER_TRANSFORM.LastDirection);
            public bool TryGetLastDirection(out Vector3 lastDirection) => TryGet(ContextKey_PLAYER_TRANSFORM.LastDirection, out lastDirection);

            public void SetInputDirection(Vector3 inputDirection) => Set(ContextKey_PLAYER_TRANSFORM.InputDirection, inputDirection);
            public Vector3 GetInputDirection() => Get<Vector3>(ContextKey_PLAYER_TRANSFORM.InputDirection);
            public bool TryGetInputDirection(out Vector3 inputDirection) => TryGet(ContextKey_PLAYER_TRANSFORM.InputDirection, out inputDirection);
        }
    }
}