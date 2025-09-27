using IuvoUnity.BaseClasses;
using IuvoUnity.Debug;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public class IncrementTimer : IDataStructBase
        {
            public float duration;
            public float elapsed;
            public bool HasStarted;
            public bool IsPaused;
            public bool IsFinished => elapsed >= duration;
            public bool IsRunning => HasStarted && !IsPaused && !IsFinished;
            public FlexibleEvent OnFinished;
            public bool onFinishedInvoked;

            public IncrementTimer()
            {
                duration = 1.0f;
                elapsed = 0.0f;
                HasStarted = false;
                IsPaused = false;
                onFinishedInvoked = false;
                OnFinished = new FlexibleEvent();
            }
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

    }
}