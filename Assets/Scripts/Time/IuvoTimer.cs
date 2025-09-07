using IuvoUnity._ECS;

namespace IuvoUnity
{
    namespace _BaseClasses  
    {
        namespace _ECS
        {
            public class IuvoTimer
            {
                public IuvoEntity _myIuvoEntity { get; set; }

                public IuvoTimer()
                {
                    var timer = IuvoEntityRegistry.CreateTimerEntity(true);
                    timer._ComponentManager.TryGetComponent<IuvoWorldID>(out var id);
                    _myIuvoEntity = id._entity;
                }


            }
        }
    }
}
