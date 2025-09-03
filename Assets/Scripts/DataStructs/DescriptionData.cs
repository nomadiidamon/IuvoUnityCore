using IuvoUnity.BaseClasses;
using System.Collections.Generic;


namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public class DescriptionData : IDataStructBase
        {
            public string _description;
        }

        [System.Serializable]
        public class LongDescriptionData : IDataStructBase
        {
            public List<string> _sentences;
        }
    }
}