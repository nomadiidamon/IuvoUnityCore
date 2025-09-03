using UnityEngine;
using IuvoUnity.Debug;
using IuvoUnity.BaseClasses;
using System.Collections.Generic;

namespace IuvoUnity
{
    namespace Configurations
    {

        [CreateAssetMenu(fileName = "New Level Configuration", menuName = "IuvoUnity/RPG/Level Configuration")]
        public class LevelConfiguration : BaseConfig
        {
            [SerializeField]
            private AnimationCurve DefaultLevelingCurve = new AnimationCurve(
                new Keyframe(1, 100),
                new Keyframe(25, 25000),
                new Keyframe(50, 150000),
                new Keyframe(75, 2500000),
                new Keyframe(100, 50000000)
            );

            public AnimationCurve GetDefaultExperienceCurve()
            {
                return DefaultLevelingCurve;
            }

            public string savedItemName = "LevelComponent"; // name for whatever any derived Level component is tracking

            // Cumulative EXP required to reach each level (index 0 = Level 1)
            // Time is the level number, value is the cumulative EXP required to reach that level
            [SerializeField] private AnimationCurve expCurve;


            [SerializeField] public int MaxLevel => Mathf.FloorToInt(expCurve.keys[expCurve.length - 1].time);

            private LevelComponentData lastLevelComponentData = new LevelComponentData { CurrentLevel = -1 };

            public virtual void OnEnable()
            {
                configName = typeof(LevelConfiguration).ToString();

                if (expCurve == null)
                {
                    ImplementDefaultScalingAlgorithm();
                }
//                MaxLevel = Mathf.FloorToInt(expCurve.keys[expCurve.length - 1].time);

                if (!Configured)
                {
                    Configure();
                    Configured = true;
                }
            }

            public virtual void OnDisable()
            {
                IuvoDebug.DebugLog("LevelConfiguration disabled.");
            }

            #region Serialization Methods

            public void Serialize(LevelComponentData levelComponent)
            {
                string json = JsonUtility.ToJson(levelComponent);
                string filePath = GetConfigSerializePath("LevelConfiguration", savedItemName, "BaseConfig");
                if (filePath == "")
                {
                    IuvoDebug.DebugLogError("Failed to get file path for serialization.");
                    return;
                }
                System.IO.File.WriteAllText(filePath, json);
                IuvoDebug.DebugLog($"Serialized LevelComponent to {filePath}");
            }

            public LevelComponentData? DeserializeLevelComponent()
            {
                string filePath = GetConfigSerializePath("LevelConfiguration", savedItemName, "BaseConfig");
                if (!System.IO.File.Exists(filePath))
                {
                    IuvoDebug.DebugLogWarning($"No save file found at {filePath}");
                    return null;
                }

                string json = System.IO.File.ReadAllText(filePath);
                LevelComponentData data = JsonUtility.FromJson<LevelComponentData>(json);
                lastLevelComponentData = data;
                return data;
            }

            #endregion

            #region Leveling Methods

            public int GetExpAtLevel(int level)
            {
                if (level < 1 || level > MaxLevel)
                {
                    IuvoDebug.DebugLogWarning("Level out of range. Returning 0.");
                    return 0;
                }
                float exp = expCurve.Evaluate(level);
                return Mathf.RoundToInt(exp);
            }

            /// <summary>
            /// Returns EXP still required to reach the next level from the current state.
            /// Uses cumulative EXP values stored in the exp curve.
            /// </summary>
            public int GetExperienceToNextLevel(int currLevel, int cumulativeExp)
            {
                if (currLevel < 1 || currLevel >= MaxLevel)
                {
                    return 0;
                }

                int nextLevelExp = GetExpAtLevel(currLevel + 1);
                return Mathf.Max(0, nextLevelExp - cumulativeExp);
            }

            /// <summary>
            /// Sets a new experience curve for level scaling.
            /// </summary>
            /// <param name="newExpCurve"></param>
            public void SetLevelScalingCurve(AnimationCurve newExpCurve)
            {
                if (newExpCurve == null)
                {
                    IuvoDebug.DebugLogError("New Expereience curve cannot be null. Implementing default scaling");
                    ImplementDefaultScalingAlgorithm();
                    return;
                }
                IuvoDebug.DebugLog("Setting level scaling algorithm...");
                expCurve = newExpCurve;
            }

            protected virtual void ImplementDefaultScalingAlgorithm()
            {
                IuvoDebug.DebugLog("Implementing default exp curve...");

                expCurve = new AnimationCurve(GetDefaultExperienceCurve().keys);
            }


            /// <summary>
            /// Returns a dictionary mapping each level to the cumulative EXP required to reach that level.
            /// </summary>
            /// <returns></returns>
            public Dictionary<int, int> GetFullLevelTable()
            {
                Dictionary<int, int> levelTable = new Dictionary<int, int>();
                for (int level = 1; level <= MaxLevel; level++)
                {
                    levelTable[level] = GetExpAtLevel(level);
                }
                return levelTable;
            }


            #endregion

            /// <summary>
            /// Prints detailed information about the level configuration, including the EXP curve and last known level component data.
            /// </summary>
            public override void PrintInfo()
            {
                base.PrintInfo();

                IuvoDebug.DebugLog("Level EXP Curve:");
                Dictionary<int, int> scaling = GetFullLevelTable();
                foreach (var kvp in scaling)
                {
                    IuvoDebug.DebugLog($"  Level {kvp.Key}: {kvp.Value} EXP");
                }

                if (lastLevelComponentData.IsValid())
                {
                    IuvoDebug.DebugLog("Last Level Component:");
                    IuvoDebug.DebugLog($"  Is Max Level: {lastLevelComponentData.CurrentLevel >= MaxLevel}");
                    IuvoDebug.DebugLog($"  Level: {lastLevelComponentData.CurrentLevel}");
                    IuvoDebug.DebugLog($"  Experience: {lastLevelComponentData.LevelProgressExperience}");
                    IuvoDebug.DebugLog($"  Lifetime Experience: {lastLevelComponentData.LifetimeExperience}");

                    IuvoDebug.DebugLog($"  Experience to Next Level: {lastLevelComponentData.ExperienceUntilNextLevel}");
                    IuvoDebug.DebugLog($"  Next Level EXP Requirement: {lastLevelComponentData.NextLevelExperienceMinimum}");

                    IuvoDebug.DebugLog($"  Current LevelUp Points: {lastLevelComponentData.CurrentLevelUpPoints}");
                    IuvoDebug.DebugLog($"  Max LevelUp Points: {lastLevelComponentData.MaxLevelUpPoints}");
                }
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