using IuvoUnity.BaseClasses;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public struct TransformData : IDataStructBase
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;

        }
    }
}