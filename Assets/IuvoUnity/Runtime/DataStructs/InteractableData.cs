using IuvoUnity.BaseClasses;

namespace IuvoUnity
{

    namespace DataStructs
    {
        [System.Serializable]
        public struct InteractableData : IDataStructBase
        {
            public NameData displayName;
            public DescriptionData tooltip;
            public bool isInteractable;
        }
    }
}