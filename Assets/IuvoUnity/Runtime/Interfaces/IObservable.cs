using IuvoUnity.BaseClasses;
using System.Collections.Generic;

namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface IObservable : IuvoInterfaceBase
        {
            List<IObserver> Observers { get; }
            void AddObserver(IObserver observer);
            void RemoveObserver(IObserver observer);
            void NotifyObservers();
        }
    }
}