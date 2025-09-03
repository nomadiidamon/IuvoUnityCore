using UnityEngine;


namespace IuvoUnity
{
    namespace _ProceduralGeneration
    {
        namespace _Mesh
        {
            public class TerrainGenerator : ProceduralGridMesh
            {

                [Header("Terrain Settings")]
                public float noiseScale = 0.3f;
                public float heightMultiplier = 3.0f;

                protected override void GenerateMesh()
                {
                    base.GenerateMesh();

                    for (int i = 0; i < vertices.Count; i++)
                    {
                        float xCoord = vertices[i].x * noiseScale;
                        float zCoord = vertices[i].z * noiseScale;
                        float y = Mathf.PerlinNoise(xCoord, zCoord) * heightMultiplier;
                        vertices[i] = new Vector3(vertices[i].x, y, vertices[i].z);
                    }
                    mesh.RecalculateNormals();
                }

            }
        }
    }
}