using UnityEngine;

namespace IuvoUnity
{
    namespace Extensions
    {
        public static class ColliderExtensions
        {
            public static bool IsTouching(this Collider collider, Collider other)
            {
                return collider.IsTouching(other);
            }
            public static bool IsTouchingLayers(this Collider collider, int layerMask)
            {
                return collider.IsTouchingLayers(layerMask);
            }
            public static bool Overlaps(this Collider collider, Collider other)
            {
                return collider.Overlaps(other);
            }
            public static bool Overlaps(this Collider collider, Collider other, out Collision collision)
            {
                return collider.Overlaps(other, out collision);
            }
            public static bool Overlaps(this Collider collider, Collider other, out Collider[] colliders)
            {
                return collider.Overlaps(other, out colliders);
            }
            public static bool Raycast(this Collider collider, Ray ray, out RaycastHit hitInfo, float maxDistance)
            {
                return collider.Raycast(ray, out hitInfo, maxDistance);
            }
            public static bool Raycast(this Collider collider, Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask)
            {
                return collider.Raycast(ray, out hitInfo, maxDistance, layerMask);
            }
            public static bool Raycast(this Collider collider, Ray ray, out RaycastHit hitInfo, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
            {
                return collider.Raycast(ray, out hitInfo, maxDistance, layerMask, queryTriggerInteraction);
            }

            public static void SetTrigger(this Collider collider, bool isTrigger)
            {
                collider.isTrigger = isTrigger;
            }

            public static void SetMaterial(this Collider collider, PhysicsMaterial material)
            {
                collider.material = material;
            }
            public static bool IsTrigger(this Collider collider)
            {
                return collider.isTrigger;
            }

            public static bool BoundsContainsPoint(this Collider collider, Vector3 point)
            {
                return collider.bounds.Contains(point);
            }

            public static bool BoundsIntersects(this Collider collider, Bounds bounds)
            {
                return collider.bounds.Intersects(bounds);
            }

            public static bool BoundsIntersects(this Collider collider, Collider other)
            {
                return collider.bounds.Intersects(other.bounds);
            }

            public static bool RaycastFromCollider(this Collider collider, Vector3 direction, out RaycastHit hit, float maxDistance = Mathf.Infinity)
            {
                return Physics.Raycast(collider.transform.position, direction, out hit, maxDistance);
            }

            public static bool RaycastFromCollider(this Collider collider, Vector3 direction, out RaycastHit hit, float maxDistance, int layerMask)
            {
                return Physics.Raycast(collider.transform.position, direction, out hit, maxDistance, layerMask);
            }

            public static bool RaycastFromCollider(this Collider collider, Vector3 direction, out RaycastHit hit, float maxDistance, int layerMask, QueryTriggerInteraction queryTriggerInteraction)
            {
                return Physics.Raycast(collider.transform.position, direction, out hit, maxDistance, layerMask, queryTriggerInteraction);
            }


            public static bool IsTouchingAnyCollider(this Collider collider)
            {
                Collider[] colliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents);
                return colliders.Length > 0;
            }

            public static bool IsTouchingAnyCollider(this Collider collider, int layerMask)
            {
                Collider[] colliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, Quaternion.identity, layerMask);
                return colliders.Length > 0;
            }

            public static bool IsTouchingColliderWithTag(this Collider collider, string tag)
            {
                Collider[] colliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents);
                foreach (Collider c in colliders)
                {
                    if (c.CompareTag(tag))
                        return true;
                }
                return false;
            }

            public static bool IsInLayer(this Collider collider, LayerMask layerMask)
            {
                return (layerMask.value & (1 << collider.gameObject.layer)) > 0;
            }


        }
    }
}