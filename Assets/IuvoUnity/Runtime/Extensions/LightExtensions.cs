using UnityEngine;
using UnityEngine.Rendering;

namespace IuvoUnity
{
    namespace Extensions
    {
        /// <summary>
        /// Extension methods for UnityEngine.Light to simplify common property adjustments.
        /// </summary>
        public static class LightExtensions
        {
            #region Basic Properties

            /// <summary>
            /// Sets the intensity of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="intensity">The desired intensity value.</param>
            public static void SetIntensity(this Light light, float intensity)
            {
                if (light == null) return;
                light.intensity = intensity;
            }

            /// <summary>
            /// Sets the range of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="range">The desired range value.</param>
            public static void SetRange(this Light light, float range)
            {
                if (light == null) return;
                light.range = range;
            }

            /// <summary>
            /// Sets the color of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="color">The desired color.</param>
            public static void SetColor(this Light light, Color color)
            {
                if (light == null) return;
                light.color = color;
            }

            /// <summary>
            /// Sets the bounce intensity of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="intensity">The desired bounce intensity.</param>
            public static void SetBounceIntensity(this Light light, float intensity)
            {
                if (light == null) return;
                light.bounceIntensity = intensity;
            }

            /// <summary>
            /// Toggles the enabled state of the light.
            /// </summary>
            /// <param name="light">The light to toggle.</param>
            public static void Toggle(this Light light)
            {
                if (light == null) return;
                light.enabled = !light.enabled;
            }

            #endregion

            #region Shadows

            /// <summary>
            /// Sets the shadow strength of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="strength">The desired shadow strength.</param>
            public static void SetShadowStrength(this Light light, float strength)
            {
                if (light == null) return;
                light.shadowStrength = strength;
            }

            /// <summary>
            /// Sets the shadow resolution of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="resolution">The desired shadow resolution.</param>
            public static void SetShadowResolution(this Light light, LightShadowResolution resolution)
            {
                if (light == null) return;
                light.shadowResolution = resolution;
            }

            /// <summary>
            /// Sets the shadow bias of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="bias">The desired shadow bias.</param>
            public static void SetShadowBias(this Light light, float bias)
            {
                if (light == null) return;
                light.shadowBias = bias;
            }

            /// <summary>
            /// Sets the shadow normal bias of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="normalBias">The desired shadow normal bias.</param>
            public static void SetShadowNormalBias(this Light light, float normalBias)
            {
                if (light == null) return;
                light.shadowNormalBias = normalBias;
            }

            #endregion

            #region Advanced Settings

            /// <summary>
            /// Sets the color temperature of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="temperature">The desired color temperature.</param>
            public static void SetColorTemperature(this Light light, float temperature)
            {
                if (light == null) return;
                light.colorTemperature = temperature;
            }

            /// <summary>
            /// Sets the culling mask of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="cullingMask">The desired culling mask.</param>
            public static void SetCullingMask(this Light light, LayerMask cullingMask)
            {
                if (light == null) return;
                light.cullingMask = cullingMask;
            }

            /// <summary>
            /// Sets the flare of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="flare">The desired flare object.</param>
            public static void SetFlare(this Light light, Flare flare)
            {
                if (light == null) return;
                light.flare = flare;
            }

            /// <summary>
            /// Sets the spot angle of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="angle">The desired spot angle.</param>
            public static void SetSpotAngle(this Light light, float angle)
            {
                if (light == null) return;
                light.spotAngle = angle;
            }

            /// <summary>
            /// Sets the cookie texture of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="cookie">The desired cookie texture.</param>
            public static void SetCookie(this Light light, Texture cookie)
            {
                if (light == null) return;
                light.cookie = cookie;
            }

            /// <summary>
            /// Sets the render mode of the light.
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="renderMode">The desired render mode.</param>
            public static void SetRenderMode(this Light light, LightRenderMode renderMode)
            {
                if (light == null) return;
                light.renderMode = renderMode;
            }

            /// <summary>
            /// Sets the area size of the light (for area lights).
            /// </summary>
            /// <param name="light">The light to modify.</param>
            /// <param name="size">The desired area size as a Vector2.</param>
            public static void SetAreaSize(this Light light, Vector2 size)
            {
                if (light == null) return;
                light.areaSize = size;
            }

            #endregion
        }
    }
}