using IuvoUnity.BaseClasses;
using IuvoUnity.DataStructs;

namespace IuvoUnity
{

    namespace Interfaces
    {
        public interface IPlayerHandler : IuvoInterfaceBase
        {
            PlayerContext playerContext { get; set; }
            ContextKey_PLAYER_HANDLERS HandlerKey { get; }

            public void UpdateHandlerInContext()
            {
                IPlayerHandler handler = this;
                if (handler != null && playerContext != null)
                {
                    // use the handler's type as the value to add to the context
                    playerContext.playerHandlers.SetHandler(HandlerKey, handler);
                }
            }
        }
    }
}