using UnityEngine;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        public class SaveToPrefabComponent : MonoBehaviour
        {
            public string savedAs;
            public bool hasBeenSaved = false;
            public Vector3 newScale = Vector3.one;


            public void ScaleAndSave(Vector3 scale, string path)
            {
                savedAs = PrefabSaver.ScaleAndSaveAsPrefab(this.gameObject, scale, path);
                hasBeenSaved = true;
            }

            public void Save(string path)
            {
                savedAs = PrefabSaver.SaveAsPrefab(this.gameObject, path);
                hasBeenSaved = true;
            }

            public bool HasBeenSaved()
            {
                return hasBeenSaved;
            }

            public string GetSavedAddress()
            {
                return savedAs;
            }
        }
    }
}