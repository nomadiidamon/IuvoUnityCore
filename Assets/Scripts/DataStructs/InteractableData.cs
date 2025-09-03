using IuvoUnity.BaseClasses;

namespace IuvoUnity
{

    namespace DataStructs
    {
        [System.Serializable]
        public struct InteractableData : IDataStructBase
        {
            public NameData displayName;
            public DataDescription tooltip;
            public bool isEnabled;
        }
    }
}