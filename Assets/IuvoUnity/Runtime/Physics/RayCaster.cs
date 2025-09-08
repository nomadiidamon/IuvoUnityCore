using UnityEngine;

namespace IuvoUnity
{
    namespace _Physics
    {
        [System.Serializable]
        public class RayCastData
        {
            [SerializeField] public Vector3 checkOrigin = Vector3.zero;
            [SerializeField] public float distanceToCheck = 1.25f;
            [SerializeField] public Vector3 directionToCheck = Vector3.down;
            [SerializeField] public bool isTouching = false;
            [SerializeField] public Ray ray = new Ray();
            [SerializeField] public RaycastHit hit = new RaycastHit();
            [SerializeField] public LayerMask layerMask = new LayerMask();

            RayCastData()
            {
                checkOrigin = Vector3.zero;
                distanceToCheck = 1.25f;
                directionToCheck = Vector3.down;
                isTouching = false;
                ray = new Ray(checkOrigin, directionToCheck);
                hit = new RaycastHit();
                layerMask = LayerMask.GetMask("Default");
            }
        }

        [System.Serializable]
        public class RayCaster : MonoBehaviour
        {
            [SerializeField] RayCastData castData;

            public void Start()
            {
                castData = GetComponent<RayCastData>();
            }

            public void FixedUpdate()
            {
                castData.isTouching = Physics.Raycast(castData.checkOrigin, castData.directionToCheck, out castData.hit, castData.distanceToCheck, castData.layerMask);
            }

            public RayCastData Cast(RayCastData castData)
            {
                castData.isTouching = Physics.Raycast(castData.checkOrigin, castData.directionToCheck, out castData.hit, castData.distanceToCheck, castData.layerMask);
                return castData;
            }
        }
    }
}