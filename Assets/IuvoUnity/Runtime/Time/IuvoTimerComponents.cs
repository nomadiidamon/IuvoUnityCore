using IuvoUnity.IuvoTime;
using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace _BaseClasses
    {
        namespace _ECS
        {

            public abstract class TimerComponent : IuvoComponentBase
            {

            }

#nullable enable
            public abstract class ECSTimerConfiguration : IuvoConfigurationBase
            {
                // All possible IuvoTimerComponets shuld be nullable variables here
                protected StartOfLife? _startOfLife;
                protected EndOfLife? _endOfLife;
                protected StartOfTime? _startOfTime;
                protected EndOfTime? _endOfTime;
                protected OnEndOfTime? _onEndOfTime;
                protected Pause? _pause;
                protected Running? _running;
                protected Finished? _finished;
                protected BasedOnTimeScale? _basedOnTimeScale;
                protected TimerActivityMode? _timerActivityMode;

                public ECSTimerConfiguration()
                {
                    // assign variables
                }

                public ECSTimerConfiguration(StartOfLife? _startOfLife, EndOfLife? _endOfLife,
                StartOfTime? _startOfTime, EndOfTime? _endOfTime, OnEndOfTime? _onEndOfTime,
                Pause? _pause, Running? _running, Finished? _finished, BasedOnTimeScale? _basedOnTimeScale,
                TimerActivityMode? _timerActivityMode)
                {

                }




#nullable disable
                // add try get funcitons with out params for these

                public bool TryGet<T>(out T component) where T : TimerComponent
                {
                    component = typeof(T) switch
                    {
                        var t when t == typeof(StartOfLife) => _startOfLife as T,
                        var t when t == typeof(EndOfLife) => _endOfLife as T,
                        var t when t == typeof(StartOfTime) => _startOfTime as T,
                        var t when t == typeof(EndOfTime) => _endOfTime as T,
                        var t when t == typeof(OnEndOfTime) => _onEndOfTime as T,
                        var t when t == typeof(Pause) => _pause as T,
                        var t when t == typeof(Running) => _running as T,
                        var t when t == typeof(Finished) => _finished as T,
                        var t when t == typeof(BasedOnTimeScale) => _basedOnTimeScale as T,
                        var t when t == typeof(TimerActivityMode) => _timerActivityMode as T,
                        _ => null
                    };

                    return component != null;
                }

                public class StartOfLife : TimerComponent
                {
                    public float _startOfLife;
                }
                public class EndOfLife : TimerComponent
                {
                    public float _endOfLife;
                }
                public class StartOfTime : TimerComponent
                {
                    public float _startOfTime { get; set; }
                }
                public class EndOfTime : TimerComponent
                {
                    public float _endOfTime { get; set; }
                }

#nullable enable
                public class OnEndOfTime : TimerComponent
                {
                    public System.Action? _OnEndOfTime;
                }
#nullable disable
                public class Pause : TimerComponent
                {
                    public bool _timerIsPaused;
                }
                public class Running : TimerComponent
                {
                    public bool _timerIsRunning;
                }
                public class Finished : TimerComponent
                {
                    public bool _timerIsFinished;
                }
                public class BasedOnTimeScale : TimerComponent
                {
                    public float _timeScale;
                }
                public class TimerActivityMode : TimerComponent
                {
                    public Timer_Activity_Mode _activityMode;
                }
                public class CountLogic : TimerComponent
                {
                    public Tick_Mode _countLogic;
                }

            }
        }

    }
}

