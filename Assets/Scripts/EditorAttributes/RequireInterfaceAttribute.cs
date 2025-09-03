using System;
using UnityEngine;

namespace IuvoUnity
{
    namespace Editor
    {
        [AttributeUsage(AttributeTargets.Field)]
        public class RequireInterfaceAttribute : PropertyAttribute
        {
            public readonly Type interfaceType;
            public RequireInterfaceAttribute(Type interfaceType)
            {
                UnityEngine.Debug.Assert(interfaceType.IsInterface, $"{nameof(interfaceType)} needs to be an interface");
                this.interfaceType = interfaceType;

            }
        }
    }
}