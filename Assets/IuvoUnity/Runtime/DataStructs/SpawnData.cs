using IuvoUnity.BaseClasses;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public class SpawnData : IDataStructBase
        {
            Vector3 position;
            Quaternion rotation;
            Vector3 scale;
            public Vector3 Position { get => position; set => position = value; }
            public Quaternion Rotation { get => rotation; set => rotation = value; }
            public Vector3 Scale { get => scale; set => scale = value; }
            public SpawnData() { position = Vector3.zero; rotation = Quaternion.identity; scale = Vector3.one; }
            public SpawnData(Vector3 position, Quaternion rotation, Vector3 scale)
            {
                this.position = position;
                this.rotation = rotation;
                this.scale = scale;
            }
            public SpawnData(Transform transform)
            {
                position = transform.position;
                rotation = transform.rotation;
                scale = transform.localScale;
            }

            public SpawnData(Vector3 position, Vector3 scale, RaycastHit hitInfo, Quaternion secondaryRotation)
            {
                this.position = position;
                this.rotation = Quaternion.LookRotation(hitInfo.normal) * secondaryRotation;
                this.scale = scale;

            }
        }
    }
}