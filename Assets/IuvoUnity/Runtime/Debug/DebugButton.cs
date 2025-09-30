using System;
using IuvoUnity.Events;


namespace IuvoUnity
{
    namespace Debug
    {
        [Serializable]
        public class DebugButton
        {
            public string Label = "Run Debug Action";

            public FlexibleEvent OnClick = new FlexibleEvent();
        }
    }
}