using IuvoUnity.Configurations;
using IuvoUnity.DataStructs;
using UnityEngine;

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
            public static bool IsLoopingTimer(Timer_Activity_Mode mode)
            {
                if (mode == Timer_Activity_Mode.LOOPING_TIMER)
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
            public static bool IsSuperLongTerm(Timer_Activity_Mode mode)
            {
                if (mode == Timer_Activity_Mode.SUPER_LONG_TERM_DECREMENT ||
                       mode == Timer_Activity_Mode.SUPER_LONG_TERM_INCREMENT ||
                       mode == Timer_Activity_Mode.SUPER_LONG_TERM_STOPWATCH)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static bool IsLongTerm(Timer_Activity_Mode mode)
            {
                if (mode == Timer_Activity_Mode.SUPER_LONG_TERM_DECREMENT ||
                       mode == Timer_Activity_Mode.LONG_TERM_DECREMENT ||
                       mode == Timer_Activity_Mode.SUPER_LONG_TERM_INCREMENT ||
                       mode == Timer_Activity_Mode.LONG_TERM_INCREMENT ||
                       mode == Timer_Activity_Mode.SUPER_LONG_TERM_STOPWATCH ||
                       mode == Timer_Activity_Mode.LONG_TERM_STOPWATCH)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static bool IsShortTerm(Timer_Activity_Mode mode)
            {
                if (mode == Timer_Activity_Mode.SHORT_DECREMENT ||
                       mode == Timer_Activity_Mode.DECREMENT ||
                       mode == Timer_Activity_Mode.SHORT_TERM_INCREMENT ||
                       mode == Timer_Activity_Mode.INCREMENT ||
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


            public static string FormatTime(float time)
            {
                if (time < 0)
                {
                    return "00:00:00";
                }
                int hours = Mathf.FloorToInt(time / 3600);
                int minutes = Mathf.FloorToInt((time % 3600) / 60);
                int seconds = Mathf.FloorToInt(time % 60);
                int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
                return string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", hours, minutes, seconds, milliseconds);
            }
            public static string FormatTimerDuration(Timer timer)
            {
                if (timer == null)
                {
                    return "00:00:00";
                }
                return FormatTime(timer.Duration());
            }
            public static string FormatElapsedTime(Timer timer)
            {
                if (timer == null)
                {
                    return "00:00:00";
                }
                return FormatTime(timer.Elapsed());
            }
            public static string FormatRemainingTime(Timer timer)
            {
                if (timer == null)
                {
                    return "00:00:00";
                }
                return FormatTime(timer.Remaining());
            }


            #endregion

            public static Timer CreateTimer(TimerConfiguration configuration)
            {
                if (factory == null)
                {
                    factory = new TimerFactory();
                }
                return factory.Create(configuration);
            }
        }

        public enum Timer_Activity_Mode
        {
            SUPER_LONG_TERM_DECREMENT, LONG_TERM_DECREMENT, SHORT_DECREMENT, DECREMENT,
            SUPER_LONG_TERM_INCREMENT, LONG_TERM_INCREMENT, SHORT_TERM_INCREMENT, INCREMENT,
            SUPER_LONG_TERM_STOPWATCH, LONG_TERM_STOPWATCH, SHORT_TERM_STOPWATCH, STOPWATCH,
            CUSTOM_TIMER, LOOPING_TIMER, UNKNOWN
        }


        public enum Tick_Mode
        {
            TICK_ON_UPDATE,
            TICK_ON_FIXED_UPDATE,
            TICK_ON_LATE_UPDATE,
            TICK_ON_REALTIME_UPDATE,
            TICK_ON_MANUAL_CALL
        }

        public class TimerFactory
        {
            public Timer Create(TimerConfiguration configuration)
            {
                // Implementation for creating a timer based on the configuration
                GameObject timerObject = new GameObject("Timer");
                Timer timer = timerObject.AddComponent<Timer>();
                if (configuration != null)
                {
                    configuration.Configure(timer);
                    timer.OnConfigure();
                }
                timer.OnCreate();

                return timer;
            }
        }

    }
}
