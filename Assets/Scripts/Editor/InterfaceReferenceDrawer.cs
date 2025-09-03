using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IuvoUnity
{
    namespace Editor
    {
        // Fix for CS1069: Ensure the correct attribute is used and the type is properly referenced
        [CustomPropertyDrawer(typeof(InterfaceReference<>), true)]
        [CustomPropertyDrawer(typeof(InterfaceReference<,>), true)]
        public class InterfaceReferenceDrawer : PropertyDrawer
        {
            const string UnderlyingValueFieldName = "underlyingValue";

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                var underlyingProperty = property.FindPropertyRelative(UnderlyingValueFieldName);
                var args = GetArguments(fieldInfo);

                EditorGUI.BeginProperty(position, label, property);
                var assignedObject = EditorGUI.ObjectField(position, label, underlyingProperty.objectReferenceValue, typeof(Object), true);

                if (assignedObject != null)
                {
                    if (assignedObject is GameObject gameObject)
                    {
                        ValidateAndAssignObject(underlyingProperty, gameObject.GetComponent(args.InterfaceType), gameObject.name, args.InterfaceType.Name);
                    }
                    else
                    {
                        ValidateAndAssignObject(underlyingProperty, assignedObject, args.InterfaceType.Name);
                    }
                }
                else
                {
                    underlyingProperty.objectReferenceValue = null;
                }

                EditorGUI.EndProperty();

            }

            static InterfaceArgs GetArguments(FieldInfo fieldInfo)
            {
                Type objectType = null, interfaceType = null;
                Type fieldType = fieldInfo.FieldType;

                bool TryGetTypesFromInterfaceReference(Type type, out Type objType, out Type interfType)
                {
                    objType = interfType = null;

                    if (type?.IsGenericType != true) return false;

                    var genericType = type.GetGenericTypeDefinition();
                    if (genericType == typeof(InterfaceReference<>)) type = type.BaseType;

                    if (type?.GetGenericTypeDefinition() == typeof(InterfaceReference<,>))
                    {
                        var types = type.GetGenericArguments();
                        interfType = types[0];
                        objType = types[1];
                        return true;
                    }
                    return false;

                }

                void GetTypesFromList(Type type, out Type objType, out Type interfType)
                {
                    objType = interfType = null;

                    var listInterface = type.GetInterfaces()
                        .FirstOrDefault(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IList<>));

                    if (listInterface != null)
                    {
                        var elementType = listInterface.GetGenericArguments()[0];
                        TryGetTypesFromInterfaceReference(elementType, out objType, out interfType);
                    }

                }

                if (!TryGetTypesFromInterfaceReference(fieldType, out objectType, out interfaceType))
                {
                    GetTypesFromList(fieldType, out objectType, out interfaceType);
                }

                return new InterfaceArgs(objectType, interfaceType);
            }

            static void ValidateAndAssignObject(SerializedProperty property, Object targetObject, string componentNameOrType, string interfaceName = null)
            {
                if (targetObject != null)
                {
                    property.objectReferenceValue = targetObject;
                }
                else
                {
                    UnityEngine.Debug.LogWarning(
                        $@"The {(interfaceName != null
                            ? $"GameObject '{componentNameOrType}'"
                            : $"assigned object")} does not have a component that implements '{componentNameOrType}.'"
                        );
                    property.objectReferenceValue = null;
                }
            }
        }

        public struct InterfaceArgs
        {
            public readonly Type ObjectType;
            public readonly Type InterfaceType;

            public InterfaceArgs(Type objectType, Type interfaceType)
            {
                UnityEngine.Debug.Assert(typeof(Object).IsAssignableFrom(objectType), $"{nameof(objectType)} needs to be of Type {typeof(Object)}");
                UnityEngine.Debug.Assert(interfaceType.IsInterface, $"{nameof(InterfaceType)} needs to be an interface.");

                ObjectType = objectType;
                InterfaceType = interfaceType;

            }
        }
    }
}