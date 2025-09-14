using IuvoUnity.Interfaces;
using UnityEngine;
using IuvoUnity.Debug;
using System.IO;

namespace IuvoUnity
{
    namespace Configurations
    {

        public abstract class BaseConfig<T> : ScriptableObject, IConfigure<T>, IReconfigure<T> where T : IConfigurable, IReconfigurable 
        {
            public string configName;

            #region IConfigurable Interface Implementation

            public virtual void Configure(T configurable)
            {

            }
            public virtual void Reconfigure(T reconfigurable)
            {

            }
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