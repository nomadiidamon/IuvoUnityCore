using UnityEngine;
using System.Collections.Generic;
using IuvoUnity.Configurations;
using IuvoUnity.DataStructs;
using IuvoUnity.Debug;

namespace IuvoUnity
{

    namespace IuvoTime
    {
        [System.Serializable]
        public struct TimerData
        {
            public Timer_Activity_Mode activityMode;
            public Tick_Mode tickMethod;
            public float duration;
            public float elapsedTime;
            public float remainingTime;
            public List<float> laps;
        }

        [CreateAssetMenu(fileName = "TimerConfiguration", menuName = "IuvoUnity/Configs/TimerConfiguration", order = 2)]
        public class TimerConfiguration : BaseConfig<Timer>
        {
            public TimerData timerData;

            private void OnEnable()
            {
                if (string.IsNullOrEmpty(configName))
                {
                    configName = "TimerConfiguration";
                }
            }

            #region IConfigurable Implementation
            public override void Configure(Timer configurable)
            {
                if (configurable == null)
                {
                    IuvoDebug.DebugLogError("Configurable Timer is null.");
                    return;
                }
                base.Configure(configurable);
                configurable.ConfigureTimer(timerData);
                configurable.OnConfigure();

            }
            #endregion

            #region IReconfigurable Implementation
            public override void Reconfigure(Timer reconfigurable)
            {

                if (reconfigurable == null)
                {
                    IuvoDebug.DebugLogError("Configurable Timer is null.");
                    return;
                }
                base.Reconfigure(reconfigurable);
                reconfigurable.ConfigureTimer(timerData);
                reconfigurable.OnReconfigure();
            }
            #endregion


            public override void PrintInfo()
            {
                base.PrintInfo();
                IuvoDebug.DebugLog(string.Concat(" - Activity Mode: ", timerData.activityMode.ToString()));
                IuvoDebug.DebugLog(string.Concat(" - Tick Method: ", timerData.tickMethod.ToString()));
                IuvoDebug.DebugLog(string.Concat(" - Duration: ", timerData.duration.ToString()));
                IuvoDebug.DebugLog(string.Concat(" - Elapsed Time: ", timerData.elapsedTime.ToString()));
                IuvoDebug.DebugLog(string.Concat(" - Remaining Time: ", timerData.remainingTime.ToString()));
            }
        }
    }
}