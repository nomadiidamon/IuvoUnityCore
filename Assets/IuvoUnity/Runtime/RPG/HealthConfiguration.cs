using IuvoUnity.Debug;
using IuvoUnity.BaseClasses;
using UnityEngine;
using System.Collections.Generic;

namespace IuvoUnity
{
    namespace Configurations
    {
        [CreateAssetMenu(fileName = "New Health Configuration", menuName = "IuvoUnity/RPG/Health Configuration")]
        public class HealthConfiguration : BaseConfig
        {
            [SerializeField]
            private AnimationCurve DefaultHealthGrowth = new AnimationCurve(
                new Keyframe(1, 100),
                new Keyframe(25, 25000),
                new Keyframe(50, 150000),
                new Keyframe(75, 2500000),
                new Keyframe(100, 50000000)
            );

            public int BaseHealth = 100;

            [SerializeField] private AnimationCurve healthCurve;
            [SerializeField] public int MaxLevel => Mathf.FloorToInt(healthCurve.keys[healthCurve.length - 1].time);
            private HealthComponentData lastHealthComponentData = new HealthComponentData { MaxHealth = -100, CurrentLevel = -1, CurrentHealth = -1 };


            public AnimationCurve GetDefaultExperienceCurve()
            {
                return DefaultHealthGrowth;
            }

            public int GetMaxHealthAtLevel(int level)
            {
                if (level < 1 || level > MaxLevel)
                {
                    IuvoDebug.DebugLogError($"Level {level} is out of bounds. Valid levels are between 1 and {MaxLevel}.");
                    return -1;
                }
                return Mathf.FloorToInt(healthCurve.Evaluate(level));
            }


            #region Serialization Methods

            public void Serialize(HealthComponentData HealthComponent)
            {

            }

            public HealthComponentData? DeserializeHealthComponent()
            {
                // no valid data to load
                return null;


                // valid data to load

            }


            #endregion

            /// <summary>
            /// Sets a new experience curve for level scaling.
            /// </summary>
            /// <param name="newExpCurve"></param>
            public void SetLevelScalingCurve(AnimationCurve newHealthCurve)
            {
                if (newHealthCurve == null)
                {
                    IuvoDebug.DebugLogError("New Expereience curve cannot be null. Implementing default scaling");
                    ImplementDefaultScalingAlgorithm();
                    return;
                }
                IuvoDebug.DebugLog("Setting level scaling algorithm...");
                healthCurve = newHealthCurve;
            }

            protected virtual void ImplementDefaultScalingAlgorithm()
            {
                IuvoDebug.DebugLog("Implementing default level scaling algorithm...");
            }


            /// <summary>
            /// Returns a dictionary mapping each level to the cumulative EXP required to reach that level.
            /// </summary>
            /// <returns></returns>
            public Dictionary<int, int> GetFullHealthTable()
            {
                Dictionary<int, int> healthTable = new Dictionary<int, int>();
                for (int level = 1; level <= MaxLevel; level++)
                {
                    healthTable[level] = GetMaxHealthAtLevel(level);
                }
                return healthTable;
            }


            public virtual void OnEnable()
            {
            }
            public virtual void OnDisable()
            {
                IuvoDebug.DebugLog("HealthConfiguration disabled.");
            }

            public override void PrintInfo()
            {
                base.PrintInfo();
                IuvoDebug.DebugLog($"Health Configuration: BaseHealth = {BaseHealth}");
            }

            #region IConfigurable Interface
            public override void Configure()
            {
                IuvoDebug.DebugLog("Configuring Level...");
                OnConfigure();
            }
            public override void OnConfigure()
            {
                IuvoDebug.DebugLog("Level configuration completed.");
            }
            public override void Reconfigure()
            {
                IuvoDebug.DebugLog("Reconfiguring Level...");
                OnReconfigure();
            }
            public override void OnReconfigure()
            {
                IuvoDebug.DebugLog("Level reconfiguration completed.");
            }
            #endregion
        }
    }
}