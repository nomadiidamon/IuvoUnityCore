using UnityEngine;

namespace IuvoUnity
{
    namespace Extensions
    {
        /// <summary>
        /// Provides extension methods for the <see cref="Renderer"/> component.
        /// </summary>
        public static class RendererExtensions
        {
            /// <summary>
            /// Sets the material of the renderer.
            /// </summary>
            /// <param name="renderer">The renderer to modify.</param>
            /// <param name="material">The material to assign.</param>
            public static void WithMaterial(this Renderer renderer, Material material)
            {
                renderer.material = material;
            }

            /// <summary>
            /// Sets the shared material of the renderer.
            /// </summary>
            /// <param name="renderer">The renderer to modify.</param>
            /// <param name="material">The shared material to assign.</param>
            public static void WithSharedMaterial(this Renderer renderer, Material material)
            {
                renderer.sharedMaterial = material;
            }

            /// <summary>
            /// Sets a material at a specific index in the renderer's material array.
            /// </summary>
            /// <param name="renderer">The renderer to modify.</param>
            /// <param name="index">The index of the material to replace.</param>
            /// <param name="material">The new material to assign at the specified index.</param>
            public static void WithMaterialAtIndex(this Renderer renderer, int index, Material material)
            {
                Material[] materials = renderer.materials;
                if (index >= 0 && index < materials.Length)
                {
                    materials[index] = material;
                    renderer.materials = materials;
                }
            }

            /// <summary>
            /// Enables the renderer, making the object visible.
            /// </summary>
            /// <param name="renderer">The renderer to show.</param>
            public static void Show(this Renderer renderer)
            {
                renderer.enabled = true;
            }

            /// <summary>
            /// Disables the renderer, making the object invisible.
            /// </summary>
            /// <param name="renderer">The renderer to hide.</param>
            public static void Hide(this Renderer renderer)
            {
                renderer.enabled = false;
            }

            /// <summary>
            /// Sets the sorting layer name of the renderer.
            /// </summary>
            /// <param name="renderer">The renderer to modify.</param>
            /// <param name="sortingLayer">The name of the sorting layer.</param>
            public static void WithSortingLayer(this Renderer renderer, string sortingLayer)
            {
                renderer.sortingLayerName = sortingLayer;
            }

            /// <summary>
            /// Sets the render queue of the renderer's material.
            /// </summary>
            /// <param name="renderer">The renderer to modify.</param>
            /// <param name="queue">The render queue value to set.</param>
            public static void WithRenderQueue(this Renderer renderer, int queue)
            {
                renderer.material.renderQueue = queue;
            }

            /// <summary>
            /// Adjusts the alpha (transparency) value of the renderer's material color.
            /// </summary>
            /// <param name="renderer">The renderer to modify.</param>
            /// <param name="alpha">The alpha value to set (0 to 1).</param>
            public static void WithTransparency(this Renderer renderer, float alpha)
            {
                Color color = renderer.material.color;
                color.a = alpha;
                renderer.material.color = color;
            }

            /// <summary>
            /// Resets the renderer's material color alpha value to fully opaque.
            /// </summary>
            /// <param name="renderer">The renderer to modify.</param>
            public static void ResetTransparency(this Renderer renderer)
            {
                Color color = renderer.material.color;
                color.a = 1f;
                renderer.material.color = color;
            }

            /// <summary>
            /// Sets the emission color and intensity for a glowing effect.
            /// </summary>
            /// <param name="renderer">The renderer to modify.</param>
            /// <param name="glowColor">The glow color to apply.</param>
            /// <param name="intensity">The intensity multiplier of the glow.</param>
            public static void WithGlow(this Renderer renderer, Color glowColor, float intensity)
            {
                renderer.material.SetColor("_EmissionColor", glowColor * intensity);
            }

            /// <summary>
            /// Assigns a new shader to the renderer's material.
            /// </summary>
            /// <param name="renderer">The renderer to modify.</param>
            /// <param name="shader">The shader to assign.</param>
            public static void WithShader(this Renderer renderer, Shader shader)
            {
                renderer.material.shader = shader;
            }

            /// <summary>
            /// Tints the renderer's material with a given color.
            /// </summary>
            /// <param name="renderer">The renderer to modify.</param>
            /// <param name="color">The tint color to apply.</param>
            public static void ApplyTint(this Renderer renderer, Color color)
            {
                renderer.material.SetColor("_Color", color);
            }
        }
    }
}