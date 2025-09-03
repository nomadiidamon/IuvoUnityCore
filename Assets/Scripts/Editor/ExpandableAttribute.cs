using UnityEngine;

namespace IuvoUnity
{
    namespace Editor
    {
        /// <summary>
        /// Makes UnityEngine.Object reference fields expandable in the inspector.
        /// Works best with ScriptableObjects or custom assets.
        /// </summary>
        public class ExpandableAttribute : PropertyAttribute
        {
        }
    }
}