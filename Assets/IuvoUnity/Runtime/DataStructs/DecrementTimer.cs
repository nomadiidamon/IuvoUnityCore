using IuvoUnity.BaseClasses;
using IuvoUnity.Events;
using IuvoUnity.Debug;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public class DecrementTimer : IDataStructBase
        {
            public float duration;
            public float remainingTime;
            public bool HasStarted;
            public bool IsPaused;
            public bool IsFinished => remainingTime <= 0;
            public bool IsRunning => !IsFinished && HasStarted && !IsPaused;
            public FlexibleEvent OnFinished;
            public bool onFinishedInvoked;

            public DecrementTimer()
            {
                duration = 1.0f;
                remainingTime = 1.0f;
                HasStarted = false;
                IsPaused = false;
                onFinishedInvoked = false;
                OnFinished = new FlexibleEvent();
            }
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

    }

}