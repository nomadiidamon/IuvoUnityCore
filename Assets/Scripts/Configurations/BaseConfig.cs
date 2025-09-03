using IuvoUnity.Interfaces;
using UnityEngine;
using IuvoUnity.Debug;
using System.IO;

namespace IuvoUnity
{
    namespace Configurations
    {
        public abstract class BaseConfig : ScriptableObject, IConfigurable
        {
            public string configName;

            #region IConfigurable Interface Implementation

            public bool Configured { get; set; } = false;
            public bool Reconfigured { get; set; } = false;

            public abstract void Configure(); // IConfigure method
            public abstract void OnConfigure(); // IConfigure method
            public abstract void OnReconfigure(); // IReconfigure method
            public abstract void Reconfigure();// IReconfigure method
            #endregion

            public virtual string GetConfigSerializePath(string actingConfigClass, string objectToSerializeName, string parentConfigClass = "BaseConfig")
            {
                if (string.IsNullOrWhiteSpace(actingConfigClass))
                {
                    IuvoDebug.DebugLogError("Acting config class name cannot be null or empty.");
                    return null;
                }

                if (string.IsNullOrWhiteSpace(objectToSerializeName))
                {
                    IuvoDebug.DebugLogError("Object to serialize name cannot be null or empty.");
                    return null;
                }

                if (string.Compare(parentConfigClass, "BaseConfig", true) == 0)
                {
                    IuvoDebug.DebugLogWarning("Parent config class is set to BaseConfig. Consider using a more specific parent class.");
                }

                string filePath =  Path.Combine(Application.persistentDataPath, "Configurations" ,parentConfigClass, actingConfigClass, objectToSerializeName + ".json");
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                return filePath;
            }

            public virtual void PrintInfo()
            {
                IuvoDebug.DebugLog(string.Concat("Config: ", configName));
            }
        }
    }
}