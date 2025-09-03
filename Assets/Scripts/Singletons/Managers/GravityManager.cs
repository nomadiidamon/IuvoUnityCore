using UnityEngine;

namespace IuvoUnity
{
    namespace Singletons
    {
        namespace Managers
        {
            public class GravityManager : MonoBehaviour
            {
                public static GravityManager Instance;

                [SerializeField] private Vector3 defaultDirection = Vector3.down;
                [SerializeField] private float defaultStrength = 9.81f;

                void Awake()
                {
                    if (Instance == null) Instance = this;
                    else Destroy(gameObject);
                }

                public Vector3 GetGlobalGravity()
                {
                    return defaultDirection.normalized * defaultStrength;
                }

                public void SetGlobalGravity(Vector3 direction, float strength)
                {
                    defaultDirection = direction.normalized;
                    defaultStrength = strength;
                }

                public float GetGlobalStrength()
                {

                    return defaultStrength;

                }

                public void SetGlobalStrength(float strength)
                {
                    defaultStrength = strength;

                }

            }
        }
    }
}