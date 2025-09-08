using System.Collections;
using UnityEngine;


namespace IuvoUnity
{
    namespace _Extensions
    {

        /// <summary>
        /// Extension methods for Unity's Rigidbody component, providing utility methods for velocity, forces, rotation, and movement control.
        /// </summary>
        public static class RigidbodyExtensions
        {
            #region Velocity Setters

            /// <summary>
            /// Sets the X component of the Rigidbody's velocity.
            /// </summary>
            /// <param name="rb">The Rigidbody to modify.</param>
            /// <param name="velocity">The X velocity to set.</param>
            public static void SetVelocityX(this Rigidbody rb, float velocity)
            {
                rb.linearVelocity = new Vector3(velocity, rb.linearVelocity.y, rb.linearVelocity.z);
            }

            /// <summary>
            /// Sets the Y component of the Rigidbody's velocity.
            /// </summary>
            /// <param name="rb">The Rigidbody to modify.</param>
            /// <param name="velocity">The Y velocity to set.</param>
            public static void SetVelocityY(this Rigidbody rb, float velocity)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, velocity, rb.linearVelocity.z);
            }

            /// <summary>
            /// Sets the Z component of the Rigidbody's velocity.
            /// </summary>
            /// <param name="rb">The Rigidbody to modify.</param>
            /// <param name="velocity">The Z velocity to set.</param>
            public static void SetVelocityZ(this Rigidbody rb, float velocity)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, velocity);
            }

            /// <summary>
            /// Sets the Rigidbody's velocity to a target vector.
            /// </summary>
            /// <param name="rb">The Rigidbody to modify.</param>
            /// <param name="targetVelocity">The new velocity vector.</param>
            public static void SetVelocityTo(this Rigidbody rb, Vector3 targetVelocity)
            {
                rb.linearVelocity = targetVelocity;
            }

            /// <summary>
            /// Sets a random velocity with speed between the specified minimum and maximum.
            /// </summary>
            /// <param name="rb">The Rigidbody to modify.</param>
            /// <param name="minSpeed">Minimum speed.</param>
            /// <param name="maxSpeed">Maximum speed.</param>
            public static void SetRandomVelocity(this Rigidbody rb, float minSpeed, float maxSpeed)
            {
                float speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
                rb.linearVelocity = UnityEngine.Random.onUnitSphere * speed;
            }

            /// <summary>
            /// Resets the Rigidbody's linear velocity to zero.
            /// </summary>
            /// <param name="rb">The Rigidbody to modify.</param>
            public static void ResetVelocity(this Rigidbody rb)
            {
                rb.linearVelocity = Vector3.zero;
            }

            /// <summary>
            /// Resets the Rigidbody's angular velocity to zero.
            /// </summary>
            /// <param name="rb">The Rigidbody to modify.</param>
            public static void ResetAngularVelocity(this Rigidbody rb)
            {
                rb.angularVelocity = Vector3.zero;
            }

            #endregion

            #region Force and Impulse

            /// <summary>
            /// Applies a damped force to the Rigidbody.
            /// </summary>
            /// <param name="rb">The Rigidbody to apply force to.</param>
            /// <param name="force">The base force vector.</param>
            /// <param name="damping">The damping factor to reduce the force.</param>
            public static void AddDampedForce(this Rigidbody rb, Vector3 force, float damping)
            {
                rb.AddForce(force * damping);
            }

            /// <summary>
            /// Applies an impulse force at a specific point.
            /// </summary>
            /// <param name="rb">The Rigidbody to apply the impulse to.</param>
            /// <param name="impulse">The impulse vector.</param>
            /// <param name="point">The world position where the impulse is applied.</param>
            public static void ApplyImpulseAtPoint(this Rigidbody rb, Vector3 impulse, Vector3 point)
            {
                rb.AddForceAtPosition(impulse, point, ForceMode.Impulse);
            }

            /// <summary>
            /// Applies a force scaled by the Rigidbody's mass.
            /// </summary>
            /// <param name="rb">The Rigidbody to apply force to.</param>
            /// <param name="force">The base force vector.</param>
            public static void ApplyForceByMass(this Rigidbody rb, Vector3 force)
            {
                rb.AddForce(force * rb.mass, ForceMode.Force);
            }

            /// <summary>
            /// Applies an upward impulse to simulate a jump.
            /// </summary>
            /// <param name="rb">The Rigidbody to apply the force to.</param>
            /// <param name="jumpForce">The magnitude of the jump force.</param>
            public static void ApplyJumpForce(this Rigidbody rb, float jumpForce)
            {
                rb.AddForce(Vector3.up * jumpForce * rb.mass, ForceMode.Impulse);
            }

            /// <summary>
            /// Applies force in the direction of current velocity.
            /// </summary>
            /// <param name="rb">The Rigidbody to modify.</param>
            /// <param name="forceAmount">The magnitude of the force to apply.</param>
            public static void ApplyForceInDirectionOfVelocity(this Rigidbody rb, float forceAmount)
            {
                if (rb.linearVelocity.sqrMagnitude > 0.01f)
                    rb.AddForce(rb.linearVelocity.normalized * forceAmount, ForceMode.VelocityChange);
            }

            /// <summary>
            /// Applies a knockback impulse away from a given impact point.
            /// </summary>
            /// <param name="rb">The Rigidbody to knock back.</param>
            /// <param name="impactPoint">The point of impact.</param>
            /// <param name="knockbackStrength">The strength of the knockback.</param>
            public static void ApplyKnockbackForce(this Rigidbody rb, Vector3 impactPoint, float knockbackStrength)
            {
                Vector3 direction = (rb.position - impactPoint).normalized;
                rb.AddForce(direction * knockbackStrength, ForceMode.Impulse);
            }

            /// <summary>
            /// Applies random rotational torque to the Rigidbody.
            /// </summary>
            /// <param name="rb">The Rigidbody to apply torque to.</param>
            /// <param name="torqueAmount">The amount of torque to apply.</param>
            public static void ApplyRandomTorque(this Rigidbody rb, float torqueAmount)
            {
                rb.AddTorque(UnityEngine.Random.onUnitSphere * torqueAmount, ForceMode.Impulse);
            }

            #endregion

            #region Rotation and Orientation

            /// <summary>
            /// Rotates the Rigidbody to face a target direction over time.
            /// </summary>
            /// <param name="rb">The Rigidbody to rotate.</param>
            /// <param name="targetDirection">The world direction to face.</param>
            /// <param name="speed">Rotation speed in radians per second.</param>
            public static void RotateToAngle(this Rigidbody rb, Vector3 targetDirection, float speed)
            {
                Vector3 newDirection = Vector3.RotateTowards(rb.transform.forward, targetDirection, speed * Time.deltaTime, 0f);
                rb.rotation = Quaternion.LookRotation(newDirection);
            }

            /// <summary>
            /// Rotates the Rigidbody using torque to align with a target position.
            /// </summary>
            /// <param name="rb">The Rigidbody to rotate.</param>
            /// <param name="targetPosition">The world position to look at.</param>
            /// <param name="torqueAmount">The torque strength to apply.</param>
            public static void RotateToAlignWithTarget(this Rigidbody rb, Vector3 targetPosition, float torqueAmount)
            {
                Vector3 directionToTarget = (targetPosition - rb.position).normalized;
                Vector3 torque = Vector3.Cross(rb.transform.forward, directionToTarget);
                rb.AddTorque(torque * torqueAmount);
            }

            /// <summary>
            /// Applies continuous torque around the Y-axis.
            /// </summary>
            /// <param name="rb">The Rigidbody to spin.</param>
            /// <param name="torqueAmount">The amount of Y-axis torque to apply.</param>
            public static void ApplySpin(this Rigidbody rb, float torqueAmount)
            {
                rb.AddTorque(Vector3.up * torqueAmount, ForceMode.VelocityChange);
            }

            #endregion

            #region Utility and Control

            /// <summary>
            /// Directly sets the Rigidbody's position.
            /// </summary>
            /// <param name="rb">The Rigidbody to move.</param>
            /// <param name="position">The new world position.</param>
            public static void SetPositionDirectly(this Rigidbody rb, Vector3 position)
            {
                rb.position = position;
            }

            /// <summary>
            /// Checks whether the Rigidbody is currently moving.
            /// </summary>
            /// <param name="rb">The Rigidbody to check.</param>
            /// <returns>True if velocity magnitude is greater than 0.1.</returns>
            public static bool IsMoving(this Rigidbody rb)
            {
                return rb.linearVelocity.magnitude > 0.1f;
            }

            /// <summary>
            /// Checks whether the Rigidbody is currently moving downwards.
            /// </summary>
            /// <param name="rb">The Rigidbody to check.</param>
            /// <returns>True if Y velocity is less than 0.</returns>
            public static bool IsMovingDown(this Rigidbody rb)
            {
                return rb.linearVelocity.y < 0;
            }

            /// <summary>
            /// Gets the speed in the forward direction of the Rigidbody.
            /// </summary>
            /// <param name="rb">The Rigidbody to evaluate.</param>
            /// <returns>The speed in forward direction (dot product).</returns>
            public static float GetSpeedInForwardDirection(this Rigidbody rb)
            {
                return Vector3.Dot(rb.linearVelocity, rb.transform.forward);
            }

            /// <summary>
            /// Restricts Rigidbody movement to a single direction.
            /// </summary>
            /// <param name="rb">The Rigidbody to modify.</param>
            /// <param name="direction">The direction to allow movement in.</param>
            public static void FreezeMovementInDirection(this Rigidbody rb, Vector3 direction)
            {
                rb.linearVelocity = Vector3.Project(rb.linearVelocity, direction.normalized);
            }

            /// <summary>
            /// Stops the Rigidbody when it reaches a specified position.
            /// </summary>
            /// <param name="rb">The Rigidbody to stop.</param>
            /// <param name="position">The target position.</param>
            public static void StopAtPosition(this Rigidbody rb, Vector3 position)
            {
                if (Vector3.Distance(rb.position, position) < 0.1f)
                {
                    rb.linearVelocity = Vector3.zero;
                }
            }

            /// <summary>
            /// Applies an impulse opposite to current velocity to bring Rigidbody to a stop.
            /// </summary>
            /// <param name="rb">The Rigidbody to stop.</param>
            public static void ApplyStopImpulse(this Rigidbody rb)
            {
                rb.AddForce(-rb.linearVelocity, ForceMode.Impulse);
            }

            /// <summary>
            /// Resets the Rigidbody's position, velocity, and angular velocity.
            /// </summary>
            /// <param name="rb">The Rigidbody to reset.</param>
            public static void ResetAll(this Rigidbody rb)
            {
                rb.position = Vector3.zero;
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            #endregion

            #region Special and Coroutine Helpers

            /// <summary>
            /// Temporarily disables gravity on a Rigidbody for a set duration.
            /// </summary>
            /// <param name="rb">The Rigidbody to modify.</param>
            /// <param name="time">Duration in seconds before restoring gravity.</param>
            /// <param name="context">A MonoBehaviour context to start the coroutine.</param>
            /// <remarks>This method requires a MonoBehaviour to start the coroutine.</remarks>
            public static void IgnoreGravityForTime(this Rigidbody rb, float time, MonoBehaviour context)
            {
                context.StartCoroutine(RestoreGravityAfterTime(rb, time));
            }

            private static IEnumerator RestoreGravityAfterTime(Rigidbody rb, float time)
            {
                rb.useGravity = false;
                yield return new WaitForSeconds(time);
                rb.useGravity = true;
            }

            #endregion
        }
    }

}
