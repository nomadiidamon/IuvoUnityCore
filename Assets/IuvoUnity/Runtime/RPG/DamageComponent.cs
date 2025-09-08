using IuvoUnity.Debug;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        public enum DamageType
        {
            PSYCHE_CONFUSION, PSYCHE_PAIN, PSYCHE_SLEEP, PSYCHE_PARALYSIS, PSYCHE_UNCONSCIOUS,

            PHYSICAL_PIERCING, PHYSICAL_SLASHING, PHYSICAL_BLUNT,

            MAGIC_FIRE, MAGIC_LAVA, MAGIC_METAL, MAGIC_EARTH, MAGIC_WATER,
            MAGIC_ICE, MAGIC_AIR, MAGIC_ELECTRICITY, MAGIC_GRAVITY, NULL
        }
        public class DamageTypeComponent : IDataStructBase
        {
            public DamageType DamageCategory;
            public DamageTypeComponent()
            {
                DamageCategory = DamageType.NULL;
            }
        }

        public interface IDealDamage
        {
            void DealDamage(HealthComponent healthComp);
        }

        public interface IDealDamageInRadius
        {
            void DealDamageInRadius(HealthComponent healthComp);
            void DealDamageInRadiusWithFalloff(HealthComponent healthComp);
        }

        public class DamageComponent : IDealDamage
        {
            public DamageTypeComponent DamageType;
            public float DamageValue;
            // maybe add a instigator for the Health Component to know who dealt the damage

            public DamageComponent(float damageAmount, DamageType damageCategory)
            {
                if (damageAmount < 0.0f)
                {
                    IuvoDebug.DebugLogWarning("Damage amount cannot be negative. Setting to 1.");
                    DamageValue = 1.0f;
                    DamageType = new DamageTypeComponent { DamageCategory = damageCategory };
                    return;
                }
                DamageValue = damageAmount;
                DamageType = new DamageTypeComponent { DamageCategory = damageCategory };
            }

            public void DealDamage(HealthComponent healthComp)
            {
                if (healthComp == null)
                {
                    IuvoDebug.DebugLogWarning("HealthComponent is null. Cannot deal damage.");
                    return;
                }
                if (DamageValue < 0.0f)
                {
                    IuvoDebug.DebugLogWarning("Damage amount cannot be negative.");
                    return;
                }
                healthComp.TakeDamage(this);
            }

            // public void DealDamageInRadius(){}


            // public void DealDamageInRadiusWithFallof(){}


            // public void DealDamageOverTime(){}
        }


    }
}