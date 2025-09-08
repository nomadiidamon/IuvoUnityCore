using UnityEngine;
using IuvoUnity.Debug;

namespace IuvoUnity
{
    namespace Extensions
    {
        /// <summary>
        /// Provides extension methods for the <see cref="MeshRenderer"/> component.
        /// </summary>
        public static class MeshRendererExtensions
        {
            /// <summary>
            /// Gets the number of submeshes in the mesh attached to the given <see cref="MeshRenderer"/>.
            /// </summary>
            /// <param name="renderer">The target <see cref="MeshRenderer"/>.</param>
            /// <returns>The number of submeshes if a valid mesh is found; otherwise, returns 0.</returns>
            public static int GetSubMeshCount(this MeshRenderer renderer)
            {
                MeshFilter meshFilter = renderer.GetComponent<MeshFilter>();
                if (meshFilter != null && meshFilter.sharedMesh != null)
                {
                    return meshFilter.sharedMesh.subMeshCount;
                }
                return 0;
            }

            /// <summary>
            /// Modifies the given <see cref="Mesh"/> to render as double-sided by inverting normals and optionally flipping triangle winding.
            /// </summary>
            /// <param name="mesh">The mesh to modify.</param>
            /// <remarks>
            /// This method inverts the normals and swaps the winding order of triangles to allow both sides of the mesh to be visible.
            /// Use this with shaders that respect backface culling settings.
            /// </remarks>
            public static void EnsureDoubleSided(Mesh mesh)
            {
                // Flip normals for double-sided rendering
                Vector3[] normals = mesh.normals;
                for (int i = 0; i < normals.Length; i++)
                {
                    normals[i] = -normals[i]; // Invert the normals
                }
                mesh.normals = normals;

                // Optionally, flip the triangles to make them visible from both sides
                int[] triangles = mesh.triangles;
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    int temp = triangles[i];
                    triangles[i] = triangles[i + 1];
                    triangles[i + 1] = temp;
                }
                mesh.triangles = triangles;
            }

            /// <summary>
            /// Sets the culling mode on the <see cref="MeshRenderer"/>'s material.
            /// </summary>
            /// <param name="renderer">The target <see cref="MeshRenderer"/>.</param>
            /// <param name="cullMode">The desired <see cref="UnityEngine.Rendering.CullMode"/>.</param>
            /// <remarks>
            /// The material must support the _Cull property for this to have an effect.
            /// </remarks>
            public static void SetCulling(this MeshRenderer renderer, UnityEngine.Rendering.CullMode cullMode)
            {
                if (renderer != null && renderer.material != null)
                {
                    // Set the Cull mode to the specified value
                    renderer.material.SetInt("_Cull", (int)cullMode);
                }
                else
                {
                    IuvoDebug.DebugLogError("MeshRenderer or material is missing!");
                }
            }
        }
    }
}
