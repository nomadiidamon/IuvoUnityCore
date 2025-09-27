using IuvoUnity.BaseClasses;
using IuvoUnity.Debug;
using IuvoUnity.Interfaces;
using IuvoUnity.IuvoTime;
using System.Threading.Tasks;
using UnityEngine;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
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
