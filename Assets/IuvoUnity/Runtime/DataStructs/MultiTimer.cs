using IuvoUnity.BaseClasses;
using IuvoUnity.Events;
using IuvoUnity.Debug;
using IuvoUnity.IuvoTime;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public class MultiTimer : IDataStructBase
        {
            public StopwatchTimer stopwatchTimer;
            public DecrementTimer countdownTimer;
            public IncrementTimer countUpTimer;
            //public LoopingTimer loopingTimer;
            bool HasBeenInitialized;

            public MultiTimer()
            {
                HasBeenInitialized = false;
                stopwatchTimer = new StopwatchTimer();
                countdownTimer = new DecrementTimer();
                countUpTimer = new IncrementTimer();
                //loopingTimer = new LoopingTimer();
            }
            public MultiTimer(float defaultDuration)
            {
                HasBeenInitialized = false;
                stopwatchTimer = new StopwatchTimer();
                countdownTimer = new DecrementTimer(defaultDuration);
                countUpTimer = new IncrementTimer(defaultDuration);
                //loopingTimer = new LoopingTimer(defaultDuration, defaultDuration, true);
            }

            public void Dispose()
            {
                stopwatchTimer.Dispose();
                countdownTimer.Dispose();
                countUpTimer.Dispose();
                //loopingTimer.Dispose();
            }

            public void Start(Timer_Activity_Mode mode)
            {
                if (TimeKeeper.IsIncrement(mode) && !countUpTimer.HasStarted)
                {
                    countUpTimer.Start();
                }
                else if (TimeKeeper.IsDecrement(mode) && !countdownTimer.HasStarted)
                {
                    countdownTimer.Start();
                }
                else if (TimeKeeper.IsStopwatch(mode) && !stopwatchTimer.HasStarted)
                {
                    stopwatchTimer.Start();
                }
                //else if (TimeKeeper.IsLoopingTimer(mode) && !loopingTimer.IsRunning)
                //{
                //    loopingTimer.Start(Timer_Activity_Mode.INCREMENT); // Default to increment for looping timer
                //}
            }
            public void Pause(Timer_Activity_Mode mode)
            {
                if (TimeKeeper.IsIncrement(mode))
                {
                    countUpTimer.Pause();
                }
                else if (TimeKeeper.IsDecrement(mode))
                {
                    countdownTimer.Pause();
                }
                else if (TimeKeeper.IsStopwatch(mode))
                {
                    stopwatchTimer.Pause();
                }
                //else if (TimeKeeper.IsLoopingTimer(mode))
                //{
                //    if (loopingTimer.IsGoingUp) // Default to increment for looping timer
                //    {
                //        loopingTimer.incrementTimer.Pause();
                //    }
                //    else
                //    {
                //        loopingTimer.decrementTimer.Pause();
                //    }
                //}
            }
            public void Resume(Timer_Activity_Mode mode)
            {
                if (TimeKeeper.IsIncrement(mode))
                {
                    countUpTimer.Resume();
                }
                else if (TimeKeeper.IsDecrement(mode))
                {
                    countdownTimer.Resume();
                }
                else if (TimeKeeper.IsStopwatch(mode))
                {
                    stopwatchTimer.Resume();
                }
                //else if (TimeKeeper.IsLoopingTimer(mode))
                //{
                //    if (loopingTimer.IsGoingUp) // Default to increment for looping timer
                //    {
                //        loopingTimer.incrementTimer.Resume();
                //    }
                //    else
                //    {
                //        loopingTimer.decrementTimer.Resume();
                //    }
                //}
            }
            // Resets the appropriate timer based on the activity mode. Call after single use
            public void Reset(Timer_Activity_Mode mode)
            {
                if (TimeKeeper.IsIncrement(mode))
                {
                    countUpTimer.Reset();
                }
                else if (TimeKeeper.IsDecrement(mode))
                {
                    countdownTimer.Reset();
                }
                else if (TimeKeeper.IsStopwatch(mode))
                {
                    stopwatchTimer.Reset();
                }
                //else if (TimeKeeper.IsLoopingTimer(mode))
                //{
                //    loopingTimer.Reset(mode);
                //}
            }
            public void SetDuration(float newDuration, Timer_Activity_Mode activityMode)
            {
                if (TimeKeeper.IsIncrement(activityMode))
                {
                    if (newDuration <= 0)
                    {
                        IuvoDebug.DebugLogWarning("Count Up Timer duration must be greater than zero. Flipping value to positive");
                        newDuration = Mathf.Abs(newDuration);
                    }
                    countUpTimer.duration = newDuration;
                }
                else if (TimeKeeper.IsDecrement(activityMode))
                {
                    if (newDuration <= 0)
                    {
                        IuvoDebug.DebugLogWarning("Countdown Timer duration must be greater than zero. Flipping value to positive");
                        newDuration = Mathf.Abs(newDuration);
                    }
                    countdownTimer.duration = newDuration;
                    countdownTimer.remainingTime = newDuration;
                }
                else if (TimeKeeper.IsStopwatch(activityMode))
                {
                }
            }
            public float GetDuration(Timer_Activity_Mode activityMode)
            {
                if (TimeKeeper.IsIncrement(activityMode))
                {
                    return countUpTimer.duration;
                }
                else if (TimeKeeper.IsDecrement(activityMode))
                {
                    return countdownTimer.duration;
                }
                else if (TimeKeeper.IsStopwatch(activityMode))
                {
                    IuvoDebug.DebugLogWarning("Stopwatch Timer has no set duration.");
                    return 0f; // Stopwatch has no set duration
                }
                else if (TimeKeeper.IsCustomTimer(activityMode))
                {
                    IuvoDebug.DebugLogWarning("Custom Timer has no set duration.");
                    return 0f; // Custom Timer has no set duration
                }
                return 0f;
            }
            public float Elapsed(Timer_Activity_Mode activityMode)
            {
                if (TimeKeeper.IsIncrement(activityMode))
                {
                    return countUpTimer.elapsed;
                }
                else if (TimeKeeper.IsDecrement(activityMode))
                {
                    return countdownTimer.duration - countdownTimer.remainingTime;
                }
                else if (TimeKeeper.IsStopwatch(activityMode))
                {
                    return stopwatchTimer.elapsed;
                }
                return 0f;
            }
            public float Remaining(Timer_Activity_Mode activityMode)
            {
                if (TimeKeeper.IsIncrement(activityMode))
                {
                    return countUpTimer.duration - countUpTimer.elapsed;
                }
                else if (TimeKeeper.IsDecrement(activityMode))
                {
                    return countdownTimer.remainingTime;
                }
                else if (TimeKeeper.IsStopwatch(activityMode))
                {
                    IuvoDebug.DebugLogWarning("Stopwatch Timer has no remaining time.");
                    return 0f; // Stopwatch has no remaining time
                }
                return 0f;
            }

            public void Tick(Timer_Activity_Mode activityMode, float deltaTime)
            {
                if (TimeKeeper.IsIncrement(activityMode) && countUpTimer.IsRunning)
                {
                    countUpTimer.Tick(deltaTime);
                }
                else if (TimeKeeper.IsDecrement(activityMode) && countdownTimer.IsRunning)
                {
                    countdownTimer.Tick(deltaTime);
                }
                else if (TimeKeeper.IsStopwatch(activityMode) && stopwatchTimer.IsRunning)
                {
                    stopwatchTimer.Tick(deltaTime);
                }
            }

            public bool IsRunning(Timer_Activity_Mode activityMode)
            {
                if (TimeKeeper.IsIncrement(activityMode))
                {
                    return countUpTimer.IsRunning;
                }
                else if (TimeKeeper.IsDecrement(activityMode))
                {
                    return countdownTimer.IsRunning;
                }
                else if (TimeKeeper.IsStopwatch(activityMode))
                {
                    return stopwatchTimer.IsRunning;
                }
                return false;
            }

            public void Initialize(float defaultDuration = 1.0f)
            {
                if (HasBeenInitialized) return;
                HasBeenInitialized = true;

                stopwatchTimer = new StopwatchTimer { OnTick = new FlexibleEvent() };
                countdownTimer = new DecrementTimer { duration = defaultDuration, remainingTime = defaultDuration, OnFinished = new FlexibleEvent() };
                countUpTimer = new IncrementTimer { duration = defaultDuration, OnFinished = new FlexibleEvent() };

                ResetAll();
            }

            // Resets all timers. Call on creation, destruction, and reconfiguration.
            public void ResetAll()
            {
                stopwatchTimer.Reset();
                countdownTimer.Reset();
                countUpTimer.Reset();
            }

            public override string ToString()
            {
                return $"MultiTimer:\n {stopwatchTimer.ToString()}\n {countdownTimer.ToString()}\n {countUpTimer.ToString()}";
            }
        }
    }
}
