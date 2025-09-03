using IuvoUnity._Physics;
using IuvoUnity.Singletons.Managers;
using UnityEngine;
using IuvoUnity.Extensions;

namespace IuvoUnity
{
    namespace _Input
    {
        namespace _Controllers
        {
            [System.Serializable]
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

                // Manual velocity tracking
                protected Vector3 velocity = Vector3.zero;

                protected virtual void Awake()
                {
                    if (groundCheck == null)
                    {
                        groundCheck = gameObject.GetOrAdd<GroundCheck>();
                        groundCheck.checkOrigin = checkPosition;
                    }
                }

                protected virtual void FixedUpdate()
                {
                    if (!useGravity) return;
                    if (onlyApplyGravityWhenAirborne && groundCheck != null && groundCheck.isGrounded)
                    {
                        velocity = Vector3.zero; // reset velocity when grounded
                        return;
                    }

                    Vector3 gravity = CalculateGravity();
                    ApplyGravity(gravity);
                    AlignToGravity(gravity);
                }

                protected virtual Vector3 CalculateGravity()
                {
                    if (gravZone != null)
                    {
                        currGravDirection = gravZone.GetGravityDirection(transform.position);
                        currGravStrength = gravZone.zoneGravityStrength;
                        groundCheck.directionToCheck = currGravDirection.normalized;
                        groundCheck.checkOrigin = checkPosition;

                        return currGravDirection.normalized * currGravStrength;
                    }

                    if (useGlobalGravity && GravityManager.Instance != null)
                    {
                        currGravDirection = GravityManager.Instance.GetGlobalGravity();
                        currGravStrength = GravityManager.Instance.GetGlobalStrength();
                        groundCheck.directionToCheck = currGravDirection.normalized;
                        groundCheck.checkOrigin = checkPosition;

                        return currGravDirection.normalized * currGravStrength;
                    }

                    currGravDirection = customDirection.normalized;
                    currGravStrength = customStrength;
                    groundCheck.directionToCheck = currGravDirection.normalized;
                    groundCheck.checkOrigin = checkPosition;

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
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 5f);
                }

                public void SetLocalGravity(Vector3 direction, float strength)
                {
                    useGlobalGravity = false;
                    customDirection = direction.normalized;
                    customStrength = strength;
                }

                public void EnableGlobalGravity() => useGlobalGravity = true;
                public void DisableGravity() => useGravity = false;
                public void EnableGravity() => useGravity = true;

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
}
