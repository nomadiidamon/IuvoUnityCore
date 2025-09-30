#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        public class PrefabSaver
        {
            public static string SaveAsPrefab(GameObject obj, string path)
            {
                string assetPath = $"Assets/IuvoUnity/Assets/Prefabs/{path}.prefab";
                PrefabUtility.SaveAsPrefabAssetAndConnect(obj, assetPath, InteractionMode.UserAction);
                return assetPath;
            }


            public static string ScaleAndSaveAsPrefab(GameObject obj, Vector3 newScale, string path)
            {
                obj.transform.localScale = newScale;
                string assetPath = $"Assets/IuvoUnity/Assets/Prefabs/{path}.prefab";
                PrefabUtility.SaveAsPrefabAssetAndConnect(obj, assetPath, InteractionMode.UserAction);
                return assetPath;
            }

        }
    }
}
#endif
