using UnityEngine;
using IuvoUnity.Configurations;
using IuvoUnity.Debug;
using UnityEngine.Events;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        [System.Serializable]
        public struct LevelComponentData
        {
            public int CurrentLevel;                // the current level of the entity
            public int LevelProgressExperience;          // the current experience points towards the next level
            public int LifetimeExperience;            // the total experience points accumulated on each successful level up. if permanentExperience is true, this increases with every experience gain, not just on level-up

            // TODO: change to a float perecentage (LevelProgressExp / NextLevelExpMin)
            public int ExperienceUntilNextLevel;    // the remaining experience points needed to reach the next level

            public int NextLevelExperienceMinimum;  // the minimum experience points required for the next level

            public int CurrentLevelUpPoints;        // the number of points available to spend on upgrades
            public int MaxLevelUpPoints;           // the maximum number of level-up points that can be accumulated

            public bool isMaxLevel;


            /// <summary>
            /// Validates the LevelComponentData to ensure it has sensible values.
            /// </summary>
            /// <returns></returns>
            public bool IsValid()
            {
                return CurrentLevel >= 1 && LevelProgressExperience >= 0;
            }
        }

        [RequireComponent(typeof(LevelConfiguration))]
        public class LevelComponent : MonoBehaviour
        {
            public int CurrentLevel { get; private set; }      // The current level of the entity
            public int LevelProgressExperience { get; private set; }       // help EXP towards next level
            public int LifetimeExperience { get; private set; }       // all EXP ever gained. permanent on level up, cannot decrease


            public int ExperienceUntilNextLevel { get; private set; }       // EXP needed to reach next level
            public int NextLevelExperienceMinimum { get; private set; }         // minimum EXP required for next level

            public int CurrentLevelUpPoints { get; private set; }       // points available to spend on upgrades
            private int MaxLevelUpPoints { get; set; }            // maximum points that can be accumulated


            private bool isMaxLevel = false;


            /// <summary>
            /// Checks if the current level is at or above the maximum level defined in the LevelConfiguration.
            /// </summary>
            /// <returns></returns>
            public bool IsMaxLevel()
            {
                if (TryGetLevelConfig(out LevelConfiguration config))
                {
                    isMaxLevel = (CurrentLevel >= config.MaxLevel);
                    return isMaxLevel;
                }
                isMaxLevel = false;
                return isMaxLevel;
            }

            [SerializeField] private bool autoLeveling = true; // true = automatic leveling
            public bool AutoLeveling
            {
                get => autoLeveling;
                set => autoLeveling = value;
            }

            [SerializeField] private bool permanentExperience = false; // if true, TotalExperience increases with every experience gain, not just on level-up. Cannot lose experience

            public FlexibleEvent<int> OnLevelUp = new FlexibleEvent<int>();
            public FlexibleEvent<int> OnLevelDown = new FlexibleEvent<int>();
            /// <summary> params = currentExperience, experienceGained, experienceLost, experienceToNextLvl </summary>
            public FlexibleDynamicEvent OnExperienceChanged = new FlexibleDynamicEvent(new UnityEvent<int, int, int, int>());

            [SerializeField] private LevelConfiguration levelConfig;

            #region Experience

            /// <summary>
            /// Returns the total experience to consider for leveling calculations.
            /// Accounts for permanentExperience mode.
            /// </summary>
            private int GetEffectiveExperience()
            {
                return permanentExperience ? LifetimeExperience : LevelProgressExperience + LifetimeExperience;
            }

            /// <summary>
            /// Experience only goes to CurrentExperience variable. TotalExperience only increases on level-up.
            /// </summary>
            /// <param name="amount"></param>
            public void AddExperience(int amount)
            {
                if (amount < 0)
                {
                    IuvoDebug.DebugLogWarning("Experience amount cannot be negative.");
                    return;
                }

                if (!TryGetLevelConfig(out LevelConfiguration config))
                {
                    IuvoDebug.DebugLogError("Failed to get LevelConfiguration. Cannot add experience.");
                    return;
                }

                if (permanentExperience)
                {
                    LifetimeExperience += amount;
                }
                else
                {
                    LevelProgressExperience += amount;
                }



                if (autoLeveling)
                {
                    CheckLevelUp(config);
                }
                int effectiveExp = GetEffectiveExperience();
                ExperienceUntilNextLevel = config.GetExperienceToNextLevel(CurrentLevel, effectiveExp);
                OnExperienceChanged.Invoke(LevelProgressExperience, amount, 0, ExperienceUntilNextLevel);
            }

            public void LoseExperience(int amount)
            {
                if (permanentExperience)
                {
                    IuvoDebug.DebugLogWarning("Cannot lose experience when permanentExperience is enabled.");
                    return;
                }

                if (amount < 0)
                {
                    IuvoDebug.DebugLogWarning("Experience amount cannot be negative.");
                    return;
                }
                LevelProgressExperience = Mathf.Max(0, LevelProgressExperience - amount);

                if (TryGetLevelConfig(out LevelConfiguration config))
                {
                    ExperienceUntilNextLevel = config.GetExperienceToNextLevel(CurrentLevel, LifetimeExperience);
                }

                OnExperienceChanged.Invoke(LevelProgressExperience, 0, amount, ExperienceUntilNextLevel);
            }
            private void CheckLevelUp(LevelConfiguration config)
            {
                if (config == null) return;
                bool leveledUp = false;
                int effectiveExp = GetEffectiveExperience();

                while (CurrentLevel < config.MaxLevel)
                {
                    int nextLevelExp = config.GetExpAtLevel(CurrentLevel + 1);

                    if (effectiveExp >= nextLevelExp)
                    {
                        leveledUp = true;
                        CurrentLevel++;

                        int leftover = effectiveExp - nextLevelExp;

                        if (!permanentExperience)
                        {
                            LifetimeExperience = nextLevelExp; // Update lifetime only on level-up
                            LevelProgressExperience = leftover;
                        }
                        else
                        {
                            LevelProgressExperience = 0; // reset current progress if permanent
                        }

                        AddLevelUpPoint(1);
                        OnLevelUp.Invoke(CurrentLevel);

                        // Update effectiveExp for next iteration
                        effectiveExp = GetEffectiveExperience();

                    }
                    else
                    {
                        break; // Not enough EXP for next level
                    }
                }

                // Update max level flag
                isMaxLevel = (CurrentLevel >= config.MaxLevel);

                // Calculate remaining EXP to next level
                if (!isMaxLevel)
                {
                    int nextLevelExp = config.GetExpAtLevel(CurrentLevel + 1);
                    ExperienceUntilNextLevel = nextLevelExp - effectiveExp;
                    NextLevelExperienceMinimum = nextLevelExp;
                }
                else
                {
                    ExperienceUntilNextLevel = 0;
                    NextLevelExperienceMinimum = config.GetExpAtLevel(config.MaxLevel); // Optional: cap at max level
                    LevelProgressExperience = 0; // optional: reset excess EXP
                }

                if (leveledUp)
                {
                    OnExperienceChanged.Invoke(LevelProgressExperience, 0, 0, ExperienceUntilNextLevel);
                }
            }

            /// <summary>
            /// Directly adds levels, bypassing experience checks. Use with caution.
            /// </summary>
            /// <param name="toAdd"></param>
            public void AddLevels(int toAdd)
            {
                if (toAdd < 0)
                {
                    IuvoDebug.DebugLogWarning("Level amount to add cannot be negative.");
                    return;
                }
                if (!TryGetLevelConfig(out LevelConfiguration config))
                {
                    IuvoDebug.DebugLogError("Failed to get LevelConfiguration. Cannot add levels.");
                    return;
                }
                int targetLevel = Mathf.Min(CurrentLevel + toAdd, config.MaxLevel);
                if (targetLevel == CurrentLevel)
                {
                    IuvoDebug.DebugLogWarning("Already at maximum level. Cannot add more levels.");
                    return;
                }
                for (int i = CurrentLevel; i < targetLevel; i++)
                {
                    CurrentLevel++;
                    AddLevelUpPoint(1);
                    OnLevelUp.Invoke(CurrentLevel);
                }
                // Update experience values based on new level
                int newLevelExp = config.GetExpAtLevel(CurrentLevel);

                LifetimeExperience = newLevelExp;
                LevelProgressExperience = 0;

                isMaxLevel = (CurrentLevel >= config.MaxLevel);
                ExperienceUntilNextLevel = isMaxLevel ? 0 : config.GetExperienceToNextLevel(CurrentLevel, GetEffectiveExperience());
                NextLevelExperienceMinimum = isMaxLevel ? newLevelExp : config.GetExpAtLevel(CurrentLevel + 1);
                OnExperienceChanged.Invoke(LevelProgressExperience, 0, 0, ExperienceUntilNextLevel);

            }

            /// <summary>
            /// Directly removes levels. Will not reduce below level 1. All experience for lost levels is
            /// retained, but can be lost again if experience is lost.
            /// </summary>
            /// <param name="toLose"></param>
            public void LoseLevels(int toLose)
            {
                if (toLose < 0)
                {
                    IuvoDebug.DebugLogWarning("Level amount to lose cannot be negative.");
                    return;
                }
                if (!TryGetLevelConfig(out LevelConfiguration config))
                {
                    IuvoDebug.DebugLogError("Failed to get LevelConfiguration. Cannot lose levels.");
                    return;
                }
                int targetLevel = Mathf.Max(CurrentLevel - toLose, 1);
                if (targetLevel == CurrentLevel)
                {
                    IuvoDebug.DebugLogWarning("Already at minimum level. Cannot lose more levels.");
                    return;
                }
                for (int i = CurrentLevel; i > targetLevel; i--)
                {
                    CurrentLevel--;
                    if (CurrentLevelUpPoints > 0)
                    {
                        CurrentLevelUpPoints--;
                    }
                    OnLevelDown.Invoke(CurrentLevel);
                }
                // Update experience values based on new level
                int newLevelExp = config.GetExpAtLevel(CurrentLevel);
                LifetimeExperience = newLevelExp;
                LevelProgressExperience = 0;
                isMaxLevel = (CurrentLevel >= config.MaxLevel);
                ExperienceUntilNextLevel = isMaxLevel ? 0 : config.GetExperienceToNextLevel(CurrentLevel, GetEffectiveExperience());
                NextLevelExperienceMinimum = isMaxLevel ? newLevelExp : config.GetExpAtLevel(CurrentLevel + 1);
                OnExperienceChanged.Invoke(LevelProgressExperience, 0, 0, ExperienceUntilNextLevel);

            }

            public void AddLevelUpPoint(int amount)
            {
                if (amount < 0)
                {
                    IuvoDebug.DebugLogWarning("Level-up point amount cannot be negative.");
                    return;
                }
                if (CurrentLevelUpPoints + amount > MaxLevelUpPoints)
                {
                    IuvoDebug.DebugLogWarning("Cannot exceed maximum level-up points.");
                }

                CurrentLevelUpPoints = Mathf.Min(CurrentLevelUpPoints + amount, MaxLevelUpPoints);
            }

            public void SpendLevelUpPoint()
            {
                if (CurrentLevelUpPoints <= 0)
                {
                    IuvoDebug.DebugLogWarning("No level-up points available to spend.");
                    return;
                }
                CurrentLevelUpPoints--;
            }


            public void ManualLevelUp()
            {
                if (TryGetLevelConfig(out LevelConfiguration config))
                {
                    CheckLevelUp(config);
                }
            }

            public int GetExperienceToNextLevel()
            {
                if (levelConfig != null)
                {
                    return levelConfig.GetExperienceToNextLevel(CurrentLevel, GetEffectiveExperience());
                }
                else
                {
                    IuvoDebug.DebugLogError("LevelConfiguration is not set. Cannot get experience to next level.");
                    return 0;
                }
            }

            #endregion

            public void SerializeSelf()
            {
                if (levelConfig == null)
                {
                    IuvoDebug.DebugLogError("LevelConfiguration is not set. Cannot serialize LevelComponent.");
                    return;
                }

                LevelComponentData data = new LevelComponentData
                {
                    CurrentLevel = CurrentLevel,
                    LevelProgressExperience = LevelProgressExperience,
                    LifetimeExperience = LifetimeExperience,

                    ExperienceUntilNextLevel = ExperienceUntilNextLevel,
                    NextLevelExperienceMinimum = NextLevelExperienceMinimum,

                    CurrentLevelUpPoints = CurrentLevelUpPoints,
                    MaxLevelUpPoints = MaxLevelUpPoints,

                    isMaxLevel = isMaxLevel
                };

                levelConfig.Serialize(data);
            }

            public virtual void OnDestroy()
            {
                try
                {
                    SerializeSelf();
                }
                catch (System.Exception ex)
                {
                    IuvoDebug.DebugLogError($"Failed to serialize LevelComponent on destroy: {ex.Message}");
                }
                finally
                {
                    OnLevelUp.RemoveAllFlexibleEventListeners();
                    OnExperienceChanged.RemoveAllFlexibleEventListeners();
                }
            }

            public virtual void Start()
            {
                if (!TryGetLevelConfig(out LevelConfiguration config))
                {
                    IuvoDebug.DebugLogError("LevelConfiguration missing at Start(). Using defaults.");
                    CurrentLevel = 1;
                    LevelProgressExperience = 0;
                    LifetimeExperience = 0;
                    ExperienceUntilNextLevel = 1;
                    MaxLevelUpPoints = 5;
                    return;
                }

                var data = config.DeserializeLevelComponent();
                if (data.HasValue && data.Value.IsValid())
                {
                    InitializeFromData(data.Value);
                }
            }

            public void InitializeFromData(LevelComponentData data)
            {
                CurrentLevel = data.CurrentLevel;
                LevelProgressExperience = data.LevelProgressExperience;
                LifetimeExperience = data.LifetimeExperience;
                ExperienceUntilNextLevel = data.ExperienceUntilNextLevel;
                NextLevelExperienceMinimum = data.NextLevelExperienceMinimum;
                CurrentLevelUpPoints = data.CurrentLevelUpPoints;
                MaxLevelUpPoints = data.MaxLevelUpPoints;
            }

            public bool TryGetLevelConfig(out LevelConfiguration config)
            {
                if (levelConfig)
                {
                    config = levelConfig;
                    return true;
                }

                IuvoDebug.DebugLogError("ERROR: LevelConfiguration is not set. Returning default runtime instance.");
                config = null;
                return false;
            }
        }
    }
}