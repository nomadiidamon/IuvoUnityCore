using System.Collections.Concurrent;
using IuvoUnity.Interfaces;


namespace IuvoUnity
{
    namespace DataStructs
    {
        public enum ContextKey_PLAYER_HANDLERS
        { AimHandler, AnimatorHandler, CameraHandler, CombatHandler, InputHandler, MovementHandler, RotationHandler, StatHandler, UIHandler, PlayerManager }

        public class PlayerHandlersContext : IContext
        {
            private ConcurrentDictionary<ContextKey_PLAYER_HANDLERS, IPlayerHandler> handlers = new ConcurrentDictionary<ContextKey_PLAYER_HANDLERS, IPlayerHandler>();

            public PlayerHandlersContext()
            {
                foreach (ContextKey_PLAYER_HANDLERS key in System.Enum.GetValues(typeof(ContextKey_PLAYER_HANDLERS)))
                {
                    handlers[key] = null;
                }
            }

            public void Dispose()
            {
                ClearHandlers();
            }

            public T GetHandler<T>(ContextKey_PLAYER_HANDLERS key) where T : class, IPlayerHandler
            {
                return handlers.TryGetValue(key, out var handler) ? handler as T : null;
            }

            public void SetHandler(ContextKey_PLAYER_HANDLERS key, IPlayerHandler handler)
            {
                handlers[key] = handler;
            }

            private void ClearHandlers()
            {
                handlers.Clear();
            }

            //public PlayerAimHandler GetAimHandler() => GetHandler<PlayerAimHandler>(ContextKey_PLAYER_HANDLER.AimHandler);
            //public PlayerAnimatorHandler GetAnimatorHandler() => GetHandler<PlayerAnimatorHandler>(ContextKey_PLAYER_HANDLER.AnimatorHandler);
            //public PlayerCameraHandler GetCameraHandler() => GetHandler<PlayerCameraHandler>(ContextKey_PLAYER_HANDLER.CameraHandler);
            //public PlayerCombatHandler GetCombatHandler() => GetHandler<PlayerCombatHandler>(ContextKey_PLAYER_HANDLER.CombatHandler);
            //public PlayerInputHandler GetInputHandler() => GetHandler<PlayerInputHandler>(ContextKey_PLAYER_HANDLER.InputHandler);
            //public PlayerMovementHandler GetMovementHandler() => GetHandler<PlayerMovementHandler>(ContextKey_PLAYER_HANDLER.MovementHandler);
            //public PlayerRotationHandler GetRotationHandler() => GetHandler<PlayerRotationHandler>(ContextKey_PLAYER_HANDLER.RotationHandler);
            //public PlayerStatHandler GetStatHandler() => GetHandler<PlayerStatHandler>(ContextKey_PLAYER_HANDLER.StatHandler);
            //public PlayerUIHandler GetUIHandler() => GetHandler<PlayerUIHandler>(ContextKey_PLAYER_HANDLER.UIHandler);
            //public PlayerManager GetPlayerManager() => GetHandler<PlayerManager>(ContextKey_PLAYER_HANDLER.PlayerManager);
        }

    }
}