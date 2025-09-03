
using System.Collections.Generic;
using UnityEngine;

namespace IuvoUnity
{
    namespace _ProceduralGeneration
    {
        namespace _Maze
        {
            public class ProceduralMazeGeneration : MonoBehaviour
            {

                public int width = 5;
                public int height = 5;
                public int levels = 3; // 3D maze height

                private List<Vector3> mazeNodes = new List<Vector3>(); // Maze nodes
                private List<ProceduralStripLocationPair> corridors = new List<ProceduralStripLocationPair>(); // Connections between rooms

                public Material pathMaterial;


                void Start()
                {
                    GenerateMaze();
                }

                void GenerateMaze()
                {
                    // Generate nodes (rooms)
                    for (int z = 0; z < levels; z++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                mazeNodes.Add(new Vector3(x * 2, y * 2, z * 2)); // Keep some space for thickness
                            }
                        }
                    }

                    // Connect rooms using random paths
                    for (int i = 0; i < mazeNodes.Count; i++)
                    {
                        Vector3 currentNode = mazeNodes[i];

                        // Connect vertically (up/down)
                        if (i + width < mazeNodes.Count)
                            corridors.Add(new ProceduralStripLocationPair(currentNode, mazeNodes[i + width], Random.ColorHSV()));

                        // Connect horizontally (left/right)
                        if (i + 1 < mazeNodes.Count && i % width != width - 1)
                            corridors.Add(new ProceduralStripLocationPair(currentNode, mazeNodes[i + 1], Random.ColorHSV()));
                    }

                    // Create the mesh from corridors
                    GenerateMesh();
                }

                void GenerateMesh()
                {
                    MeshFilter meshFilter = GetComponent<MeshFilter>();
                    Mesh mesh = new Mesh();
                    List<Vector3> vertices = new List<Vector3>();
                    List<int> triangles = new List<int>();
                    List<Vector2> uvs = new List<Vector2>();

                    // Generate vertices for each corridor
                    foreach (var corridor in corridors)
                    {
                        Vector3 A = corridor.A;
                        Vector3 B = corridor.B;
                        // Create vertices for corridor mesh (simple quad for now)
                        vertices.Add(A);
                        vertices.Add(B);
                        vertices.Add(A + Vector3.up * 1); // Vertical offset for thickness
                        vertices.Add(B + Vector3.up * 1);

                        // Triangles (forming a quad)
                        int vertCount = vertices.Count;
                        triangles.Add(vertCount - 4);
                        triangles.Add(vertCount - 3);
                        triangles.Add(vertCount - 2);
                        triangles.Add(vertCount - 2);
                        triangles.Add(vertCount - 3);
                        triangles.Add(vertCount - 1);

                        // UVs (simple mapping for now)
                        uvs.Add(new Vector2(0, 0));
                        uvs.Add(new Vector2(1, 0));
                        uvs.Add(new Vector2(0, 1));
                        uvs.Add(new Vector2(1, 1));
                    }

                    // Set mesh data
                    mesh.SetVertices(vertices);
                    mesh.SetTriangles(triangles, 0);
                    mesh.SetUVs(0, uvs);

                    mesh.RecalculateNormals();
                    mesh.RecalculateBounds();

                    meshFilter.mesh = mesh;
                }

                void ApplyTextures()
                {
                    MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
                    if (pathMaterial != null)
                    {
                        meshRenderer.material = pathMaterial;
                    }
                }
            }
        }
    }
}