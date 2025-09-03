using IuvoUnity.DataStructs;
using UnityEngine;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        namespace RPG
        {
            namespace Items
            {

                public class ItemComponent : RPGComponentBase
                {
                    public NameData Name;
                    public DataDescription Description;
                    public CurrencyComponent Currency;
                    public ItemTypeComponent ItemType;
                }

                public class ItemTypeComponent : RPGComponentBase
                {
                    public enum ItemType { BASIC_ITEM, KEY_ITEM, MATERIAL_ITEM, EQUIPMENT_ITEM, CONSUMABLE_ITEM }
                    public ItemType _type;
                }

                public class CurrencyComponent : RPGComponentBase
                {
                    public DataDescription _bonusFromSpeech;
                    public CurrencyValueComponent _currencyValue;
                    public CurrencyMultiplierComponent _currencyMultiplier;
                }

                public class CurrencyValueComponent : RPGComponentBase
                {
                    public int _value;
                }

                public class ValueMultiplier : RPGComponentBase
                {
                    public float _multiplier = 1.0f; // Default multiplier is 1 (no change)
                }

                public class CurrencyMultiplierComponent : ValueMultiplier
                {

                }

                public class WeightValueComponent : RPGComponentBase
                {
                    public int _weight;
                }

                public class StackableComponent : RPGComponentBase
                {
                    public ClampedValue<int> _stack;
                }

                public class DensityValueComponent : RPGComponentBase
                {
                    public float _density;
                }

                public class DurabilityComponent : RPGComponentBase
                {
                    public float _currentDurability;
                    public Range<int> _minMax;
                }

                public class StatBonusComponent : RPGComponentBase
                {
                    public int _bonus;
                }

                public class StatBonusMultiplierComponent : ValueMultiplier
                {

                }

                public class AccuracyComponent : RPGComponentBase
                {
                    public int _accuracy;
                }

                public class RangeComponent : RPGComponentBase
                {
                    public int _range;
                }

                public class EquipmentComponent : ItemComponent
                {
                }

                public class AttackSpeedComponent : RPGComponentBase
                {
                    public float _attacksPerSecond;
                }

                public class CriticalHitComponent : RPGComponentBase
                {
                    public float _critChance; // e.g., 0.15f = 15%
                    public float _critMultiplier; // e.g., 2.0x
                }

                public class KnockbackComponent : RPGComponentBase
                {
                    public float _force;
                }

                public class DurabilityDrainRateComponent : RPGComponentBase
                {
                    public float _drainRate; // e.g., durability lost per swing or hit
                }

                public class ReloadComponent : RPGComponentBase
                {
                    public int _maxAmmo;
                    public int _currentAmmo;
                    public float _reloadTime;
                }

                public class ProjectileComponent : RPGComponentBase
                {
                    public GameObject _projectilePrefab;
                    public float _speed;
                    public float _lifetime;
                }

                public class WeaponSkillComponent : RPGComponentBase
                {
                    public string _skillName;
                    public float _cooldown;
                }

                public class NoiseLevelComponent : RPGComponentBase
                {
                    public float _decibels;
                }

                public enum WeaponTier { COMMON, UNCOMMON, RARE, EPIC, LEGENDARY }
                public class WeaponTierComponent : RPGComponentBase
                {
                    public WeaponTier _tier;
                }


                public class ArmorSlotComponent : EquipmentComponent
                {
                    public enum ArmorSlot { HEAD, CHEST, ARMS, LEGS }
                    public ArmorSlot _armorSlot;
                }

                public class AccessorySlotComponent : EquipmentComponent
                {
                    public enum AccessorySlot
                    {
                        VEIL, CROWN, CAPE, NECKLACE, LEFT_EARRING, RIGHT_EARRING,
                        COLLAR, LEFT_WRIST, RIGHT_WRIST, LEFT_RING, RIGHT_RING
                    }

                    public AccessorySlot _accessorySlot;
                }

            }
        }
    }
}