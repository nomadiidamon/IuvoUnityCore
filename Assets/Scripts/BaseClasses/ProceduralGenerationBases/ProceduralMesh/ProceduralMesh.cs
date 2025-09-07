using System.Collections.Generic;
using UnityEngine;



namespace IuvoUnity
{
    namespace ProceduralGeneration
    {

        [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
        public abstract class ProceduralMesh : MonoBehaviour
        {
            public List<Vector3> vertices = new List<Vector3>();
            public List<int> triangles = new List<int>();
            public List<Vector2> uvs = new List<Vector2>();
            public List<Vector3> normals = new List<Vector3>();
            public Mesh mesh;
            public string meshName = "ProceduralMesh";
            public Material meshMaterial;

            protected virtual void Awake() => Generate();

            public void Generate()
            {
                ClearMeshData();
                GenerateMesh();
                ApplyMesh();
            }

            protected abstract void GenerateMesh();

            protected virtual void ClearMeshData()
            {
                vertices.Clear();
                triangles.Clear();
                uvs.Clear();
                normals.Clear();
            }

            protected virtual void ApplyMesh()
            {
                if (mesh == null)
                {
                    mesh = new Mesh { name = meshName };
                }

                mesh.Clear();
                mesh.SetVertices(vertices);
                mesh.SetTriangles(triangles, 0);

                if (uvs.Count == vertices.Count)
                    mesh.SetUVs(0, uvs);
                else
                    mesh.uv = new Vector2[vertices.Count];


                if (normals.Count == vertices.Count)
                    mesh.SetNormals(normals);
                else
                    mesh.RecalculateNormals();

                mesh.RecalculateBounds();
                GetComponent<MeshFilter>().mesh = mesh;
                if (meshMaterial != null)
                    GetComponent<MeshRenderer>().material = meshMaterial;

            }
        }
    }
}