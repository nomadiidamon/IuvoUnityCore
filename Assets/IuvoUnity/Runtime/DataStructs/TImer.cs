using IuvoUnity.BaseClasses;
using IuvoUnity.Debug;
using IuvoUnity.Interfaces;
using IuvoUnity.IuvoTime;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public struct IncrementTimer : IDataStructBase
        {
            public float duration;
            public float elapsed;
            public bool HasStarted;
            public bool IsPaused;
            public bool IsFinished => elapsed >= duration;
            public bool IsRunning => HasStarted && !IsPaused && !IsFinished;
            public FlexibleEvent OnFinished;
            public bool onFinishedInvoked;

            //public IncrementTimer()
            //{
            //    duration = 1.0f;
            //    elapsed = 0.0f;
            //    HasStarted = false;
            //    IsPaused = false;
            //    onFinishedInvoked = false;
            //    OnFinished = new FlexibleEvent();
            //}
            public IncrementTimer(float timerDuration)
            {
                if (timerDuration <= 0)
                {
                    IuvoDebug.DebugLogWarning("Increment Timer duration must be greater than zero. Flipping value to positive");
                    duration = Mathf.Abs(timerDuration);
                }
                else
                {
                    duration = timerDuration;
                }

                elapsed = 0.0f;
                HasStarted = false;
                IsPaused = false;
                onFinishedInvoked = false;
                OnFinished = new FlexibleEvent();
            }
            public void Dispose()
            {
                OnFinished.RemoveAllFlexibleEventListeners();
            }

            public void Start()
            {
                if (HasStarted) return;
                HasStarted = true;
                IsPaused = false;
            }
            public void Restart()
            {
                Reset();
                HasStarted = true;
                IsPaused = false;
            }
            public void Pause() => IsPaused = true;
            public void Resume() => IsPaused = false;
            public void Reset()
            {
                elapsed = 0;
                HasStarted = false;
                IsPaused = false;
                onFinishedInvoked = false;
            }
            public void Tick(float deltaTime)
            {
                if (!IsRunning) return;


                elapsed += deltaTime;
                if (IsFinished && !onFinishedInvoked)
                {
                    OnFinished.Invoke();
                    onFinishedInvoked = true;
                }
            }
            public override string ToString()
            {
                return $"IncrementTimer: Duration={duration}, Elapsed={elapsed}, HasStarted={HasStarted}, IsPaused={IsPaused}, IsFinished={IsFinished}, IsRunning={IsRunning}";
            }
        }


        [System.Serializable]
        public struct DecrementTimer : IDataStructBase
        {
            public float duration;
            public float remainingTime;
            public bool HasStarted;
            public bool IsPaused;
            public bool IsFinished => remainingTime <= 0;
            public bool IsRunning => !IsFinished && HasStarted && !IsPaused;
            public FlexibleEvent OnFinished;
            public bool onFinishedInvoked;

            //public DecrementTimer()
            //{
            //    duration = 1.0f;
            //    remainingTime = 1.0f;
            //    HasStarted = false;
            //    IsPaused = false;
            //    onFinishedInvoked = false;
            //    OnFinished = new FlexibleEvent();
            //}
            public DecrementTimer(float timerDuration)
            {
                if (timerDuration <= 0)
                {
                    IuvoDebug.DebugLogWarning("Decrement Timer duration must be greater than zero. Flipping value to positive");
                    duration = Mathf.Abs(timerDuration);
                }
                else
                {
                    duration = timerDuration;
                }
                remainingTime = timerDuration;
                HasStarted = false;
                IsPaused = false;
                onFinishedInvoked = false;
                OnFinished = new FlexibleEvent();
            }
            public void Dispose()
            {
                OnFinished.RemoveAllFlexibleEventListeners();
            }

            public void Start()
            {
                if (HasStarted) return;
                HasStarted = true;
                IsPaused = false;
            }
            public void Restart()
            {
                Reset();
                HasStarted = true;
                IsPaused = false;
            }
            public void Pause() => IsPaused = true;
            public void Resume() => IsPaused = false;
            public void Reset()
            {
                remainingTime = duration;
                HasStarted = false;
                IsPaused = false;
                onFinishedInvoked = false;
            }
            public void Tick(float deltaTime)
            {
                if (!IsRunning) return;

                remainingTime -= deltaTime;
                if (IsFinished && !onFinishedInvoked)
                {
                    OnFinished.Invoke();
                    onFinishedInvoked = true;
                }
            }
            public override string ToString()
            {
                return $"DecrementTimer: Duration={duration}, RemainingTime={remainingTime}, HasStarted={HasStarted}, IsPaused={IsPaused}, IsFinished={IsFinished}, IsRunning={IsRunning}";
            }
        }

        [System.Serializable]
        public struct StopwatchTimer : IDataStructBase
        {
            public float elapsed;
            public List<float> splitLaps;//= new List<float>();
            public List<float> absoluteLaps;// = new List<float>();
            public bool HasStarted;
            public bool IsPaused;
            public bool IsRunning => HasStarted && !IsPaused;
            public FlexibleEvent OnTick;

            //public StopwatchTimer()
            //{
            //    elapsed = 0.0f;
            //    HasStarted = false;
            //    IsPaused = false;
            //    OnTick = new FlexibleEvent();
            //}
            public void Dispose()
            {
                OnTick.RemoveAllFlexibleEventListeners();
            }

            public void Start()
            {
                if (HasStarted) return;
                HasStarted = true;
                IsPaused = false;
            }
            public void Restart()
            {
                Reset();
                HasStarted = true;
                IsPaused = false;
            }
            public void Pause() => IsPaused = true;
            public void Resume() => IsPaused = false;
            public void Reset()
            {
                elapsed = 0;
                HasStarted = false;
                IsPaused = false;

                splitLaps?.Clear();
                absoluteLaps?.Clear();
            }
            public void Tick(float deltaTime)
            {
                if (!IsRunning) return;

                elapsed += deltaTime;
                OnTick.Invoke();
            }
            // adds both a split lap and an absolute lap
            public void AddLap()
            {
                AddSplitLap();
                AddAbsoluteLap();
            }
            // adds the time since the last split lap to the split laps list
            public void AddSplitLap()
            {
                if (splitLaps == null)
                {
                    splitLaps = new List<float>();
                }
                float val = elapsed;
                if (splitLaps.Count > 0)
                {
                    val -= splitLaps[splitLaps.Count - 1];
                }
                splitLaps.Add(val);
            }
            // only adds the total elapsed time to the absolute laps list
            public void AddAbsoluteLap()
            {
                if (absoluteLaps == null)
                {
                    absoluteLaps = new List<float>();
                }
                absoluteLaps.Add(elapsed);
            }
            public override string ToString()
            {
                return $"StopwatchTimer: Elapsed={elapsed}, HasStarted={HasStarted}, IsPaused={IsPaused}, IsRunning={IsRunning}, SplitLapsCount={(splitLaps != null ? splitLaps.Count : 0)}, AbsoluteLapsCount={(absoluteLaps != null ? absoluteLaps.Count : 0)}";
            }
        }


        /// <summary>
        /// TODO: Reasses flow, checks, and presets
        /// </summary>
        [System.Serializable]
        public struct LoopingTimer
        {
            public IncrementTimer incrementTimer;
            public DecrementTimer decrementTimer;
            public bool IsLooping;
            public bool IsRunning => incrementTimer.IsRunning || decrementTimer.IsRunning;
            public bool IsGoingUp => incrementTimer.IsRunning;

            //public LoopingTimer()
            //{
            //    incrementTimer = new IncrementTimer { duration = 1.0f, OnFinished = new FlexibleEvent() };
            //    decrementTimer = new DecrementTimer { duration = 1.0f, remainingTime = 1.0f, OnFinished = new FlexibleEvent() };
            //    IsLooping = false;
            //}
            public LoopingTimer(IncrementTimer incrementTimer, DecrementTimer decrementTimer, bool isLooping)
            {
                this.incrementTimer = incrementTimer;
                this.decrementTimer = decrementTimer;
                IsLooping = isLooping;
            }
            public LoopingTimer(float incrementTime, float decrementTime, bool isLooping)
            {
                incrementTimer = new IncrementTimer { duration = incrementTime, OnFinished = new FlexibleEvent() };
                decrementTimer = new DecrementTimer { duration = decrementTime, remainingTime = decrementTime, OnFinished = new FlexibleEvent() };
                IsLooping = isLooping;
            }
            public void Dispose()
            {
                incrementTimer.Dispose();
                decrementTimer.Dispose();
            }

            public void Start(Timer_Activity_Mode mode)
            {
                if (TimeKeeper.IsIncrement(mode) && !incrementTimer.HasStarted)
                {
                    incrementTimer.Start();
                }
                else if (TimeKeeper.IsDecrement(mode) && !decrementTimer.HasStarted)
                {
                    decrementTimer.Start();
                }
                IsLooping = true;
            }
            public void Pause(Timer_Activity_Mode mode)
            {
                if (TimeKeeper.IsIncrement(mode))
                {
                    incrementTimer.Pause();
                }
                else if (TimeKeeper.IsDecrement(mode))
                {
                    decrementTimer.Pause();
                }
            }
            public void Resume(Timer_Activity_Mode mode)
            {
                if (TimeKeeper.IsIncrement(mode))
                {
                    incrementTimer.Resume();
                }
                else if (TimeKeeper.IsDecrement(mode))
                {
                    decrementTimer.Resume();
                }
            }
            // Resets the appropriate timer based on the activity mode. Call after single use
            public void Reset(Timer_Activity_Mode mode)
            {
                if (TimeKeeper.IsIncrement(mode))
                {
                    incrementTimer.Reset();
                }
                else if (TimeKeeper.IsDecrement(mode))
                {
                    decrementTimer.Reset();
                }
            }
            public void Tick(Timer_Activity_Mode activityMode, float deltaTime)
            {

                if (TimeKeeper.IsIncrement(activityMode) && incrementTimer.IsRunning)
                {
                    incrementTimer.Tick(deltaTime);
                    if (incrementTimer.IsFinished)
                    {
                        incrementTimer.Reset();
                        if (!IsLooping) return;
                        incrementTimer.Start();
                    }
                }
                else if (TimeKeeper.IsDecrement(activityMode) && decrementTimer.IsRunning)
                {
                    decrementTimer.Tick(deltaTime);
                    if (decrementTimer.IsFinished)
                    {
                        decrementTimer.Reset();
                        if (!IsLooping) return;
                        decrementTimer.Start();
                    }
                }
            }
            public void StopLooping()
            {
                IsLooping = false;
                //incrementTimer.Reset();
                //decrementTimer.Reset();
            }
            public override string ToString()
            {
                return $"LoopingTimer:\n {incrementTimer.ToString()}\n {decrementTimer.ToString()}";
            }
        }

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

                stopwatchTimer = new StopwatchTimer();
                countdownTimer = new DecrementTimer(defaultDuration);
                countUpTimer = new IncrementTimer (defaultDuration);

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

        [System.Serializable]
        public class CustomTimer : MonoBehaviour, IDataStructBase
        {
            public MultiTimer timer;

            void Awake()
            {
                timer = new MultiTimer();
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

            public void OnDestroy()
            {
                timer.Dispose();
            }
        }



        public class Timer : MonoBehaviour, IDataStructBase, ICreatable, IDestructible, IConfigurable, IReconfigurable
        {
            public Timer_Activity_Mode activityMode;
            public Tick_Mode tickMode;
            public MultiTimer timer;


            private void InitTimer()
            {
                timer = new MultiTimer();
                timer.Initialize();
            }

            void Awake() => InitTimer();

            public void SetDuration(float newDuration)
            {
                timer.SetDuration(newDuration, activityMode);
            }
            public float Duration()
            {
                return timer.GetDuration(activityMode);
            }
            public float Elapsed()
            {
                return timer.Elapsed(activityMode);
            }
            public float Remaining()
            {
                return timer.Remaining(activityMode);
            }

            public bool IsRunning(Timer_Activity_Mode activityMode)
            {
                return timer.IsRunning(activityMode);
            }

            #region TimerFlow
            public void StartTimer()
            {
                timer.Start(activityMode);
            }
            public async Task AwaitFinish()
            {
                var tcs = new TaskCompletionSource<bool>();


                void Handler()
                {
                    tcs.TrySetResult(true);
                }

                if (activityMode == Timer_Activity_Mode.INCREMENT)
                {
                    timer.countUpTimer.OnFinished.AddListener(Handler);
                }
                else if (activityMode == Timer_Activity_Mode.DECREMENT)
                {
                    timer.countdownTimer.OnFinished.AddListener(Handler);
                }
                else
                {
                    throw new System.NotSupportedException("Stopwatch timer does not finish by itself.");
                }

                if (!timer.IsRunning(activityMode))
                {
                    StartTimer();
                }
                else if (timer.IsRunning(activityMode))
                {
                    IuvoDebug.DebugLogWarning("Timer is already running. AwaitFinish will return when the current timer finishes.");
                }
                await tcs.Task;

                // Cleanup listener
                if (activityMode == Timer_Activity_Mode.INCREMENT)
                {
                    timer.countUpTimer.OnFinished.RemoveListener(Handler);
                }
                else if (activityMode == Timer_Activity_Mode.DECREMENT)
                {
                    timer.countdownTimer.OnFinished.RemoveListener(Handler);
                }
            }

            public void UpdateTimer()
            {
                timer.Tick(activityMode, Time.deltaTime);
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

            public void PauseTimer()
            {
                timer.Pause(activityMode);
            }

            public void ResumeTimer()
            {
                timer.Resume(activityMode);
            }

            public void ResetTimer()
            {
                timer.Reset(activityMode);
            }


            #endregion

            #region ICreateable Implementation
            public void OnCreate() => InitTimer();
            #endregion

            #region IDestructible Implementation
            public void OnDestroy()
            {
                timer.Dispose();
            }

            #endregion
            public void Destroy()
            {
                Destroy(this);
            }

            public void ConfigureTimer(TimerData timerData)
            {
                activityMode = timerData.activityMode;
                tickMode = timerData.tickMethod;
                ResetTimer();
                SetDuration(timerData.duration);
            }

            #region IConfigurable Implementation
            public bool Configured { get; set; }
            public virtual void OnConfigure()
            {
                Configured = true;
            }

            #endregion

            #region IReconfigurable Implementation
            public bool Reconfigured { get; set; }
            public virtual void OnReconfigure()
            {
                Reconfigured = true;
            }

            #endregion
        }
    }
}
