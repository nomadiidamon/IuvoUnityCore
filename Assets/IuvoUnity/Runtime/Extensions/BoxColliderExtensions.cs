using System.Collections.Generic;
using UnityEngine;
using IuvoUnity.Debug;

namespace IuvoUnity
{
    namespace Extensions
    {

        public static class BoxColliderExtensions
        {

            /// <summary>
            /// Snaps the source GameObject (with a BoxCollider) to a target GameObject (also with a BoxCollider)
            /// in the specified direction (Vector3.right, Vector3.left, etc.).
            /// </summary>
            public static void SnapTo(this GameObject source, GameObject target, Vector3 direction)
            {
                if (source == null || target == null) return;

                BoxCollider sourceCol = source.GetComponent<BoxCollider>();
                BoxCollider targetCol = target.GetComponent<BoxCollider>();

                if (sourceCol == null || targetCol == null)
                {
                    IuvoDebug.DebugLogWarning("Both GameObjects must have a BoxCollider.");
                    return;
                }

                Bounds sourceBounds = sourceCol.bounds;
                Bounds targetBounds = targetCol.bounds;

                Vector3 newPosition = source.transform.position;

                if (direction == Vector3.right)
                {
                    float sourceOffset = sourceBounds.extents.x;
                    float targetEdge = targetBounds.max.x;
                    newPosition.x = targetEdge + sourceOffset;
                }
                else if (direction == Vector3.left)
                {
                    float sourceOffset = sourceBounds.extents.x;
                    float targetEdge = targetBounds.min.x;
                    newPosition.x = targetEdge - sourceOffset;
                }
                else if (direction == Vector3.up)
                {
                    float sourceOffset = sourceBounds.extents.y;
                    float targetEdge = targetBounds.max.y;
                    newPosition.y = targetEdge + sourceOffset;
                }
                else if (direction == Vector3.down)
                {
                    float sourceOffset = sourceBounds.extents.y;
                    float targetEdge = targetBounds.min.y;
                    newPosition.y = targetEdge - sourceOffset;
                }
                else if (direction == Vector3.forward)
                {
                    float sourceOffset = sourceBounds.extents.z;
                    float targetEdge = targetBounds.max.z;
                    newPosition.z = targetEdge + sourceOffset;
                }
                else if (direction == Vector3.back)
                {
                    float sourceOffset = sourceBounds.extents.z;
                    float targetEdge = targetBounds.min.z;
                    newPosition.z = targetEdge - sourceOffset;
                }
                else
                {
                    IuvoDebug.DebugLogWarning("Unsupported direction. Use Vector3.left/right/up/down/forward/back.");
                    return;
                }

                source.transform.position = newPosition;
            }

            public static bool IsTouching(this BoxCollider col, BoxCollider other, float tolerance = 0.01f)
            {
                return col.GetWorldBounds().Intersects(other.GetWorldBounds()) ||
                       col.GetWorldBounds().SqrDistance(other.GetWorldBounds().center) <= tolerance * tolerance;
            }

            public static Vector3[] GetWorldCorners(this BoxCollider box)
            {
                Vector3 center = box.transform.TransformPoint(box.center);
                Vector3 size = Vector3.Scale(box.size, box.transform.lossyScale);
                Vector3 half = size * 0.5f;

                Vector3[] localCorners = new Vector3[8]
                {
                    new Vector3(-half.x, -half.y, -half.z),
                    new Vector3(-half.x, -half.y,  half.z),
                    new Vector3(-half.x,  half.y, -half.z),
                    new Vector3(-half.x,  half.y,  half.z),
                    new Vector3( half.x, -half.y, -half.z),
                    new Vector3( half.x, -half.y,  half.z),
                    new Vector3( half.x,  half.y, -half.z),
                    new Vector3( half.x,  half.y,  half.z),
                };

                Vector3[] worldCorners = new Vector3[8];
                for (int i = 0; i < 8; i++)
                {
                    worldCorners[i] = box.transform.TransformPoint(box.center + localCorners[i]);
                }

                return worldCorners;
            }

            public static bool IsPointInside(this BoxCollider box, Vector3 point)
            {
                Vector3[] corners = box.GetWorldCorners();
                Vector3 min = new Vector3(
                    Mathf.Min(corners[0].x, corners[1].x, corners[2].x, corners[3].x, corners[4].x, corners[5].x, corners[6].x, corners[7].x),
                    Mathf.Min(corners[0].y, corners[1].y, corners[2].y, corners[3].y, corners[4].y, corners[5].y, corners[6].y, corners[7].y),
                    Mathf.Min(corners[0].z, corners[1].z, corners[2].z, corners[3].z, corners[4].z, corners[5].z, corners[6].z, corners[7].z)
                );

                Vector3 max = new Vector3(
                    Mathf.Max(corners[0].x, corners[1].x, corners[2].x, corners[3].x, corners[4].x, corners[5].x, corners[6].x, corners[7].x),
                    Mathf.Max(corners[0].y, corners[1].y, corners[2].y, corners[3].y, corners[4].y, corners[5].y, corners[6].y, corners[7].y),
                    Mathf.Max(corners[0].z, corners[1].z, corners[2].z, corners[3].z, corners[4].z, corners[5].z, corners[6].z, corners[7].z)
                );

                return point.x >= min.x && point.x <= max.x &&
                       point.y >= min.y && point.y <= max.y &&
                       point.z >= min.z && point.z <= max.z;
            }

            public static bool IsOverlappingWith(this BoxCollider source, BoxCollider target)
            {
                Bounds sourceBounds = source.GetWorldBounds();
                Bounds targetBounds = target.GetWorldBounds();

                return sourceBounds.Intersects(targetBounds);
            }

            // Entry point: Combine all BoxColliders under a root into a single mesh GameObject
            public static GameObject CombineBoxCollidersToMesh(this GameObject source, Material optionalMaterial = null)
            {
                var colliders = source.GetComponentsInChildren<BoxCollider>();
                if (colliders.Length == 0)
                {
                    IuvoDebug.DebugLogWarning("No BoxColliders found to combine.");
                    return null;
                }

                List<CombineInstance> combineInstances = new List<CombineInstance>();

                foreach (var col in colliders)
                {
                    CombineInstance instance = CreateCombineInstanceFromBox(col);
                    combineInstances.Add(instance);
                }

                return CreateCombinedMeshObject(source.name + "_Combined", combineInstances, optionalMaterial);
            }

            // Converts a single BoxCollider into a CombineInstance (using a cube mesh)
            private static CombineInstance CreateCombineInstanceFromBox(BoxCollider col)
            {
                Mesh cubeMesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx"); // Unity's default cube
                CombineInstance ci = new CombineInstance
                {
                    mesh = cubeMesh,
                    transform = Matrix4x4.TRS(
                        col.transform.TransformPoint(col.center),
                        col.transform.rotation,
                        Vector3.Scale(col.size, col.transform.lossyScale)
                    )
                };
                return ci;
            }

            // Combines a list of CombineInstances into one GameObject with MeshFilter + MeshRenderer
            private static GameObject CreateCombinedMeshObject(string name, List<CombineInstance> combineInstances, Material optionalMaterial)
            {
                GameObject go = new GameObject(name);

                Mesh combinedMesh = new Mesh();
                combinedMesh.CombineMeshes(combineInstances.ToArray());

                var meshFilter = go.AddComponent<MeshFilter>();
                meshFilter.mesh = combinedMesh;

                var meshRenderer = go.AddComponent<MeshRenderer>();
                meshRenderer.material = optionalMaterial ?? new Material(Shader.Find("Standard"));

                return go;
            }

            public static Bounds GetWorldBounds(this BoxCollider col)
            {
                Vector3 worldCenter = col.transform.TransformPoint(col.center);
                Vector3 worldSize = Vector3.Scale(col.size, col.transform.lossyScale);
                return new Bounds(worldCenter, worldSize);
            }

            public static Vector3 GetWorldSize(this BoxCollider col)
            {
                return Vector3.Scale(col.size, col.transform.lossyScale);
            }

        }

    }
}
