using IuvoUnity.Interfaces;

namespace IuvoUnity
{
    namespace DataStructs
    {
        public class PlayerContext : IContext
        {
            public ActorContext actorContext;
            public PlayerHandlersContext playerHandlers;
            public ConditionContext playerCondition;
            public StatsContext playerStats;
            public TransformContext playerTransform;
            public EventContext playerEvents;

            public PlayerContext()
            {
                actorContext = new ActorContext();
                playerHandlers = new PlayerHandlersContext();
                playerCondition = new ConditionContext();
                playerStats = new StatsContext();
                playerTransform = new TransformContext();
                playerEvents = new EventContext();
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