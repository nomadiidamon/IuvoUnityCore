using UnityEngine;


namespace IuvoUnity
{
    namespace ProceduralGeneration
    {

        public class ProceduralCylinder : ProceduralMesh
        {
            [Header("Mesh Settings")]
            public int segments = 16;
            public int slices = 1;
            public float segmentHeight = 1f;
            public float radius = 0.5f;

            protected override void GenerateMesh()
            {
                float angleStep = 360f / segments;

                // Bottom and top circle vertices
                for (int i = 0; i <= segments; i++)
                {
                    float angle = Mathf.Deg2Rad * angleStep * i;
                    float x = Mathf.Cos(angle) * radius;
                    float z = Mathf.Sin(angle) * radius;

                    vertices.Add(new Vector3(x, 0, z)); // Bottom ring
                    vertices.Add(new Vector3(x, segmentHeight, z)); // Top ring

                    uvs.Add(new Vector2((float)i / segments, 0));
                    uvs.Add(new Vector2((float)i / segments, 1));
                }

                // Side triangles
                for (int i = 0; i < segments * 2; i += 2)
                {
                    triangles.Add(i);
                    triangles.Add(i + 3);
                    triangles.Add(i + 1);

                    triangles.Add(i);
                    triangles.Add(i + 2);
                    triangles.Add(i + 3);
                }

                // Cap centers
                int bottomCenterIndex = vertices.Count;
                vertices.Add(Vector3.zero); // Bottom center
                uvs.Add(new Vector2(0.5f, 0.5f));

                int topCenterIndex = vertices.Count;
                vertices.Add(new Vector3(0, segmentHeight, 0)); // Top center
                uvs.Add(new Vector2(0.5f, 0.5f));

                // Cap triangles
                for (int i = 0; i < segments; i++)
                {
                    int bottomOuter = i * 2;
                    int topOuter = i * 2 + 1;
                    int nextBottomOuter = ((i + 1) % segments) * 2;
                    int nextTopOuter = ((i + 1) % segments) * 2 + 1;

                    // Bottom cap
                    triangles.Add(bottomCenterIndex);
                    triangles.Add(nextBottomOuter);
                    triangles.Add(bottomOuter);

                    // Top cap
                    triangles.Add(topCenterIndex);
                    triangles.Add(topOuter);
                    triangles.Add(nextTopOuter);
                }
            }
        }
    }
}