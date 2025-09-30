using System.Collections.Generic;
using UnityEngine;


namespace IuvoUnity
{
    namespace Extensions
    {

        /// <summary>
        /// Provides extension methods for Unity's <see cref="Transform"/> component,
        /// enabling simplified manipulation and queries of transforms in the scene.
        /// </summary>
        public static class TransformExtensions
        {
            /// <summary>
            /// Sets the parent of this transform without modifying its world position, rotation, or scale.
            /// </summary>
            public static void SetParentWithoutAffectingTransform(this Transform transform, Transform newParent)
            {
                Vector3 worldPosition = transform.position;
                Quaternion worldRotation = transform.rotation;
                Vector3 worldScale = transform.lossyScale;

                transform.SetParent(newParent, worldPositionStays: false);

                transform.position = worldPosition;
                transform.rotation = worldRotation;

                // Manually set scale because Unity doesn't preserve it when reparenting
                Vector3 parentScale = newParent != null ? newParent.lossyScale : Vector3.one;
                transform.localScale = new Vector3(
                    worldScale.x / parentScale.x,
                    worldScale.y / parentScale.y,
                    worldScale.z / parentScale.z
                );
            }



            /// <summary>Finds a direct child of the transform by name.</summary>
            public static Transform FindChildByName(this Transform transform, string name)
            {
                foreach (Transform child in transform)
                {
                    if (child.name == name) return child;
                }
                return null;
            }

            /// <summary>Recursively searches for a child transform by name.</summary>
            public static Transform FindChildByNameRecursive(this Transform transform, string name)
            {
                foreach (Transform child in transform)
                {
                    if (child.name == name) return child;
                    Transform found = child.FindChildByNameRecursive(name);
                    if (found != null) return found;
                }
                return null;
            }

            /// <summary>Sets the transform's rotation to face the given direction.</summary>
            public static void SetRotationToTargetDirection(this Transform transform, Vector3 direction)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }

            /// <summary>Smoothly rotates the transform to face a target transform.</summary>
            public static void RotateTowardsTarget(this Transform transform, Transform target, float speed, float deltaTime)
            {
                Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * deltaTime);
            }

            /// <summary>Returns true if this transform is above the other transform.</summary>
            public static bool IsAbove(this Transform transform, Transform other) => transform.position.y > other.position.y;

            /// <summary>Returns true if this transform is below the other transform.</summary>
            public static bool IsBelow(this Transform transform, Transform other) => transform.position.y < other.position.y;

            /// <summary>Returns true if this transform is to the left of the other transform.</summary>
            public static bool IsLeftOf(this Transform transform, Transform other) => transform.position.x < other.position.x;

            /// <summary>Returns true if this transform is to the right of the other transform.</summary>
            public static bool IsRightOf(this Transform transform, Transform other) => transform.position.x > other.position.x;

            /// <summary>Returns true if this transform is in front of the other transform.</summary>
            public static bool IsInFrontOf(this Transform transform, Transform other) => transform.position.z > other.position.z;

            /// <summary>Returns true if this transform is behind the other transform.</summary>
            public static bool IsBehind(this Transform transform, Transform other) => transform.position.z < other.position.z;

            /// <summary>Sets this transform's rotation to match the target's rotation.</summary>
            public static void RotateToMatch(this Transform transform, Transform target)
            {
                transform.rotation = target.rotation;
            }

            /// <summary>Multiplies the transform's local scale by the given vector.</summary>
            public static void MultiplyLocalScale(this Transform transform, Vector3 multiplier)
            {
                transform.localScale = Vector3.Scale(transform.localScale, multiplier);
            }

            /// <summary>Sets this transform's local scale to match the target's.</summary>
            public static void ScaleToMatch(this Transform transform, Transform target)
            {
                transform.localScale = target.localScale;
            }

            /// <summary>Moves the transform along a given axis by a specified distance.</summary>
            public static void TranslateAlongAxis(this Transform transform, Vector3 axis, float distance)
            {
                transform.position += axis.normalized * distance;
            }

            /// <summary>Moves the transform towards a target position at a specified speed.</summary>
            public static void MoveTowardsPosition(this Transform transform, Vector3 targetPosition, float speed, float deltaTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * deltaTime);
            }

            /// <summary>Gets the world rotation of the transform.</summary>
            public static Quaternion GetWorldRotation(this Transform transform)
            {
                return transform.rotation;
            }

            /// <summary>Sets the world rotation of the transform.</summary>
            public static void SetWorldRotation(this Transform transform, Quaternion rotation)
            {
                transform.rotation = rotation;
            }

            /// <summary>Rotates the transform around a world axis by a specified angle.</summary>
            public static void RotateAroundWorldAxis(this Transform transform, Vector3 worldAxis, float angle)
            {
                transform.Rotate(worldAxis, angle, Space.World);
            }

            /// <summary>Rotates the transform around a local axis by a specified angle.</summary>
            public static void RotateAroundLocalAxis(this Transform transform, Vector3 localAxis, float angle)
            {
                transform.Rotate(localAxis, angle, Space.Self);
            }

            /// <summary>Aligns this transform's position and rotation with the target.</summary>
            public static void AlignWith(this Transform transform, Transform target)
            {
                transform.position = target.position;
                transform.rotation = target.rotation;
            }

            /// <summary>Aligns this transform with the target using a position offset.</summary>
            public static void AlignWith(this Transform transform, Transform target, Vector3 offset)
            {
                transform.position = target.position + offset;
                transform.rotation = target.rotation;
            }

            /// <summary>Aligns this transform with the target using position and rotation offsets.</summary>
            public static void AlignWith(this Transform transform, Transform target, Vector3 positionOffset, Vector3 rotationOffset)
            {
                transform.position = target.position + positionOffset;
                transform.rotation = target.rotation * Quaternion.Euler(rotationOffset);
            }

            /// <summary>Aligns this transform with the target using position, rotation offsets, and absolute scale.</summary>
            public static void AlignWith(this Transform transform, Transform target, Vector3 positionOffset, Vector3 rotationOffset, Vector3 scale)
            {
                transform.position = target.position + positionOffset;
                transform.rotation = target.rotation * Quaternion.Euler(rotationOffset);
                transform.localScale = scale;
            }

            /// <summary>Aligns this transform with the target using position, rotation offsets, and uniform scale.</summary>
            public static void AlignWith(this Transform transform, Transform target, Vector3 positionOffset, Vector3 rotationOffset, float scale)
            {
                AlignWith(transform, target, positionOffset, rotationOffset, new Vector3(scale, scale, scale));
            }

            /// <summary>Detaches this transform from its parent and destroys the associated GameObject.</summary>
            public static void DetachAndDestroy(this Transform transform)
            {
                transform.SetParent(null);
                Object.Destroy(transform.gameObject);
            }

            /// <summary>Rotates the transform around a point along the given axis.</summary>
            public static void RotateAround(this Transform transform, Vector3 point, Vector3 axis, float angle)
            {
                transform.RotateAround(point, axis, angle);
            }

            /// <summary>Smoothly rotates around a given axis over a set duration.</summary>
            public static void RotateAroundAxisWithDuration(this Transform transform, Vector3 axis, float angle, float duration, float deltaTime)
            {
                float angleStep = angle * (deltaTime / duration);
                transform.Rotate(axis, angleStep, Space.Self);
            }

            /// <summary>Swaps the position of this transform with another.</summary>
            public static void SwapPositionWith(this Transform transform, Transform target)
            {
                Vector3 temp = transform.position;
                transform.position = target.position;
                target.position = temp;
            }

            /// <summary>Swaps the rotation of this transform with another.</summary>
            public static void SwapRotationWith(this Transform transform, Transform target)
            {
                Quaternion temp = transform.rotation;
                transform.rotation = target.rotation;
                target.rotation = temp;
            }

            /// <summary>Swaps the local scale of this transform with another.</summary>
            public static void SwapScaleWith(this Transform transform, Transform target)
            {
                Vector3 temp = transform.localScale;
                transform.localScale = target.localScale;
                target.localScale = temp;
            }


            /// <summary>Smoothly rotates the transform to face a world position.</summary>
            public static void RotateSmoothlyTowards(this Transform transform, Vector3 targetPosition, float rotationSpeed, float deltaTime)
            {
                Vector3 direction = targetPosition - transform.position;
                if (direction != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * deltaTime);
                }
            }

            /// <summary>Smoothly rotates the transform to face another transform.</summary>
            public static void RotateSmoothlyTowards(this Transform transform, Transform target, float rotationSpeed, float deltaTime)
            {
                RotateSmoothlyTowards(transform, target.position, rotationSpeed, deltaTime);
            }

            /// <summary>Recursively gets all descendant GameObjects of the transform.</summary>
            public static IEnumerable<GameObject> GetAllChildren(this Transform transform)
            {
                foreach (Transform child in transform)
                {
                    yield return child.gameObject;

                    foreach (var descendant in child.GetAllChildren())
                    {
                        yield return descendant;
                    }
                }
            }
        }
    }
}