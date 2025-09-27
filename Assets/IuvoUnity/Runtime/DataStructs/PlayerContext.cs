using IuvoUnity.Interfaces;

namespace IuvoUnity
{
    namespace DataStructs
    {
        public class PlayerContext : IContext
        {
            public ActorContext actorContext;
            public PlayerHandlerContext playerHandlers;
            public ConditionContext playerCondition;
            public StatContext playerStats;
            public TransformContext playerTransform;
            public PlayerEventContext playerEvents;

            public PlayerContext()
            {
                actorContext = new ActorContext();
                playerHandlers = new PlayerHandlerContext();
                playerCondition = new ConditionContext();
                playerStats = new StatContext();
                playerTransform = new TransformContext();
                playerEvents = new PlayerEventContext();
            }
            public void Dispose()
            {
                actorContext?.Dispose();
                playerHandlers?.Dispose();
                playerCondition?.Dispose();
                playerStats?.Dispose();
                playerTransform?.Dispose();
                playerEvents?.Dispose();
            }

        }
    }
}