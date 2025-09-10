using IuvoUnity.BaseClasses;
using IuvoUnity.Interfaces;
using IuvoUnity.IuvoTime;
using System.Collections.Generic;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public struct IncrementTimer
        {
            public float duration;
            public float elapsed;

            public bool IsFinished => elapsed >= duration;
            public bool IsRunning => elapsed > 0 && !IsFinished;
            public FlexibleEvent OnFinished;

            public void Reset() => elapsed = 0;
            public void Tick(float deltaTime)
            {
                elapsed += deltaTime;
                if (IsFinished)
                {
                    OnFinished.Invoke();
                }
            }
        }

        [System.Serializable]
        public struct DecrementTimer
        {
            public float duration;
            public float remainingTime;
            public bool IsFinished => remainingTime <= 0;
            public bool IsRunning => remainingTime < duration && !IsFinished;
            public FlexibleEvent OnFinished;
            public void Reset() => remainingTime = duration;
            public void Tick(float deltaTime)
            {
                remainingTime -= deltaTime;
                if (IsFinished)
                {
                    OnFinished.Invoke();
                }
            }
        }

        [System.Serializable]
        public struct StopwatchTimer
        {
            public float elapsed;
            public List<float> laps;
            public bool IsRunning => elapsed > 0;
            public FlexibleEvent OnTick;
            public void Reset() => elapsed = 0;
            public void Tick(float deltaTime)
            {
                elapsed += deltaTime;
                OnTick.Invoke();
            }
            public void Lap()
            {
                if (laps == null)
                {
                    laps = new List<float>();
                }
                laps.Add(elapsed);
            }
        }

        [System.Serializable]
        public struct MultiTimer : IDataStructBase
        {
            public StopwatchTimer stopwatchTimer;
            public DecrementTimer countdownTimer;
            public IncrementTimer countUpTimer;

            bool HasBeenInitialized;

            public void Initialize()
            {
                if (HasBeenInitialized) return;

                HasBeenInitialized = true;
                stopwatchTimer = new StopwatchTimer();
                stopwatchTimer.OnTick = new FlexibleEvent();

                countdownTimer = new DecrementTimer();
                countdownTimer.OnFinished = new FlexibleEvent();

                countUpTimer = new IncrementTimer();
                countUpTimer.OnFinished = new FlexibleEvent();
            }

            public void ResetAll()
            {
                stopwatchTimer.Reset();
                countdownTimer.Reset();
                countUpTimer.Reset();
            }
        }

        [System.Serializable]
        public class CustomTimer : MonoBehaviour, IDataStructBase
        {
            public MultiTimer timer;
            public bool HasStarted = false;
            public bool IsPaused = false;

            void Awake()
            {
                timer = new MultiTimer();
                timer.ResetAll();
                timer.Initialize();
            }

            public virtual void StartTimer()
            {

            }

            public virtual void PauseTimer()
            {

            }

            public virtual void ResumeTimer()
            {

            }

            public virtual void UpdateTimer()
            {

            }

            public virtual void ResetTimer()
            {
            }

        }

        [System.Serializable]
        public class Timer : MonoBehaviour, IDataStructBase, ICreatable, IDestructible
        {
            public Timer_Activity_Mode activityMode;
            public Tick_Mode tickMode;
            public MultiTimer timer;
            public bool HasStarted = false;
            public bool IsPaused = false;

            void Awake()
            {
                timer = new MultiTimer();
                timer.ResetAll();
                timer.Initialize();
            }

            public void SetDuration(float newDuration)
            {
                if (TimeKeeper.IsIncrement(activityMode))
                {
                    timer.countUpTimer.duration = newDuration;
                }
                else if (TimeKeeper.IsDecrement(activityMode))
                {
                    timer.countdownTimer.duration = newDuration;
                    timer.countdownTimer.remainingTime = newDuration;
                }
                else if (TimeKeeper.IsStopwatch(activityMode))
                {

                }
            }
            public float Duration()
            {
                if (TimeKeeper.IsIncrement(activityMode))
                {
                    return timer.countUpTimer.duration;
                }
                else if (TimeKeeper.IsDecrement(activityMode))
                {
                    return timer.countdownTimer.duration;
                }
                else if (TimeKeeper.IsStopwatch(activityMode))
                {
                    return 0f; // Stopwatch has no set duration
                }
                return 0f;
            }
            public float Elapsed()
            {
                if (TimeKeeper.IsIncrement(activityMode))
                {
                    return timer.countUpTimer.elapsed;
                }
                else if (TimeKeeper.IsDecrement(activityMode))
                {
                    return timer.countdownTimer.duration - timer.countdownTimer.remainingTime;
                }
                else if (TimeKeeper.IsStopwatch(activityMode))
                {
                    return timer.stopwatchTimer.elapsed;
                }
                return 0f;
            }

            #region TimerFlow
            public void StartTimer()
            {
                ResetTimer();
                HasStarted = true;
            }

            #region TimerTicking
            public void UpdateTimer()
            {
                if (HasStarted && !IsPaused)
                {
                    if (TimeKeeper.IsIncrement(activityMode) && !timer.countUpTimer.IsFinished)
                    {
                        timer.countUpTimer.Tick(Time.deltaTime);
                    }
                    else if (TimeKeeper.IsDecrement(activityMode) && !timer.countdownTimer.IsFinished)
                    {
                        timer.countdownTimer.Tick(Time.deltaTime);
                    }
                    else if (TimeKeeper.IsStopwatch(activityMode))
                    {
                        timer.stopwatchTimer.Tick(Time.deltaTime);
                    }
                }
            }

            public virtual void Update()
            {
                if (tickMode == Tick_Mode.TICK_ON_UPDATE)
                {
                    UpdateTimer();
                }
            }

            public virtual void FixedUpdate()
            {
                if (tickMode == Tick_Mode.TICK_ON_FIXED_UPDATE)
                {
                    UpdateTimer();
                }
            }

            public virtual void LateUpdate()
            {
                if (tickMode == Tick_Mode.TICK_ON_LATE_UPDATE)
                {
                    UpdateTimer();
                }
            }
            #endregion

            public void PauseTimer()
            {
                IsPaused = true;
            }

            public void ResumeTimer()
            {
                IsPaused = false;
            }

            public void ResetTimer()
            {
                if (TimeKeeper.IsIncrement(activityMode))
                {
                    timer.countUpTimer.Reset();
                }
                else if (TimeKeeper.IsDecrement(activityMode))
                {
                    timer.countdownTimer.Reset();
                }
                else if (TimeKeeper.IsStopwatch(activityMode))
                {
                    timer.stopwatchTimer.Reset();
                }

                HasStarted = false;
                IsPaused = false;
            }

            #endregion

            #region ICreateable Implementation
            public void OnCreate()
            {
                timer = new MultiTimer();
                timer.ResetAll();
                timer.Initialize();
            }
            #endregion

            #region IDesctructible Implementation
            public void OnDestroy()
            {
               
            }

            #endregion
        }
    }
}
