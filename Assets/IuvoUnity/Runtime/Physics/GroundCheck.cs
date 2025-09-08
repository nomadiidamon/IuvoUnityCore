using Unity.VisualScripting;
using UnityEngine;

namespace IuvoUnity
{
    namespace IuvoPhysics
    {
        [System.Serializable]
        public class GroundCheck : MonoBehaviour
        {
            [SerializeField] private Vector3 checkOrigin = Vector3.zero;
            [SerializeField] private float radiusToCheck = 0.5f;
            [Tooltip("Distance to check in the specified direction. Will always require tweaking based on ground and object variances")]
            [SerializeField] private float distanceToCheck = 0.5f;
            [SerializeField] private Vector3 directionToCheck = Vector3.down;
            [SerializeField] private Vector3 velocity = Vector3.zero;

            #region Getters & Setters
            public void SetCheckOrigin(Vector3 newOrigin) { checkOrigin = newOrigin; }
            public void SetRadiusToCheck(float newRadius) { radiusToCheck = newRadius; }
            public void SetDistanceToCheck(float newDistance) { distanceToCheck = newDistance; }
            public void SetDirectionToCheck(Vector3 newDirection) { directionToCheck = newDirection.normalized; }
            public void SetVelocity(Vector3 newVelocity) { velocity = newVelocity; }
            public Vector3 GetCheckOrigin() { return checkOrigin; }
            public float GetRadiusToCheck() { return radiusToCheck; }
            public float GetDistanceToCheck() { return distanceToCheck; }
            public Vector3 GetDirectionToCheck() { return directionToCheck; }
            public Vector3 GetVelocity() { return velocity; }
            public bool Grounded; 
            #endregion

            private bool isGrounded = false;

            void FixedUpdate()
            {

                if ( Physics.SphereCast(checkOrigin, radiusToCheck, directionToCheck, out _, distanceToCheck))
                {
                    UnityEngine.Debug.DrawRay(checkOrigin, directionToCheck * distanceToCheck, Color.green);
                    isGrounded = true;
                }
                else
                {
                    UnityEngine.Debug.DrawRay(checkOrigin, directionToCheck * distanceToCheck, Color.red);
                    isGrounded = false;
                }
                Grounded = isGrounded;


                // predict next frame position
                checkOrigin += velocity * Time.fixedDeltaTime;

                if (Physics.SphereCast(checkOrigin, radiusToCheck, directionToCheck, out _, distanceToCheck))
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }

                Grounded = isGrounded;
                checkOrigin -= velocity * Time.fixedDeltaTime; // reset position

            }

            public void ForceGroundCheck()
            {
                if (Physics.SphereCast(checkOrigin, radiusToCheck, directionToCheck, out _, distanceToCheck))
                {
                    UnityEngine.Debug.DrawRay(checkOrigin, directionToCheck * distanceToCheck, Color.green);
                    isGrounded = true;
                }
                else
                {
                    UnityEngine.Debug.DrawRay(checkOrigin, directionToCheck * distanceToCheck, Color.red);
                    isGrounded = false;
                }
                Grounded = isGrounded;


                // predict next frame position
                checkOrigin += velocity * Time.fixedDeltaTime;

                if (Physics.SphereCast(checkOrigin, radiusToCheck, directionToCheck, out _, distanceToCheck))
                {
                    isGrounded = true;
                }
                else
                {
                    isGrounded = false;
                }

                Grounded = isGrounded;
                checkOrigin -= velocity * Time.fixedDeltaTime; // reset position
            }
        }
    }
}