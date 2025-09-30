using IuvoUnity.Debug;
using UnityEditor;
using UnityEngine;

namespace IuvoUnity
{
    namespace Editor
    {
        [CustomPropertyDrawer(typeof(DebugButton))]
        public class DebugButtonDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                SerializedProperty labelProp = property.FindPropertyRelative("Label");

                // Draw the button
                if (GUI.Button(position, string.IsNullOrEmpty(labelProp.stringValue) ? "Debug Action" : labelProp.stringValue))
                {
                    // Get the target object that owns this property
                    object target = property.serializedObject.targetObject;

                    // Get the actual field value (DebugButton instance)
                    var fieldInfo = this.fieldInfo;
                    var debugButton = fieldInfo.GetValue(target) as DebugButton;

                    // Call assigned action
                    debugButton.OnClick.Invoke();
                }
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return EditorGUIUtility.singleLineHeight + 2;
            }
        }
    }
}