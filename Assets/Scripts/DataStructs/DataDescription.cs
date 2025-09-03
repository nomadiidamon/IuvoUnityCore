using IuvoUnity.BaseClasses;
using System.Collections.Generic;


namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public class DataDescription : IDataStructBase
        {
            public string _description;
        }

        [System.Serializable]
        public class DataLongDescription : IDataStructBase
        {
            public List<string> _sentences;
        }
    }
}