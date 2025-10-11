using UnityEngine;
using IuvoUnity.Debug;

namespace IuvoUnity
{
    namespace Extensions
    {
        /// <summary>
        /// Provides extension methods for working with Unity Materials.
        /// </summary>
        public static class MaterialExtensions
        {
            /// <summary>
            /// Sets the main color of the material, if the "_Color" property exists.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="color">The color to apply.</param>
            public static void WithMainColor(this Material material, Color color)
            {
                if (material.HasProperty("_Color"))
                    material.SetColor("_Color", color);
                else
                    IuvoDebug.DebugLogWarning($"Material '{material.name}' does not have a _Color property.");
            }

            /// <summary>
            /// Sets the main texture of the material, if the "_MainTex" property exists.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="texture">The texture to assign.</param>
            public static void WithMainTexture(this Material material, Texture texture)
            {
                if (material.HasProperty("_MainTex"))
                    material.SetTexture("_MainTex", texture);
                else
                    IuvoDebug.DebugLogWarning($"Material '{material.name}' does not have a _MainTex property.");
            }

            /// <summary>
            /// Sets the offset of the main texture, if the "_MainTex" property exists.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="offset">The texture offset.</param>
            public static void WithMainTextureOffset(this Material material, Vector2 offset)
            {
                if (material.HasProperty("_MainTex"))
                    material.SetTextureOffset("_MainTex", offset);
            }

            /// <summary>
            /// Sets the scale of the main texture, if the "_MainTex" property exists.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="scale">The texture scale.</param>
            public static void WithMainTextureScale(this Material material, Vector2 scale)
            {
                if (material.HasProperty("_MainTex"))
                    material.SetTextureScale("_MainTex", scale);
            }

            /// <summary>
            /// Sets the tiling (scale) of the main texture.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="tiling">The tiling vector.</param>
            public static void WithMainTextureTiling(this Material material, Vector2 tiling)
            {
                material.WithMainTextureScale(tiling);
            }

            /// <summary>
            /// Sets the metallic value of the material, clamped between 0 and 1.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="metallic">The metallic value.</param>
            public static void WithMetallic(this Material material, float metallic)
            {
                if (material.HasProperty("_Metallic"))
                    material.SetFloat("_Metallic", Mathf.Clamp01(metallic));
            }

            /// <summary>
            /// Sets the emission color of the material, if the "_EmissionColor" property exists.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="color">The emission color.</param>
            public static void WithEmissionColor(this Material material, Color color)
            {
                if (material.HasProperty("_EmissionColor"))
                    material.SetColor("_EmissionColor", color);
            }

            /// <summary>
            /// Sets the shader of the material.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="shader">The shader to assign.</param>
            public static void WithShader(this Material material, Shader shader)
            {
                if (shader != null)
                {
                    material.shader = shader;
                }
                else
                {
                    IuvoDebug.DebugLogWarning($"Null shader passed to material '{material.name}'. Shader not changed.");
                }
            }

            /// <summary
            /// Sets the shader of the material by shader name.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="shaderName">The name of the shader.</param>
            public static void WithShader(this Material material, string shaderName)
            {
                Shader shader = Shader.Find(shaderName);
                if (shader != null)
                {
                    material.shader = shader;
                }
                else
                {
                    IuvoDebug.DebugLogWarning($"Shader '{shaderName}' not found. Material '{material.name}' was not updated.");
                }
            }

            /// <summary>
            /// Sets the shader of the material by name, falling back to a secondary shader if the first is not found.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="shaderName">The primary shader name.</param>
            /// <param name="fallbackShaderName">The fallback shader name.</param>
            public static void WithShader(this Material material, string shaderName, string fallbackShaderName)
            {
                Shader shader = Shader.Find(shaderName);
                if (shader == null)
                {
                    IuvoDebug.DebugLogWarning($"Shader '{shaderName}' not found. Falling back to '{fallbackShaderName}'.");
                    shader = Shader.Find(fallbackShaderName);
                }

                if (shader != null)
                {
                    material.shader = shader;
                }
                else
                {
                    IuvoDebug.DebugLogError($"Both shader '{shaderName}' and fallback '{fallbackShaderName}' not found. Material '{material.name}' shader not changed.");
                }
            }

            /// <summary>
            /// Sets the rendering mode of the material using the "_Mode" property.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="mode">The rendering mode (typically 0=Opaque, 1=Cutout, 2=Fade, 3=Transparent).</param>
            public static void WithRenderingMode(this Material material, int mode)
            {
                if (material.HasProperty("_Mode"))
                {
                    material.SetInt("_Mode", mode);
                }
                else
                {
                    IuvoDebug.DebugLogWarning($"Material '{material.name}' does not support '_Mode'. Use a compatible shader for transparency.");
                }
            }

            /// <summary>
            /// Sets a compute buffer on the material.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="propertyName">The shader property name.</param>
            /// <param name="buffer">The compute buffer.</param>
            public static void WithBuffer(this Material material, string propertyName, ComputeBuffer buffer)
            {
                if (material.HasProperty(propertyName))
                    material.SetBuffer(propertyName, buffer);
                else
                    IuvoDebug.DebugLogWarning($"Material '{material.name}' does not have property '{propertyName}' for ComputeBuffer.");
            }

            /// <summary>
            /// Sets a color array on the material.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="propertyName">The shader property name.</param>
            /// <param name="colors">The array of colors.</param>
            public static void WithColorArray(this Material material, string propertyName, Color[] colors)
            {
                if (material.HasProperty(propertyName))
                    material.SetColorArray(propertyName, colors);
            }

            /// <summary>
            /// Sets a float array on the material.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="propertyName">The shader property name.</param>
            /// <param name="values">The array of float values.</param>
            public static void WithFloatArray(this Material material, string propertyName, float[] values)
            {
                if (material.HasProperty(propertyName))
                    material.SetFloatArray(propertyName, values);
            }

            /// <summary>
            /// Sets a matrix array on the material.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="propertyName">The shader property name.</param>
            /// <param name="matrices">The array of matrices.</param>
            public static void WithMatrixArray(this Material material, string propertyName, Matrix4x4[] matrices)
            {
                if (material.HasProperty(propertyName))
                    material.SetMatrixArray(propertyName, matrices);
            }

            /// <summary>
            /// Sets a texture array (Texture2DArray) on the material.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="propertyName">The shader property name.</param>
            /// <param name="textures">The texture array to assign.</param>
            public static void WithTextureArray(this Material material, string propertyName, Texture2DArray textures)
            {
                if (material.HasProperty(propertyName))
                    material.SetTexture(propertyName, textures);
                else
                    IuvoDebug.DebugLogWarning($"Material '{material.name}' does not support texture arrays at '{propertyName}'.");
            }

            /// <summary>
            /// Sets a vector array on the material.
            /// </summary>
            /// <param name="material">The material to modify.</param>
            /// <param name="propertyName">The shader property name.</param>
            /// <param name="vectors">The array of vectors.</param>
            public static void WithVectorArray(this Material material, string propertyName, Vector4[] vectors)
            {
                if (material.HasProperty(propertyName))
                    material.SetVectorArray(propertyName, vectors);
            }
        }
    }
}