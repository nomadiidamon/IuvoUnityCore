using UnityEngine;

namespace IuvoUnity
{
    namespace _Physics
    {
        [System.Serializable ]
        public class GroundCheck : MonoBehaviour
        {
            [SerializeField] public Vector3 checkOrigin { get; set; }
            [SerializeField] public float radiusToCheck { get; set; } = 0.5f;
            [SerializeField] public float distanceToCheck { get; set; } = 1.25f;
            [SerializeField] public Vector3 directionToCheck { get; set; } = Vector3.down;

            public bool isGrounded { get; private set; }

            void FixedUpdate()
            {
                checkOrigin = transform.position;
                isGrounded = Physics.SphereCast(checkOrigin, radiusToCheck, Vector3.down, out _, distanceToCheck);
            }
        }
    }
}