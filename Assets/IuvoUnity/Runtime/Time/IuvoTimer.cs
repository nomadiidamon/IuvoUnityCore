using IuvoUnity.ECS;

namespace IuvoUnity
{
    namespace BaseClasses  
    {
        namespace ECS
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
