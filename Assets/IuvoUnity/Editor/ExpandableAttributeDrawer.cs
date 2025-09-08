using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace IuvoUnity
{
    namespace Editor
    {
        [CustomPropertyDrawer(typeof(ExpandableAttribute))]
        public class ExpandableAttributeDrawer : PropertyDrawer
        {
            // Track foldout states per property
            private static readonly Dictionary<string, bool> _foldouts = new Dictionary<string, bool>();

            // Track recursion to prevent infinite loops
            private static readonly HashSet<int> _drawStack = new HashSet<int>();

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                if (property.propertyType != SerializedPropertyType.ObjectReference)
                {
                    EditorGUI.PropertyField(position, property, label, true);
                    return;
                }

                // Draw object field
                position.height = EditorGUIUtility.singleLineHeight;
                property.objectReferenceValue = EditorGUI.ObjectField(
                    position, label, property.objectReferenceValue, fieldInfo.FieldType, false
                );

                if (property.objectReferenceValue == null)
                    return;

                string key = GetFoldoutKey(property);
                if (!_foldouts.ContainsKey(key)) _foldouts[key] = false;

                // Foldout arrow
                Rect foldoutRect = position;
                foldoutRect.xMin += EditorGUIUtility.labelWidth;
                _foldouts[key] = EditorGUI.Foldout(foldoutRect, _foldouts[key], GUIContent.none, true);

                if (_foldouts[key])
                {
                    int objId = property.objectReferenceValue.GetInstanceID();
                    if (_drawStack.Contains(objId))
                    {
                        // Prevent infinite recursion
                        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                        EditorGUI.LabelField(position, "<Recursive Reference Detected>");
                        return;
                    }

                    _drawStack.Add(objId);

                    // Draw boxed background
                    EditorGUI.indentLevel++;
                    Rect boxRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2,
                        position.width, GetExpandedHeight(property) - EditorGUIUtility.singleLineHeight - 4);
                    GUI.Box(boxRect, GUIContent.none, EditorStyles.helpBox);

                    // Offset content inside box
                    Rect innerPosition = new Rect(position.x + 4, position.y + EditorGUIUtility.singleLineHeight + 6,
                        position.width - 8, EditorGUIUtility.singleLineHeight);

                    DrawExpandedInspector(property.objectReferenceValue, ref innerPosition);
                    EditorGUI.indentLevel--;

                    _drawStack.Remove(objId);
                }
            }

            private void DrawExpandedInspector(Object target, ref Rect position)
            {
                SerializedObject serializedObject = new SerializedObject(target);
                SerializedProperty iterator = serializedObject.GetIterator();

                bool enterChildren = true;
                while (iterator.NextVisible(enterChildren))
                {
                    if (iterator.name == "m_Script") continue;

                    float height = EditorGUI.GetPropertyHeight(iterator, true);
                    Rect propRect = new Rect(position.x, position.y, position.width, height);

                    EditorGUI.PropertyField(propRect, iterator, true);
                    position.y += height + EditorGUIUtility.standardVerticalSpacing;

                    enterChildren = false;
                }

                serializedObject.ApplyModifiedProperties();
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                float height = EditorGUIUtility.singleLineHeight;

                if (property.objectReferenceValue == null)
                    return height;

                string key = GetFoldoutKey(property);
                if (!_foldouts.TryGetValue(key, out bool expanded) || !expanded)
                    return height;

                height = GetExpandedHeight(property);
                return height;
            }

            private float GetExpandedHeight(SerializedProperty property)
            {
                float height = EditorGUIUtility.singleLineHeight + 6; // object field + spacing

                SerializedObject serializedObject = new SerializedObject(property.objectReferenceValue);
                SerializedProperty iterator = serializedObject.GetIterator();

                bool enterChildren = true;
                while (iterator.NextVisible(enterChildren))
                {
                    if (iterator.name == "m_Script") continue;
                    height += EditorGUI.GetPropertyHeight(iterator, true) + EditorGUIUtility.standardVerticalSpacing;
                    enterChildren = false;
                }

                height += 6; // padding inside box
                return height;
            }

            private string GetFoldoutKey(SerializedProperty property)
            {
                return $"{property.serializedObject.targetObject.GetInstanceID()}_{property.propertyPath}";
            }
        }
    }
}