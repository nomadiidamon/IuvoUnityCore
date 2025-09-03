using UnityEngine;


namespace IuvoUnity
{
    namespace _Extensions
    {
        /// <summary>
        /// Provides extension methods for the <see cref="UnityEngine.Vector3"/> struct to simplify common vector operations.
        /// </summary>
        public static class Vector3Extensions
        {
            /// <summary>
            /// Returns a new Vector3 with selectively overridden x, y, and/or z values.
            /// </summary>
            /// <param name="vector">The original vector.</param>
            /// <param name="x">Optional new x value.</param>
            /// <param name="y">Optional new y value.</param>
            /// <param name="z">Optional new z value.</param>
            /// <returns>A new Vector3 with specified values replaced.</returns>
            public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
            {
                return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
            }

            /// <summary>
            /// Adds specified values to the vector's components.
            /// </summary>
            /// <param name="vector">The original vector.</param>
            /// <param name="x">Value to add to x component.</param>
            /// <param name="y">Value to add to y component.</param>
            /// <param name="z">Value to add to z component.</param>
            /// <returns>The resulting vector after addition.</returns>
            public static Vector3 Add(this Vector3 vector, float? x = null, float? y = null, float? z = null)
            {
                return new Vector3(vector.x + (x ?? 0), vector.y + (y ?? 0), vector.z + (z ?? 0));
            }

            /// <summary>
            /// Subtracts specified values from the vector's components.
            /// </summary>
            /// <param name="vector">The original vector.</param>
            /// <param name="x">Value to subtract from x component.</param>
            /// <param name="y">Value to subtract from y component.</param>
            /// <param name="z">Value to subtract from z component.</param>
            /// <returns>The resulting vector after subtraction.</returns>
            public static Vector3 Subtract(this Vector3 vector, float? x = null, float? y = null, float? z = null)
            {
                return new Vector3(vector.x - (x ?? 0), vector.y - (y ?? 0), vector.z - (z ?? 0));
            }

            /// <summary>
            /// Multiplies the vector's components by specified values.
            /// </summary>
            /// <param name="vector">The original vector.</param>
            /// <param name="x">Multiplier for x component.</param>
            /// <param name="y">Multiplier for y component.</param>
            /// <param name="z">Multiplier for z component.</param>
            /// <returns>The resulting vector after multiplication.</returns>
            public static Vector3 Multiply(this Vector3 vector, float? x = null, float? y = null, float? z = null)
            {
                return new Vector3(vector.x * (x ?? 1), vector.y * (y ?? 1), vector.z * (z ?? 1));
            }

            /// <summary>
            /// Divides the vector's components by specified values.
            /// </summary>
            /// <param name="vector">The original vector.</param>
            /// <param name="x">Divisor for x component.</param>
            /// <param name="y">Divisor for y component.</param>
            /// <param name="z">Divisor for z component.</param>
            /// <returns>The resulting vector after division.</returns>
            public static Vector3 Divide(this Vector3 vector, float? x = null, float? y = null, float? z = null)
            {
                return new Vector3(
                    vector.x / (x ?? 1),
                    vector.y / (y ?? 1),
                    vector.z / (z ?? 1)
                );
            }

            /// <summary>
            /// Clamps each component of the vector between the corresponding components of two other vectors.
            /// </summary>
            /// <param name="vector">The original vector.</param>
            /// <param name="min">The minimum vector limits.</param>
            /// <param name="max">The maximum vector limits.</param>
            /// <returns>The clamped vector.</returns>
            public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max)
            {
                return new Vector3(
                    Mathf.Clamp(vector.x, min.x, max.x),
                    Mathf.Clamp(vector.y, min.y, max.y),
                    Mathf.Clamp(vector.z, min.z, max.z)
                );
            }

            /// <summary>
            /// Returns a new vector with a modified x component.
            /// </summary>
            public static Vector3 WithX(this Vector3 vector, float x) => new Vector3(x, vector.y, vector.z);

            /// <summary>
            /// Returns a new vector with a modified y component.
            /// </summary>
            public static Vector3 WithY(this Vector3 vector, float y) => new Vector3(vector.x, y, vector.z);

            /// <summary>
            /// Returns a new vector with a modified z component.
            /// </summary>
            public static Vector3 WithZ(this Vector3 vector, float z) => new Vector3(vector.x, vector.y, z);

            /// <summary>
            /// Rotates the vector by a given quaternion.
            /// </summary>
            /// <param name="vector">The original vector.</param>
            /// <param name="rotation">The rotation to apply.</param>
            /// <returns>The rotated vector.</returns>
            public static Vector3 Rotate(this Vector3 vector, Quaternion rotation) => rotation * vector;

            /// <summary>
            /// Returns the average of this vector and another vector.
            /// </summary>
            /// <param name="vector">The first vector.</param>
            /// <param name="other">The second vector.</param>
            /// <returns>The average vector.</returns>
            public static Vector3 Average(this Vector3 vector, Vector3 other)
            {
                return (vector + other) / 2f;
            }

            /// <summary>
            /// Calculates the angle in radians between this vector and another vector.
            /// </summary>
            /// <param name="vector">The first vector.</param>
            /// <param name="target">The second vector.</param>
            /// <returns>The angle in radians.</returns>
            public static float AngleBetween(this Vector3 vector, Vector3 target)
            {
                return Vector3.Angle(vector, target) * Mathf.Deg2Rad;
            }

            /// <summary>
            /// Determines whether two vectors are approximately equal within a specified distance tolerance.
            /// </summary>
            /// <param name="vector">The first vector.</param>
            /// <param name="other">The second vector.</param>
            /// <param name="tolerance">The maximum distance to consider the vectors equal.</param>
            /// <returns>True if the vectors are within the tolerance; otherwise, false.</returns>
            public static bool ApproximatelyEqualDistance(this Vector3 vector, Vector3 other, float tolerance = 0.0001f)
            {
                return Vector3.Distance(vector, other) < tolerance;
            }

            /// <summary>
            /// Checks if there are any objects with the specified layer within a sphere around the position.
            /// </summary>
            /// <param name="position">The center of the sphere.</param>
            /// <param name="layer">The layer to detect.</param>
            /// <param name="radius">The radius of the sphere.</param>
            /// <returns>True if an object is found; otherwise, false.</returns>
            public static bool HasObjectWithLayer(this Vector3 position, int layer, float radius = 0.1f)
            {
                int layerMask = 1 << layer;
                Collider[] colliders = Physics.OverlapSphere(position, radius, layerMask);
                return colliders.Length > 0;
            }

            /// <summary>
            /// Checks if there are any objects with the specified layer within a box around the position.
            /// </summary>
            /// <param name="position">The center of the box.</param>
            /// <param name="layer">The layer to detect.</param>
            /// <param name="halfExtents">Half of the box size in each dimension.</param>
            /// <param name="rotation">The rotation of the box.</param>
            /// <returns>True if an object is found; otherwise, false.</returns>
            public static bool HasObjectWithLayerInBox(this Vector3 position, int layer, Vector3 halfExtents, Quaternion rotation = default)
            {
                int layerMask = 1 << layer;
                Collider[] colliders = Physics.OverlapBox(position, halfExtents, rotation, layerMask);
                return colliders.Length > 0;
            }

            /// <summary>
            /// Checks for collisions along all three axes using spherical checks offset in each direction.
            /// </summary>
            /// <param name="position">The position to check from.</param>
            /// <param name="radius">The radius of the check spheres.</param>
            /// <param name="layerMask">The layer mask to check against.</param>
            /// <returns>True if any collision is found along any axis; otherwise, false.</returns>
            public static bool CheckAllAxesForCollision(this Vector3 position, float radius, LayerMask layerMask)
            {
                Vector3 xOffset = new Vector3(radius, 0, 0);
                if (Physics.CheckSphere(position + xOffset, radius, layerMask) ||
                    Physics.CheckSphere(position - xOffset, radius, layerMask))
                {
                    return true;
                }

                Vector3 yOffset = new Vector3(0, radius, 0);
                if (Physics.CheckSphere(position + yOffset, radius, layerMask) ||
                    Physics.CheckSphere(position - yOffset, radius, layerMask))
                {
                    return true;
                }

                Vector3 zOffset = new Vector3(0, 0, radius);
                if (Physics.CheckSphere(position + zOffset, radius, layerMask) ||
                    Physics.CheckSphere(position - zOffset, radius, layerMask))
                {
                    return true;
                }

                return false;
            }
        }

    }
}