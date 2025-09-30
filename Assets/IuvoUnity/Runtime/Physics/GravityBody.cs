using IuvoUnity.Singletons;
using UnityEngine;
using IuvoUnity.Extensions;

namespace IuvoUnity
{
    namespace IuvoPhysics
    {
        [System.Serializable]
        [RequireComponent(typeof(GroundCheck))]
        public class GravityBody : MonoBehaviour
        {
            protected GravityZone gravZone;

            [Header("Gravity Settings")]
            public bool useGravity = true;
            public bool useGlobalGravity = true;
            [Space(3)]
            public Vector3 customDirection = Vector3.down;
            public Vector3 currGravDirection = Vector3.zero;
            [Space(1)]
            public float customStrength = 9.81f;
            public float currGravStrength = 0.0f;

            [Header("Ground Detection")]
            public Vector3 checkPosition = Vector3.zero;

            public GroundCheck groundCheck;
            public bool onlyApplyGravityWhenAirborne = true;
            public bool Grounded;

            // Manual velocity tracking
            protected Vector3 velocity = Vector3.zero;

            protected virtual void Awake()
            {
                checkPosition = transform.position;
                groundCheck = gameObject.GetOrAdd<GroundCheck>();
                groundCheck.SetCheckOrigin(checkPosition);
                groundCheck.SetDirectionToCheck(customDirection.normalized);
            }

            protected virtual void FixedUpdate()
            {

                if (!useGravity) return;
                if (onlyApplyGravityWhenAirborne && groundCheck != null && groundCheck.Grounded)
                {
                    velocity = Vector3.zero; // reset velocity when grounded
                    Grounded = true;
                    return;
                }

                Vector3 gravity = CalculateGravity();
                ApplyGravity(gravity);
                AlignToGravity(gravity);
                if (groundCheck != null)
                {
                    Grounded = groundCheck.Grounded;
                }
            }

            protected virtual Vector3 CalculateGravity()
            {
                checkPosition = transform.position;
                groundCheck.SetCheckOrigin(checkPosition);

                if (gravZone != null)
                {
                    currGravDirection = gravZone.GetGravityDirection(transform.position);
                    currGravStrength = gravZone.zoneGravityStrength;
                    groundCheck.SetDirectionToCheck(currGravDirection.normalized);

                    return currGravDirection.normalized * currGravStrength;
                }

                if (useGlobalGravity && GravityManager.Instance != null)
                {
                    currGravDirection = GravityManager.Instance.GravityDirection();
                    currGravStrength = GravityManager.Instance.GravityStrength();
                    groundCheck.SetDirectionToCheck(currGravDirection.normalized);

                    return currGravDirection.normalized * currGravStrength;
                }

                currGravDirection = customDirection.normalized;
                currGravStrength = customStrength;
                groundCheck.SetDirectionToCheck(currGravDirection.normalized);
                
                return customDirection.normalized * customStrength;
            }

            protected virtual void ApplyGravity(Vector3 gravity)
            {
                velocity += gravity * Time.fixedDeltaTime;
                transform.position += velocity * Time.fixedDeltaTime;
            }

            protected virtual void AlignToGravity(Vector3 gravity)
            {
                Vector3 gravityDirection = -gravity.normalized;
                Quaternion targetRotation = Quaternion.FromToRotation(transform.up, gravityDirection) * transform.rotation;
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
            }

            public void SetLocalGravity(Vector3 direction, float strength)
            {
                useGlobalGravity = false;
                customDirection = direction.normalized;
                customStrength = strength;
            }

            public void EnableGlobalGravity() => useGlobalGravity = true;
            public void ToggleGravity(bool state) => useGravity = state;
            public void EnterGravityZone(GravityZone zone) => gravZone = zone;
            public void ExitGravityZone() => gravZone = null;

            public virtual void OnDrawGizmosSelected()
            {
                if (!Application.isPlaying || !useGravity) return;

                Vector3 gravity = CalculateGravity();

                Gizmos.color = Color.cyan;
                Gizmos.DrawRay(transform.position, gravity);

                Gizmos.color = Color.magenta;
                Gizmos.DrawRay(transform.position, -gravity.normalized * 2f);
            }
        }
    }
}
