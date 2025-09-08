
namespace IuvoUnity
{
    namespace Interfaces
    {

        public interface IObserver
        {
            bool IsConditionMet(IObservable subject);
            void OnNotify(IObservable subject);

        }
    }
}