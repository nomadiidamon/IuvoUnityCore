
using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace Interfaces
    {

        public interface IObserver : IuvoInterfaceBase
        {
            bool IsConditionMet(IObservable subject);
            void OnNotify(IObservable subject);

        }
    }
}