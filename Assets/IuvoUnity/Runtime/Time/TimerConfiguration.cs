using UnityEngine;
using IuvoUnity.Configurations;

namespace IuvoUnity
{

    namespace IuvoTime
    {
        public struct TimerData
        {
            public Timer_Activity_Mode activityMode;
            public Tick_Mode tickMethod;
            public float duration;
            public float elapsedTime;
            public float remainingTime;

        }

        [CreateAssetMenu(fileName = "TimerConfiguration", menuName = "IuvoUnity/Configs/TimerConfiguration", order = 2)]
        public class TimerConfiguration : BaseConfig
        {
            public TimerData timerData;

            private void OnEnable()
            {
                if (string.IsNullOrEmpty(configName))
                {
                    configName = "{name}";
                }
                timerData = new TimerData();
            }


            public override void Configure()
            {
                // Implement configuration logic here
            }

            public override void OnConfigure()
            {
                // Implement post-configuration logic here
            }

            public override void Reconfigure()
            {
                // Implement reconfiguration logic here
            }

            public override void OnReconfigure()
            {
                // Implement post-reconfiguration logic here
            }

            public override void PrintInfo()
            {
                base.PrintInfo();
                // Additional info if needed
            }
        }

    }

}