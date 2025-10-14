
using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace DataStructs
    {
        public enum PriorityLevel
        {
            None,
            Low,
            Moderate,
            High,
            Emergency
        }

        public interface IPriority : IuvoInterfaceBase
        {
            public PriorityLevel PriorityLevel { get; set; }
            public ClampedData<float> priorityScale { get; set; }
        }
    }
}