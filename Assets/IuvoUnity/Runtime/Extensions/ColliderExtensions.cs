using UnityEngine;

namespace IuvoUnity
{
    namespace Extensions
    {
        public static class ColliderExtensions
        {
            public static bool Overlaps(this Collider collider, Collider other)
            {
                return collider.bounds.Intersects(other.bounds);
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
                return collider.BoundsContainsPoint(bounds.center);
            }

            public static bool BoundsIntersects(this Collider collider, Collider other)
            {
                return collider.Overlaps(other);
            }

            public static bool IsTouchingAnyCollider(this Collider collider, out Collider[] colls)
            {
                Collider[] colliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents);
                colls = colliders;
                return colliders.Length > 0;
            }

            public static bool IsTouchingAnyCollider(this Collider collider, int layerMask, out Collider[] colls)
            {
                Collider[] colliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents, Quaternion.identity, layerMask);
                colls = colliders;
                return colliders.Length > 0;
            }

            public static bool IsTouchingColliderWithTag(this Collider collider, string tag, out Collider[] colls)
            {
                Collider[] colliders = Physics.OverlapBox(collider.bounds.center, collider.bounds.extents);
                Collider[] foundColls = System.Array.FindAll(colliders, c => c.CompareTag(tag));
                if (foundColls.Length > 0)
                {
                    colls = foundColls;
                    return true;
                }
                colls = null;
                return false;
            }


            public static bool IsInLayer(this Collider collider, LayerMask layerMask)
            {
                return (layerMask.value & (1 << collider.gameObject.layer)) > 0;
            }


        }
    }
}