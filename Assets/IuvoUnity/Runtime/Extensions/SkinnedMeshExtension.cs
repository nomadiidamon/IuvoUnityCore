using System;
using UnityEngine;

namespace IuvoUnity
{
    namespace Extensions
    {
        /// <summary>
        /// Extension methods for SkinnedMeshRenderer to simplify material and bounds manipulation.
        /// </summary>
        public static class SkinnedMeshExtensions
        {
            /// <summary>
            /// Sets a single material on the SkinnedMeshRenderer.
            /// </summary>
            /// <param name="renderer">The SkinnedMeshRenderer to modify.</param>
            /// <param name="material">The material to assign.</param>
            public static void SetMaterial(this SkinnedMeshRenderer renderer, Material material)
            {
                renderer.material = material;
            }

            /// <summary>
            /// Gets the material at a specific index from the SkinnedMeshRenderer.
            /// </summary>
            /// <param name="renderer">The SkinnedMeshRenderer to read from.</param>
            /// <param name="index">The index of the material.</param>
            /// <returns>The material at the given index, or null if index is out of range.</returns>
            public static Material GetMaterialAtIndex(this SkinnedMeshRenderer renderer, int index)
            {
                Material[] materials = renderer.materials;
                return (index >= 0 && index < materials.Length) ? materials[index] : null;
            }

            /// <summary>
            /// Sets a material at a specific index in the SkinnedMeshRenderer.
            /// </summary>
            /// <param name="renderer">The SkinnedMeshRenderer to modify.</param>
            /// <param name="index">The index to set the material at.</param>
            /// <param name="material">The material to set.</param>
            public static void SetMaterialAtIndex(this SkinnedMeshRenderer renderer, int index, Material material)
            {
                Material[] materials = renderer.materials;
                if (index >= 0 && index < materials.Length)
                {
                    materials[index] = material;
                    renderer.materials = materials;
                }
            }

            /// <summary>
            /// Sets an array of materials on the SkinnedMeshRenderer.
            /// </summary>
            /// <param name="renderer">The SkinnedMeshRenderer to modify.</param>
            /// <param name="materials">The array of materials to set.</param>
            public static void SetMaterials(this SkinnedMeshRenderer renderer, Material[] materials)
            {
                renderer.materials = materials;
            }

            /// <summary>
            /// Clears all materials from the SkinnedMeshRenderer.
            /// </summary>
            /// <param name="renderer">The SkinnedMeshRenderer to clear.</param>
            public static void ClearMaterials(this SkinnedMeshRenderer renderer)
            {
                renderer.materials = new Material[0];
            }

            /// <summary>
            /// Checks whether the SkinnedMeshRenderer contains a specific material.
            /// </summary>
            /// <param name="renderer">The SkinnedMeshRenderer to check.</param>
            /// <param name="material">The material to look for.</param>
            /// <returns>True if the material is found; otherwise, false.</returns>
            public static bool HasMaterial(this SkinnedMeshRenderer renderer, Material material)
            {
                return Array.Exists(renderer.materials, m => m == material);
            }

            /// <summary>
            /// Gets the bounds of the SkinnedMeshRenderer.
            /// </summary>
            /// <param name="renderer">The SkinnedMeshRenderer to query.</param>
            /// <returns>The bounds of the renderer.</returns>
            public static Bounds GetBounds(this SkinnedMeshRenderer renderer)
            {
                return renderer.bounds;
            }

            /// <summary>
            /// Sets the bounds of the SkinnedMeshRenderer.
            /// </summary>
            /// <param name="renderer">The SkinnedMeshRenderer to modify.</param>
            /// <param name="bounds">The new bounds to assign.</param>
            public static void SetBounds(this SkinnedMeshRenderer renderer, Bounds bounds)
            {
                renderer.bounds = bounds;
            }

            /// <summary>
            /// Gets the number of submeshes in the shared mesh of the SkinnedMeshRenderer.
            /// </summary>
            /// <param name="renderer">The SkinnedMeshRenderer to query.</param>
            /// <returns>The number of submeshes.</returns>
            public static int GetSubMeshCount(this SkinnedMeshRenderer renderer)
            {
                return renderer.sharedMesh.subMeshCount;
            }
        }
    }
}
