using UnityEngine;

namespace IuvoUnity
{
    namespace Singletons
    {
        /// <summary>
        /// Global gravity manager for all gravity bodies.
        /// </summary>
        public class GravityManager : PersistentSingleton<GravityManager>
        {

            [SerializeField] private Vector3 defaultDirection = Vector3.down;
            [SerializeField] private float defaultStrength = 9.81f;

            public Vector3 gravityDirection { get; private set; } = Vector3.zero;
            public float gravityStrength { get; private set; } = 0.0f;

            protected override void Awake()
            {
                base.Awake();
            }

            public Vector3 GravityDirection()
            {
                return gravityDirection.Equals(Vector3.zero) ? defaultDirection.normalized : gravityDirection.normalized;
            }

            public void SetGravityDirection(Vector3 direction)
            {
                defaultDirection = direction.normalized;
            }

            public float GravityStrength()
            {
                return gravityStrength == 0.0f ? defaultStrength : gravityStrength;
            }

            public void SetGlobalStrength(float strength)
            {
                defaultStrength = strength;
            }

        }
    }
}