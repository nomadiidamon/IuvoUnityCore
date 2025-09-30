using UnityEngine;

namespace IuvoUnity
{
    namespace IuvoPhysics
    {
        [System.Serializable]
        [RequireComponent(typeof(BoxCollider))]
        public class GravityZone : MonoBehaviour
        {
            BoxCollider m_BoxCollider;

            [Tooltip("Gravity strength within this zone.")]
            public float zoneGravityStrength = 9.81f;

            [Tooltip("If true, gravity pulls away from center instead of toward it.")]
            public bool reverseGravity = false;

            void Awake()
            {
                m_BoxCollider = GetComponent<BoxCollider>();
                m_BoxCollider.isTrigger = true;
            }

            void OnTriggerEnter(Collider other)
            {
                if (other.attachedRigidbody == null) return;

                if (other.attachedRigidbody.TryGetComponent(out GravityBody body))
                {
                    body.EnterGravityZone(this);
                }
            }

            void OnTriggerExit(Collider other)
            {
                if (other.attachedRigidbody == null) return;

                if (other.attachedRigidbody.TryGetComponent(out GravityBody body))
                {
                    body.ExitGravityZone();
                }
            }

            public Vector3 GetGravityDirection(Vector3 objectPosition)
            {
                Vector3 direction = (transform.position - objectPosition).normalized;
                return reverseGravity ? -direction : direction;
            }

            void OnDrawGizmos()
            {
                Gizmos.color = reverseGravity ? Color.red : Color.green;
                Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
            }
        }
    }
}