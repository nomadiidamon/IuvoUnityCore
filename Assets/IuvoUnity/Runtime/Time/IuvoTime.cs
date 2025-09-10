using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace IuvoTime
    {
        public static class TimeKeeper
        {
            public static float SUPER_LONG_TERM_MAX = 18000000; // 5000 hours
            public static float SUPER_LONG_TERM_MIN = 36000.0f; // 10 hours

            public static float LONG_TERM_MAX = 35999.9f; // 10 hours
            public static float LONG_TERM_MIN = 300.0f; // 5 minutes

            public static float SHORT_TERM_MAX = 259.9f; // 4 minutes
            public static float SHORT_TERM_MIN = 0.5f; // half a second

            public static TimerFactory factory;

            #region TimerUtilities
            public static bool IsDecrement(Timer_Activity_Mode mode)
            {
                if (mode == Timer_Activity_Mode.SUPER_LONG_TERM_DECREMENT ||
                       mode == Timer_Activity_Mode.LONG_TERM_DECREMENT ||
                       mode == Timer_Activity_Mode.SHORT_DECREMENT ||
                       mode == Timer_Activity_Mode.DECREMENT)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static bool IsIncrement(Timer_Activity_Mode mode)
            {
                if (mode == Timer_Activity_Mode.SUPER_LONG_TERM_INCREMENT ||
                       mode == Timer_Activity_Mode.LONG_TERM_INCREMENT ||
                       mode == Timer_Activity_Mode.SHORT_TERM_INCREMENT ||
                       mode == Timer_Activity_Mode.INCREMENT)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static bool IsStopwatch(Timer_Activity_Mode mode)
            {
                if (mode == Timer_Activity_Mode.SUPER_LONG_TERM_STOPWATCH ||
                       mode == Timer_Activity_Mode.LONG_TERM_STOPWATCH ||
                       mode == Timer_Activity_Mode.SHORT_TERM_STOPWATCH ||
                       mode == Timer_Activity_Mode.STOPWATCH)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static bool IsCustomTimer(Timer_Activity_Mode mode)
            {
                if (mode == Timer_Activity_Mode.CUSTOM_TIMER)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

#endregion


        }

        public enum Timer_Activity_Mode
        {
            SUPER_LONG_TERM_DECREMENT, LONG_TERM_DECREMENT, SHORT_DECREMENT, DECREMENT,
            SUPER_LONG_TERM_INCREMENT, LONG_TERM_INCREMENT, SHORT_TERM_INCREMENT, INCREMENT,
            SUPER_LONG_TERM_STOPWATCH, LONG_TERM_STOPWATCH, SHORT_TERM_STOPWATCH, STOPWATCH,
            CUSTOM_TIMER, UNKNOWN
        }


        public enum Tick_Mode
        {
            TICK_ON_UPDATE,
            TICK_ON_FIXED_UPDATE,
            TICK_ON_LATE_UPDATE,
            TICK_ON_REALTIME_UPDATE,
            TICK_ON_MANUAL_CALL
        }

    }
}
