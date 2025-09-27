using IuvoUnity.BaseClasses;
using System.Collections.Generic;

namespace IuvoUnity
{
    namespace DataStructs
    {
        [System.Serializable]
        public class StopwatchTimer : IDataStructBase
        {
            public float elapsed;
            public List<float> splitLaps = new List<float>();
            public List<float> absoluteLaps = new List<float>();
            public bool HasStarted;
            public bool IsPaused;
            public bool IsRunning => HasStarted && !IsPaused;
            public FlexibleEvent OnTick;

            public StopwatchTimer()
            {
                elapsed = 0.0f;
                HasStarted = false;
                IsPaused = false;
                OnTick = new FlexibleEvent();
            }
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
                absoluteLaps.Add(elapsed);
            }
            public override string ToString()
            {
                return $"StopwatchTimer: Elapsed={elapsed}, HasStarted={HasStarted}, IsPaused={IsPaused}, IsRunning={IsRunning}, SplitLapsCount={(splitLaps != null ? splitLaps.Count : 0)}, AbsoluteLapsCount={(absoluteLaps != null ? absoluteLaps.Count : 0)}";
            }
        }
    }
}