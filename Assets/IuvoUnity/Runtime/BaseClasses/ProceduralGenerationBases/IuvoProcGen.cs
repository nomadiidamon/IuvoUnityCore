using UnityEngine;

namespace IuvoUnity
{
    namespace ProceduralGeneration
    {

        public enum CenteringMode
        {
            TransformPosition, // Default: use transform.position
            BoundsMin,         // Use the minimum bound (bottom/left/back)
            BoundsMax,         // Use the maximum bound (top/right/front)
            CustomPosition     // Use a custom Vector3
        }

        public static class IuvoProceduralGeneration
        {

        }

    }
}