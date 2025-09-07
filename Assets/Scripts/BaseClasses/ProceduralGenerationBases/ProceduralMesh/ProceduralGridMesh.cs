
using UnityEngine;

namespace IuvoUnity
{
    namespace ProceduralGeneration
    {

        [RequireComponent(typeof(MeshFilter))]
        public class ProceduralGridMesh : ProceduralMesh
        {
            [Header("Grid Settings")]
            public int width = 10;
            public int length = 10;
            public float cellSize = 2.0f;

            protected override void GenerateMesh()
            {
                for (int z = 0; z <= length; z++)
                {
                    for (int x = 0; x <= width; x++)
                    {
                        vertices.Add(new Vector3(x * cellSize, 0, z * cellSize));
                        uvs.Add(new Vector2((float)x / width, (float)z / length));
                    }
                }

                for (int z = 0; z < length; z++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int i = z * (width + 1) + x;

                        triangles.Add(i);
                        triangles.Add(i + width + 1);
                        triangles.Add(i + 1);

                        triangles.Add(i + 1);
                        triangles.Add(i + width + 1);
                        triangles.Add(i + width + 2);
                    }
                }

                if (mesh == null)
                {
                    mesh = new Mesh();
                    mesh.vertices = vertices.ToArray();
                    mesh.triangles = triangles.ToArray();
                    mesh.uv = uvs.ToArray();
                    mesh.RecalculateBounds();
                    mesh.RecalculateNormals();
                }
            }
        }
    }
}