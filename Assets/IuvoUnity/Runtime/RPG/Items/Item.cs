using IuvoUnity.DataStructs;
using IuvoUnity.BaseClasses.RPG;
using UnityEngine;
using IuvoUnity.Interfaces;
using IuvoUnity.Configurations;

namespace IuvoUnity
{
    namespace BaseClasses
    {

        namespace Items
        {
            [CreateAssetMenu(fileName = "Item", menuName = "IuvoUnity/Items/Item", order = 1)]
            public class Item : ScriptableObject, IConfigurable, IReconfigurable //, IInteractable
            {

                [SerializeField]
                private ItemConfiguration configuration;


                private bool configured = false;
                public bool Configured { get => configured; set => configured = value; }
                private bool reconfigured = false;
                public bool Reconfigured { get => reconfigured; set => reconfigured = value; }

                public void OnConfigure()
                {
                    Configured = true;
                }

                public void OnReconfigure()
                {
                    Reconfigured = true;
                }
            }

            public abstract class ItemComponent 
            {

            }

            public class ItemTypeComponent : ItemComponent
            {
                public enum ItemType { BASIC_ITEM, KEY_ITEM, MATERIAL_ITEM, EQUIPMENT_ITEM, CONSUMABLE_ITEM }
                public ItemType _type;
            }

            public class CurrencyComponent : ItemComponent
            {
                public CurrencyValueComponent _currencyValue;
                public ValueMultiplier _currencyMultiplier;
            }

            public class CurrencyValueComponent : ItemComponent
            {
                public int _value;
            }

            public class ValueMultiplier : ItemComponent
            {
                public float _multiplier = 1.0f; // Default multiplier is 1 (no change)
            }

            public class WeightValueComponent : ItemComponent
            {
                public int _weight;
            }


            public class DurabilityComponent : ItemComponent
            {
                public float _currentDurabilityPercent;
                public ClampedData<int> _minMax;
            }

            public class StatBonusComponent : ItemComponent
            {
                public int _bonus;
            }

            public class AccuracyComponent : ItemComponent
            {
                public int _accuracy;
            }

            public class RangeComponent : ItemComponent
            {
                public int _range;
            }

            public class EquipmentComponent : ItemComponent
            {
            }


            public class CriticalHitComponent : ItemComponent
            {
                public float _critChance; // e.g., 0.15f = 15%
                public float _critMultiplier; // e.g., 2.0x
            }

            public class KnockbackComponent : ItemComponent
            {
                public float _force;
            }


            public class ReloadComponent : ItemComponent
            {
                public int _maxAmmo;
                public int _currentAmmo;
            }

            public class ProjectileComponent : ItemComponent
            {
                public GameObject _projectilePrefab;
                public Rigidbody _projectileRigidbody;
                public DamageComponent _damageComponent;

                public float _lifetime;

                public AudioClip _launchSound;
                public ParticleSystem _launchEffect;

                public AudioClip _impactSound;
                public ParticleSystem _impactEffect;
            }

            public class KnockbackProjectileComponent : ItemComponent
            {
                public ProjectileComponent _projectileComponent;
                public KnockbackComponent _knockbackComponent;
            }


            public enum WeaponTier { COMMON, UNCOMMON, RARE, EPIC, LEGENDARY }
            public class WeaponTierComponent : ItemComponent
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