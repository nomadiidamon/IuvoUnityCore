using IuvoUnity.IuvoTime;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {

        /// <summary>
        /// TODO: Reasses flow, checks, and presets
        /// </summary>
        [System.Serializable]
        public class LoopingTimer
        {
            public IncrementTimer incrementTimer;
            public DecrementTimer decrementTimer;
            public bool IsLooping;
            public bool IsRunning => incrementTimer.IsRunning || decrementTimer.IsRunning;
            public bool IsGoingUp => incrementTimer.IsRunning;

            public LoopingTimer()
            {
                incrementTimer = new IncrementTimer { duration = 1.0f, OnFinished = new FlexibleEvent() };
                decrementTimer = new DecrementTimer { duration = 1.0f, remainingTime = 1.0f, OnFinished = new FlexibleEvent() };
                IsLooping = false;
            }
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
    }
}
