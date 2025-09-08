using IuvoUnity._Physics;
using UnityEngine;

namespace IuvoUnity.src._Physics
{
    [System.Serializable]
    public class SphereCastData
    {
        [SerializeField] public Vector3 checkOrigin = Vector3.zero;
        [SerializeField] public float distanceToCheck = 1.25f;
        [SerializeField] public float radiusToCheck = 1.0f;
        [SerializeField] public Vector3 directionToCheck = Vector3.down;
        [SerializeField] public bool isTouching = false;
        [SerializeField] public Ray ray = new Ray();
        [SerializeField] public RaycastHit hit = new RaycastHit();
        [SerializeField] public LayerMask layerMask = new LayerMask();

        SphereCastData()
        {
            checkOrigin = Vector3.zero;
            distanceToCheck = 1.25f;
            radiusToCheck = 1.0f;
            directionToCheck = Vector3.down;
            isTouching = false;
            ray = new Ray(checkOrigin, directionToCheck);
            hit = new RaycastHit();
            layerMask = LayerMask.GetMask("Default");
        }
    }

    [System.Serializable]
    public class SphereCaster : MonoBehaviour
    {
        [SerializeField] SphereCastData castData;

        public void Start()
        {
            castData = GetComponent<SphereCastData>();
        }

        public void FixedUpdate()
        {
            castData.isTouching = Physics.SphereCast(castData.checkOrigin, castData.radiusToCheck, castData.directionToCheck, out castData.hit, castData.distanceToCheck, castData.layerMask);
        }

        public SphereCastData Cast(SphereCastData castData)
        {
            castData.isTouching = Physics.SphereCast(castData.checkOrigin, castData.radiusToCheck, castData.directionToCheck, out castData.hit, castData.distanceToCheck, castData.layerMask);
            return castData;
        }
    }
}
