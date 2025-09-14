using System.Collections;
using UnityEngine;

namespace IuvoUnity
{
    namespace Extensions
    {
        public static class CameraExtensions
        {
            private static MonoBehaviour coroutineRunner;

            /// <summary>
            /// Initializes the <see cref="CameraExtensions"/> with a coroutine runner.
            /// </summary>
            /// <param name="runner">The MonoBehaviour instance used to run coroutines.</param>
            public static void Initialize(MonoBehaviour runner)
            {
                coroutineRunner = runner;
            }

            /// <summary>
            /// Shakes the camera screen by applying random offsets to its position.
            /// </summary>
            /// <param name="camera">The camera to shake.</param>
            /// <param name="magnitude">The magnitude of the shake (larger values produce a stronger shake).</param>
            /// <param name="duration">How long the shake will last, in seconds.</param>
            public static void ShakeScreen(this Camera camera, float magnitude, float duration)
            {
                coroutineRunner.StartCoroutine(ShakeScreenCoroutine(camera, magnitude, duration));
            }

            private static IEnumerator ShakeScreenCoroutine(Camera camera, float magnitude, float duration)
            {
                Vector3 originalPosition = camera.transform.position;
                float timeElapsed = 0f;

                while (timeElapsed < duration)
                {
                    camera.transform.position = originalPosition + UnityEngine.Random.insideUnitSphere * magnitude;
                    timeElapsed += Time.deltaTime;
                    yield return null;
                }
                camera.transform.position = originalPosition;
            }


            /// <summary>
            /// Sets the camera's background color.
            /// </summary>
            /// <param name="camera">The camera whose background color is to be set.</param>
            /// <param name="color">The color to set as the background.</param>
            public static void SetBackgroundColor(this Camera camera, Color color)
            {
                camera.backgroundColor = color;
            }

            /// <summary>
            /// Fades the camera's background to transparent over a given duration.
            /// </summary>
            /// <param name="camera">The camera to fade.</param>
            /// <param name="duration">The time, in seconds, for the fade effect.</param>
            public static void FadeOut(this Camera camera, float duration)
            {
                coroutineRunner.StartCoroutine(FadeOutCoroutine(camera, duration));
            }

            private static IEnumerator FadeOutCoroutine(Camera camera, float duration)
            {
                float startAlpha = camera.backgroundColor.a;
                float timer = 0;
                while (timer < duration)
                {
                    Color newColor = camera.backgroundColor;
                    newColor.a = Mathf.Lerp(startAlpha, 0, timer / duration);
                    camera.backgroundColor = newColor;
                    timer += Time.deltaTime;
                    yield return null;
                }
            }

            /// <summary>
            /// Fades the camera's background from transparent to opaque over a given duration.
            /// </summary>
            /// <param name="camera">The camera to fade.</param>
            /// <param name="duration">The time, in seconds, for the fade effect.</param>
            public static void FadeIn(this Camera camera, float duration)
            {
                coroutineRunner.StartCoroutine(FadeInCoroutine(camera, duration));
            }

            private static IEnumerator FadeInCoroutine(Camera camera, float duration)
            {
                float startAlpha = camera.backgroundColor.a;
                float timer = 0;
                while (timer < duration)
                {
                    Color newColor = camera.backgroundColor;
                    newColor.a = Mathf.Lerp(startAlpha, 1, timer / duration);
                    camera.backgroundColor = newColor;
                    timer += Time.deltaTime;
                    yield return null;
                }
            }

            /// <summary>
            /// Moves the camera towards a target position at a specified speed.
            /// </summary>
            /// <param name="camera">The camera to move.</param>
            /// <param name="targetPosition">The target position to move towards.</param>
            /// <param name="speed">The speed of the movement.</param>
            public static void MoveToPosition(this Camera camera, Vector3 targetPosition, float speed)
            {
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetPosition, speed * Time.deltaTime);
            }

            /// <summary>
            /// Rotates the camera towards a target rotation at a specified speed.
            /// </summary>
            /// <param name="camera">The camera to rotate.</param>
            /// <param name="targetRotation">The target rotation to rotate towards.</param>
            /// <param name="speed">The speed of the rotation.</param>
            public static void RotateToRotation(this Camera camera, Quaternion targetRotation, float speed)
            {
                camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, targetRotation, speed * Time.deltaTime);
            }

            /// <summary>
            /// Makes the camera look at a specified target.
            /// </summary>
            /// <param name="camera">The camera to look at the target.</param>
            /// <param name="target">The target to look at.</param>
            public static void LookAt(this Camera camera, Transform target)
            {
                camera.transform.LookAt(target);
            }

            /// <summary>
            /// Makes the camera look at a specified target position.
            /// </summary>
            /// <param name="camera">The camera to look at the target.</param>
            /// <param name="target">The target position to look at.</param>
            public static void LookAt(this Camera camera, Vector3 target)
            {
                camera.transform.LookAt(target);
            }

            /// <summary>
            /// Makes the camera look at a specified target GameObject.
            /// </summary>
            /// <param name="camera">The camera to look at the target.</param>
            /// <param name="target">The target GameObject to look at.</param>
            public static void LookAt(this Camera camera, GameObject target)
            {
                camera.transform.LookAt(target.transform);
            }

            /// <summary>
            /// Makes the camera follow a specified target with an offset.
            /// </summary>
            /// <param name="camera">The camera to follow the target.</param>
            /// <param name="target">The target to follow.</param>
            /// <param name="offset">The offset from the target.</param>
            public static void Follow(this Camera camera, Transform target, Vector3 offset)
            {
                camera.transform.position = target.position + offset;
            }

            // Additional Follow methods for GameObject and Vector3...

            /// <summary>
            /// Zooms in the camera by reducing its field of view.
            /// </summary>
            /// <param name="camera">The camera to zoom in.</param>
            /// <param name="amount">The amount to zoom in, reducing the field of view.</param>
            public static void ZoomIn(this Camera camera, float amount)
            {
                camera.fieldOfView -= amount;
            }

            /// <summary>
            /// Zooms out the camera by increasing its field of view.
            /// </summary>
            /// <param name="camera">The camera to zoom out.</param>
            /// <param name="amount">The amount to zoom out, increasing the field of view.</param>
            public static void ZoomOut(this Camera camera, float amount)
            {
                camera.fieldOfView += amount;
            }

            /// <summary>
            /// Sets the camera's position to a specific value.
            /// </summary>
            /// <param name="camera">The camera to set the position for.</param>
            /// <param name="position">The position to set for the camera.</param>
            public static void SetPosition(this Camera camera, Vector3 position)
            {
                camera.transform.position = position;
            }

            /// <summary>
            /// Sets the camera's rotation to a specific value.
            /// </summary>
            /// <param name="camera">The camera to set the rotation for.</param>
            /// <param name="rotation">The rotation to set for the camera.</param>
            public static void SetRotation(this Camera camera, Quaternion rotation)
            {
                camera.transform.rotation = rotation;
            }

            /// <summary>
            /// Locks the camera's rotation on specific axes.
            /// </summary>
            /// <param name="camera">The camera whose rotation axes are to be locked.</param>
            /// <param name="lockedAxis">The axes to lock (x, y, z).</param>
            public static void LockRotationAxis(this Camera camera, Vector3 lockedAxis)
            {
                camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x * lockedAxis.x,
                                                            camera.transform.eulerAngles.y * lockedAxis.y,
                                                            camera.transform.eulerAngles.z * lockedAxis.z);
            }

            // Additional methods like ResetPosition, RotateAroundPoint, ToggleBackgroundColor, etc.

            /// <summary>
            /// Sets the camera's render target to a specific RenderTexture.
            /// </summary>
            /// <param name="camera">The camera to set the render target for.</param>
            /// <param name="renderTexture">The RenderTexture to use as the render target.</param>
            public static void SetRenderTarget(this Camera camera, RenderTexture renderTexture)
            {
                camera.targetTexture = renderTexture;
            }

            /// <summary>
            /// Gets a ray from the center of the camera's viewport.
            /// </summary>
            /// <param name="camera">The camera from which the ray is cast.</param>
            /// <returns>A ray originating from the center of the camera's viewport.</returns>
            public static Ray GetCenterRay(this Camera camera)
            {
                return camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            }

            /// <summary>
            /// Performs a raycast from the center of the camera's viewport.
            /// </summary>
            /// <param name="camera">The camera to perform the raycast from.</param>
            /// <param name="maxDistance">The maximum distance the ray should check for collisions.</param>
            /// <param name="layerMask">The layers to include in the raycast.</param>
            /// <returns>A RaycastHit containing information about the hit, or null if no hit occurred.</returns>
            public static RaycastHit? RaycastFromCenter(this Camera camera, float maxDistance, LayerMask layerMask)
            {
                Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask))
                    return hit;
                return null;
            }
        }

    }
}