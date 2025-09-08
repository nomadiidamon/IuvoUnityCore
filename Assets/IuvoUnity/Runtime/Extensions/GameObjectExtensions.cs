using System.Linq;
using UnityEngine;
using IuvoUnity.Debug;

namespace IuvoUnity
{
    namespace Extensions
    {
        public static class GameObjectExtensions
        {
            /// <summary>
            /// Recursively sets the layer of a GameObject and all of its children
            /// </summary>
            /// <param name="obj">The GameObject to change the layer of.</param>
            /// <param name="layer">The Layer to set.</param>
            public static void SetLayerRecursively(this GameObject obj, int layer)
            {
                obj.layer = layer;
                foreach (Transform child in obj.transform)
                {
                    child.gameObject.SetLayerRecursively(layer);
                }
            }


            /// <summary>
            /// Gets the component of type T if its exists otherwise adds it to the GameObject.
            /// </summary>
            /// <typeparam name="T">The type of the component to get or add.</typeparam>
            /// <param name="gameObject">The GameObject to get or add the component to.</param>
            /// <returns>The component of type T</returns>
            public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
            {
                T component = gameObject.GetComponent<T>();
                if (!component) component = gameObject.AddComponent<T>();

                return component;
            }



            /// <summary>
            /// Returns the object if it is not null, otherwise returns null.
            /// </summary>
            /// <typeparam name="T">The type of object.</typeparam>
            /// <param name="obj">The object to check.</param>
            /// <returns>The object or null if it's null.</returns>
            public static T OrNull<T>(this T obj) where T : UnityEngine.Object => (bool)obj ? obj : null;



            /// <summary>
            /// Finds a child GameObject by its name.
            /// </summary>
            /// <param name="gameObject">The parent GameObject.</param>
            /// <param name="childName">The name of the child GameObject to find.</param>
            /// <returns>The child GameObject, or null if not found.</returns>
            public static GameObject FindChildByName(this GameObject gameObject, string childName)
            {
                Transform childTransform = gameObject.transform.Find(childName);
                return childTransform != null ? childTransform.gameObject : null;
            }



            /// <summary>
            /// Finds a child GameObject by its tag.
            /// </summary>
            /// <param name="gameObject">The parent GameObject.</param>
            /// <param name="tag">The tag to search for.</param>
            /// <returns>The first child GameObject with the given tag, or null if not found.</returns>
            public static GameObject FindChildByTag(this GameObject gameObject, string tag)
            {
                foreach (Transform child in gameObject.transform)
                {
                    if (child.CompareTag(tag))
                    {
                        return child.gameObject;
                    }
                }
                return null;
            }


            /// <summary>
            /// Checks if a GameObject has a component of type T.
            /// </summary>
            /// <typeparam name="T">The type of the component to check for.</typeparam>
            /// <param name="gameObject">The GameObject to check.</param>
            /// <returns>True if the component is present, otherwise false.</returns>
            public static bool HasComponent<T>(this GameObject gameObject) where T : Component
            {
                return gameObject.GetComponent<T>() != null;
            }


            /// <summary>
            /// Logs all components of type T attached to the GameObject.
            /// </summary>
            /// <typeparam name="T">The type of component to log.</typeparam>
            /// <param name="gameObject">The GameObject whose components to log.</param>
            public static void LogComponents<T>(this GameObject gameObject) where T : Component
            {
                T[] components = gameObject.GetComponents<T>();
                foreach (T component in components)
                {
                    IuvoDebug.DebugLog(component.name);
                }
            }


            /// <summary>
            /// Logs all components of type T attached to the GameObject and its children.
            /// </summary>
            /// <typeparam name="T">The type of component to log.</typeparam>
            /// <param name="gameObject">The GameObject whose components to log.</param>
            public static void LogComponentsInChildren<T>(this GameObject gameObject) where T : Component
            {
                T[] components = gameObject.GetComponentsInChildren<T>();
                foreach (T component in components)
                {
                    IuvoDebug.DebugLog(component.name);
                }
            }

            /// <summary>
            /// Checks if the GameObject has a Rigidbody component.
            /// </summary>
            /// <param name="gameObject">The GameObject to check.</param>
            /// <returns>True if the GameObject has a Rigidbody, otherwise false.</returns>
            public static bool HasRigidbody(this GameObject gameObject)
            {
                return gameObject.GetComponent<Rigidbody>() != null;
            }


            /// <summary>
            /// Enables the specified component on the GameObject.
            /// </summary>
            /// <typeparam name="T">The type of component to enable.</typeparam>
            /// <param name="gameObject">The GameObject to enable the component on.</param>
            public static void EnableComponent<T>(this GameObject gameObject) where T : Behaviour
            {
                T component = gameObject.GetComponent<T>();
                if (component != null)
                {
                    component.enabled = true;
                }
            }


            /// <summary>
            /// Disables the specified component on the GameObject.
            /// </summary>
            /// <typeparam name="T">The type of component to disable.</typeparam>
            /// <param name="gameObject">The GameObject to disable the component on.</param>
            public static void DisableComponent<T>(this GameObject gameObject) where T : Behaviour
            {
                T component = gameObject.GetComponent<T>();
                if (component != null)
                {
                    component.enabled = false;
                }
            }


            /// <summary>
            /// Sets the material color of the GameObject's Renderer.
            /// </summary>
            /// <param name="gameObject">The GameObject to set the material color for.</param>
            /// <param name="color">The color to set.</param>
            public static void SetMaterialColor(this GameObject gameObject, Color color)
            {
                Renderer renderer = gameObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = color;
                }
            }



            /// <summary>
            /// Checks if the GameObject is colliding with another GameObject.
            /// </summary>
            /// <param name="gameObject">The GameObject to check.</param>
            /// <param name="other">The other GameObject to check for collision with.</param>
            /// <returns>True if the GameObject is colliding with the other GameObject, otherwise false.</returns>
            public static bool IsCollidingWith(this GameObject gameObject, GameObject other)
            {
                Collider collider = gameObject.GetComponent<Collider>();
                Collider otherCollider = other.GetComponent<Collider>();
                return collider != null && otherCollider != null && collider.bounds.Intersects(otherCollider.bounds);
            }

            ///// <summary>
            ///// Gets or adds the specified component to the GameObject.
            ///// </summary>
            ///// <typeparam name="T">The type of component to get or add.</typeparam>
            ///// <param name="gameObject">The GameObject to get or add the component to.</param>
            ///// <returns>The component of type T.</returns>
            //public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
            //{
            //    T component = gameObject.GetComponent<T>();
            //    if (component == null)
            //    {
            //        component = gameObject.AddComponent<T>();
            //    }
            //    return component;
            //}


            /// <summary>
            /// Scales all static children with MeshFilter components by the given scale.
            /// </summary>
            /// <param name="parent">The parent GameObject.</param>
            /// <param name="scale">The scale factor to apply.</param>
            /// <param name="includeParent">Whether to include the parent in the scaling.</param>
            public static void ScaleStaticChildrenWithMesh(this GameObject parent, Vector3 scale, bool includeParent = false)
            {
                if (parent == null)
                {
                    IuvoDebug.DebugLogError("Parent GameObject is null.");
                    return;
                }

                // Scale the children.
                foreach (Transform child in parent.transform)
                {
                    if (child.gameObject.isStatic && child.GetComponent<MeshFilter>() != null)
                    {
                        child.localScale = Vector3.Scale(child.localScale, scale);
                    }
                }

                // Scale the parent if specified and it meets the criteria.
                if (includeParent && parent.isStatic && parent.GetComponent<MeshFilter>() != null)
                {
                    parent.transform.localScale = Vector3.Scale(parent.transform.localScale, scale);
                }

            }


            /// <summary>
            /// Scales all non-static children with MeshFilter components by the given scale.
            /// </summary>
            /// <param name="parent">The parent GameObject.</param>
            /// <param name="scale">The scale factor to apply.</param>
            /// <param name="includeParent">Whether to include the parent in the scaling.</param>
            public static void ScaleNonStaticChildrenWithMesh(this GameObject parent, Vector3 scale, bool includeParent = false)
            {
                if (parent == null)
                {
                    IuvoDebug.DebugLogError("Parent GameObject is null.");
                    return;
                }

                // Scale the children that are non-static and have a MeshFilter.
                foreach (Transform child in parent.transform)
                {
                    if (!child.gameObject.isStatic && child.GetComponent<MeshFilter>() != null)
                    {
                        child.localScale = Vector3.Scale(child.localScale, scale);
                    }
                }

                // Optionally scale the parent if it's non-static and meets the criteria.
                if (includeParent && !parent.isStatic && parent.GetComponent<MeshFilter>() != null)
                {
                    parent.transform.localScale = Vector3.Scale(parent.transform.localScale, scale);
                }
            }


            /// <summary>
            /// Gets all colliders attached to a GameObject and its children as an array.
            /// </summary>
            /// <param name="gameObject">The GameObject to search for colliders.</param>
            /// <returns>An array of Colliders.</returns>
            public static Collider[] GetAllColliders(this GameObject gameObject)
            {
                return gameObject.GetComponentsInChildren<Collider>().ToArray();
            }

        }
    }
}