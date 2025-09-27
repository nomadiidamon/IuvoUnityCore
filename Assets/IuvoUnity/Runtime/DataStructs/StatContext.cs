using IuvoUnity.Interfaces;

namespace IuvoUnity
{
    namespace DataStructs
    {
        public enum ContextKey_PLAYER_STAT { CharcterStats, Score, Deaths, Kills, }

        public class StatContext : IContext
        {
            // CharacterStats class relies on SemiBehavior. THus, SemiBehavior is a dependency of this class
            // and StatContext refactor cannot be done without refactoring SemiBehavior first.

            // TODO: REFACTOR SEMIBEHAVIOR


            public void Dispose()
            {

            }
        }
    }
}