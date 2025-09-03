
namespace IuvoUnity
{
    namespace Interfaces
    {

        public interface IProximityCondition : IStateACondition
        {
            float GetDistance();
            object GetTarget();
        }
    }

}