using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace DataStructs
    {

        [System.Serializable]
        public struct Timer : IDataStructBase
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
    }
}
