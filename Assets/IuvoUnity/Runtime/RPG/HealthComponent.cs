using UnityEngine;
using IuvoUnity.Debug;
using IuvoUnity.Events;
using IuvoUnity.Configurations;
using IuvoUnity.Interfaces;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        public interface ITakeDamage
        {
            void TakeDamage(DamageComponent damageComponent);
        }

        public interface IHeal
        {
            void Heal(float amount);
        }

        public interface IHealInRadius
        {
            void HealInRadius(float amount);
            void HealInRadiusWithFalloff(float amount);
        }

        public interface IShield
        {
            float GetShieldValue();
            void AbsorbDamage(DamageComponent damageComponent);
        }

        public struct HealthComponentData
        {
            public float CurrentHealth;
            public float MaxHealth;
            public int CurrentLevel;

            public bool IsValid()
            {
                return MaxHealth >= 0 && CurrentLevel >= 1;
            }
        }

        public class HealthComponent : MonoBehaviour, ITakeDamage, IHeal, IConfigurable, IReconfigurable
        {
            // may turn to a specific HealthLevel SO
            [SerializeField] private HealthConfiguration HealthConfig;

            public float CurrentHealth { get; private set; } = 25;
            public float MaxHealth { get; private set; } = 25;
            public int CurrentLevel = 1;

            private Transform parent;

            public float HealthPercentage()
            {
                if (isDead)
                {
                    return 0f; // If dead, health percentage is 0
                }
                return (float)CurrentHealth / MaxHealth;
            }
            private bool isDead = false;
            public bool IsDead() => isDead;
            public bool TryGetHealthConfig(out HealthConfiguration config)
            {
                if (HealthConfig)
                {
                    config = HealthConfig;
                    return true;
                }
                config = null;
                return false;
            }

            public virtual void UpdateMaxHealth()
            {
                if (HealthConfig != null)
                {
                    int newMaxHealth = HealthConfig.GetMaxHealthAtLevel(CurrentLevel);
                    if (newMaxHealth < 0)
                    {
                        IuvoDebug.DebugLogError("Failed to update MaxHealth due to invalid level or configuration.");
                        return;
                    }
                    MaxHealth = newMaxHealth;
                    if (CurrentHealth > MaxHealth)
                    {
                        CurrentHealth = MaxHealth; // Ensure current health does not exceed max health
                    }
                }
            }

            // passes the amount of health gained
            public FlexibleEvent<float> OnGainHealth = new FlexibleEvent<float>();
            // passes the amount of health lost
            public FlexibleEvent<float> OnLoseHealth = new FlexibleEvent<float>();
            // passes current health and max health
            public FlexibleEvent<float, float> OnHealthChanged = new FlexibleEvent<float, float>();
            // passes the component that did the damage
            public FlexibleEvent<DamageComponent> OnHealthDepleted = new FlexibleEvent<DamageComponent>();

            public void Start()
            {
                if (parent == null)
                {
                    parent = transform.parent;
                }
                if (parent == null)
                {
                    IuvoDebug.DebugLogWarning("HealthComponent has no parent transform.");
                }

                if (HealthConfig != null)
                {
                    //CurrentLevel = HealthConfig.LoadLevel();

                    UpdateMaxHealth();
                }
            }

            public void OnDestroy()
            {
                if (HealthConfig != null)
                {
                    HealthConfig.OnDisable();

                    IuvoDebug.DebugLog("HealthConfiguration disabled.");
                }
            }

            #region IHeal Implementation
            public virtual void Heal(float amount)
            {
                if (amount < 0.0f)
                {
                    IuvoDebug.DebugLogWarning("Heal amount cannot be negative.");
                    return;
                }
                CurrentHealth += amount;
                if (CurrentHealth > MaxHealth)
                {
                    CurrentHealth = MaxHealth;
                }
                OnGainHealth.Invoke(amount);
                OnHealthChanged.Invoke(CurrentHealth, MaxHealth);
            }
            public void HealInRadius(float amount)
            {
                // noop
            }
            public void HealInRadiusWithFalloff(float amount)
            {
                //noop
            }
            public void HealRandomInRadius(float amount)
            {
                //noop
            }
            public void HealRandomInRadiusWithFalloff(float amount)
            {
                //noop
            }
            #endregion

            #region ITakeDamage Implementation
            public virtual void TakeDamage(DamageComponent damageComponent)
            {
                if (damageComponent == null || damageComponent.DamageValue < 0.0f)
                {
                    IuvoDebug.DebugLogWarning("Invalid DamageComponent.");
                    return;
                }

                if (this is IShield shieldComp)
                {
                    shieldComp.AbsorbDamage(damageComponent);
                    if (damageComponent.DamageValue <= 0.0f)
                    {
                        return; // All damage absorbed by shield
                    }
                }
                else if (parent != null)
                {
                    IShield[] shields = parent.GetComponentsInChildren<IShield>();
                    foreach (var shield in shields)
                    {
                        shield.AbsorbDamage(damageComponent);
                        if (damageComponent.DamageValue <= 0.0f)
                        {
                            return; // All damage absorbed by shield
                        }
                    }
                }

                CurrentHealth -= damageComponent.DamageValue;
                OnLoseHealth.Invoke(damageComponent.DamageValue);

                if (CurrentHealth <= 0.0f)
                {
                    CurrentHealth = 0;
                    if (!isDead)
                    {
                        isDead = true;
                        OnHealthDepleted.Invoke(damageComponent);
                    }
                }
                OnHealthChanged.Invoke(CurrentHealth, MaxHealth);
            }

            #endregion

            #region IConfigurable Implementation

            public bool Configured { get; set; }
            public virtual void OnConfigure()
            {

            }

            #endregion

            #region IReconfigurable Implementation

            public bool Reconfigured { get; set; }
            public virtual void OnReconfigure()
            {
            }

            #endregion
        }
    }
}