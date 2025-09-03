
using System.Collections.Generic;
using UnityEngine;
using IuvoUnity.Debug;

namespace IuvoUnity
{
    [System.Serializable]
    public struct ProceduralStripLocationPair
    {
        public Vector3 A;
        public Vector3 B;
        public Color color;

        public ProceduralStripLocationPair(Vector3 a, Vector3 b, Color color)
        {
            A = a;
            B = b;
            this.color = color;
        }
    }

    namespace _ProceduralGeneration
    {

        namespace _Mesh
        {
            [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
            public class ProceduralStripMesh : ProceduralMesh
            {
                [Header("Strip Settings")]
                public List<ProceduralStripLocationPair> pairedPoints = new List<ProceduralStripLocationPair>();
                public bool closedLoop = false;
                public float thickness = 0f; // zero = flat strip

                [Header("UV Settings")]
                public bool stretchUV = false;
                public float uvTileX = 1f;
                public float uvTileY = 1f;

                [Header("Color Settings")]
                public bool useColors = false;
                protected List<Color> colors = new List<Color>();

                protected override void GenerateMesh()
                {
                    if (pairedPoints.Count < 2)
                    {
                        IuvoDebug.DebugLogWarning("Need at least two pairs to generate a strip.");
                        return;
                    }

                    int loopLimit = closedLoop ? pairedPoints.Count : pairedPoints.Count - 1;

                    for (int i = 0; i < loopLimit; i++)
                    {
                        ProceduralStripLocationPair p0 = pairedPoints[i];
                        ProceduralStripLocationPair p1 = pairedPoints[(i + 1) % pairedPoints.Count];

                        int startIndex = vertices.Count;

                        Vector3 dirA = (p1.A - p0.A).normalized;
                        Vector3 dirB = (p1.B - p0.B).normalized;
                        Vector3 normal = Vector3.Cross(dirB - dirA, p0.B - p0.A).normalized;

                        Vector3 tOffset = thickness * normal * 0.5f;

                        // Vertices for quad (with thickness)
                        vertices.Add(p0.A - tOffset);
                        vertices.Add(p1.A - tOffset);
                        vertices.Add(p1.B + tOffset);
                        vertices.Add(p0.B + tOffset);

                        // Triangles
                        triangles.Add(startIndex + 0);
                        triangles.Add(startIndex + 1);
                        triangles.Add(startIndex + 2);

                        triangles.Add(startIndex + 2);
                        triangles.Add(startIndex + 3);
                        triangles.Add(startIndex + 0);

                        // UVs
                        float t0 = stretchUV ? (float)i / loopLimit : i;
                        float t1 = stretchUV ? (float)(i + 1) / loopLimit : i + 1;

                        uvs.Add(new Vector2(0, t0 * uvTileY));
                        uvs.Add(new Vector2(0, t1 * uvTileY));
                        uvs.Add(new Vector2(1 * uvTileX, t1 * uvTileY));
                        uvs.Add(new Vector2(1 * uvTileX, t0 * uvTileY));

                        // Normals
                        for (int j = 0; j < 4; j++)
                            normals.Add(normal);

                        // Colors (optional)
                        if (useColors)
                        {
                            colors.Add(p0.color);
                            colors.Add(p1.color);
                            colors.Add(p1.color);
                            colors.Add(p0.color);
                        }
                    }
                }

                protected override void ApplyMesh()
                {
                    base.ApplyMesh();

                    if (useColors && colors.Count == vertices.Count)
                        mesh.SetColors(colors);
                }

                protected override void ClearMeshData()
                {
                    base.ClearMeshData();
                    colors.Clear();
                }

                public void SetPairs(List<ProceduralStripLocationPair> pairs)
                {
                    pairedPoints = new List<ProceduralStripLocationPair>(pairs);
                    Generate();
                }

                public void GenerateBetweenStrips(List<ProceduralStripLocationPair> stripA, List<ProceduralStripLocationPair> stripB)
                {
                    if (stripA.Count != stripB.Count)
                    {
                        IuvoDebug.DebugLogError("Strips must have the same number of pairs.");
                        return;
                    }

                    ClearMeshData();

                    for (int i = 0; i < stripA.Count - 1; i++)
                    {
                        ProceduralStripLocationPair a0 = stripA[i];
                        ProceduralStripLocationPair a1 = stripA[i + 1];

                        ProceduralStripLocationPair b0 = stripB[i];
                        ProceduralStripLocationPair b1 = stripB[i + 1];

                        int startIndex = vertices.Count;

                        // Vertices (quad: a0.A -> a1.A -> b1.A -> b0.A)
                        vertices.Add(a0.A);
                        vertices.Add(a1.A);
                        vertices.Add(b1.A);
                        vertices.Add(b0.A);

                        vertices.Add(a0.B);
                        vertices.Add(a1.B);
                        vertices.Add(b1.B);
                        vertices.Add(b0.B);

                        // Triangles for both quads
                        triangles.AddRange(new int[]
                        {
                            startIndex + 0, startIndex + 1, startIndex + 2,
                            startIndex + 2, startIndex + 3, startIndex + 0,

                            startIndex + 4, startIndex + 5, startIndex + 6,
                            startIndex + 6, startIndex + 7, startIndex + 4
                        });

                        // Normals (approximate cross product)
                        Vector3 forwardA = a1.A - a0.A;
                        Vector3 upA = b0.A - a0.A;
                        Vector3 normalA = Vector3.Cross(forwardA, upA).normalized;

                        Vector3 forwardB = a1.B - a0.B;
                        Vector3 upB = b0.B - a0.B;
                        Vector3 normalB = Vector3.Cross(forwardB, upB).normalized;

                        normals.AddRange(new[] { normalA, normalA, normalA, normalA });
                        normals.AddRange(new[] { normalB, normalB, normalB, normalB });

                        // UVs (simple mapping)
                        float v0 = (float)i / (stripA.Count - 1);
                        float v1 = (float)(i + 1) / (stripA.Count - 1);

                        uvs.AddRange(new[]
                        {
                            new Vector2(0, v0),
                            new Vector2(0, v1),
                            new Vector2(1, v1),
                            new Vector2(1, v0),

                            new Vector2(0, v0),
                            new Vector2(0, v1),
                            new Vector2(1, v1),
                            new Vector2(1, v0)
                        });

                        if (useColors)
                        {
                            colors.AddRange(new[] { a0.color, a1.color, b1.color, b0.color });
                            colors.AddRange(new[] { a0.color, a1.color, b1.color, b0.color });
                        }
                    }

                    ApplyMesh();
                }
            }
        }
    }
}
