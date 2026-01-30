using System;
using System.Collections.Generic;
using IuvoUnity.Configurations;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        namespace Items
        {
            namespace Weapons
            {
                public class Weapon<TSubType> : Item where TSubType : Enum
                {
                    public WeaponBase<TSubType> _base;
                    public void SetWeaponBase(WeaponBase<TSubType> weapon)
                    {
                        _base = weapon;
                    }
                }

                #region WeaponConfiguration
                public class WeaponConfiguration : ItemConfiguration 
                {
                    WeaponBase<Enum> weaponBase;

                    int ItemPrice;
                    float ItemWeight;

                }

                public class WeaponBase<TSubType> where TSubType : Enum
                {
                    public WeaponCategory<TSubType> _Category { get; }
                    public WeaponActivityType _Type { get; }

                    public HandType _HandType { get; set; }
                    public WeaponEffects _Effects { get; set; }

                    public List<WeaponPart<TSubType>> _WeaponParts { get; set; }

                    public WeaponBase(WeaponCategory<TSubType> category, WeaponActivityType type, HandType handType, WeaponEffects weaponEffects)
                    {
                        _Category = category;
                        _Type = type;
                        _HandType = handType;
                        _Effects = weaponEffects;
                    }

                }



                public class WeaponCategory<T_SubType> where T_SubType : Enum
                {
                    public WeaponMainType _mainType { get; }
                    public T_SubType _subType { get; }

                    public WeaponCategory(T_SubType subType)
                    {
                        _mainType = GetMainTypeForSubType(typeof(T_SubType));
                        _subType = IsValidSubType(_mainType, subType)
                            ? subType
                            : (T_SubType)GetDefaultSubType(_mainType);
                    }

                    private static readonly Dictionary<Type, WeaponMainType> subTypeToMainMap = new Dictionary<Type, WeaponMainType>()
                    {
                        { typeof(SwordType), WeaponMainType.SWORD },
                        { typeof(AxeType), WeaponMainType.AXE },
                        { typeof(HammerType), WeaponMainType.HAMMER },
                        { typeof(PoleArmType), WeaponMainType.POLEARM },
                        { typeof(RangedType), WeaponMainType.RANGED },
                        { typeof(ThrowableType), WeaponMainType.THROWABLE }
                    };

                    public static WeaponMainType GetMainTypeForSubType(Type enumType)
                    {
                        if (subTypeToMainMap.TryGetValue(enumType, out var mainType))
                        {
                            return mainType;
                        }
                        throw new ArgumentException($"Unknown weapon subtype enum: {enumType.Name}");
                    }

                    public static bool IsValidSubType<TSubType>(WeaponMainType mainType, TSubType subType) where TSubType : Enum
                    {
                        return GetMainTypeForSubType(typeof(TSubType)) == mainType;
                    }

                    public static Enum GetDefaultSubType(WeaponMainType mainType)
                    {
                        return mainType switch
                        {
                            WeaponMainType.SWORD => SwordType.SHORT_SWORD,
                            WeaponMainType.AXE => AxeType.HATCHET,
                            WeaponMainType.HAMMER => HammerType.CLUB,
                            WeaponMainType.POLEARM => PoleArmType.SPEAR,
                            WeaponMainType.RANGED => RangedType.ARROW,
                            WeaponMainType.THROWABLE => ThrowableType.ROCK,
                            WeaponMainType.HYBRID => HybridType.OTHER, // Optional
                            _ => throw new ArgumentOutOfRangeException(nameof(mainType), $"No default for {mainType}")
                        };
                    }

                    public override string ToString()
                    {
                        return $"{_mainType} : {_subType}";
                    }

                }

                #endregion

                #region BasicWeaponEnums
                public enum WeaponMainType
                {
                    SWORD,
                    AXE,
                    HAMMER,
                    POLEARM,
                    RANGED,
                    THROWABLE,
                    HYBRID
                }
                public enum WeaponActivityType
                {

                    PIERCING,            // arrows, spears, throwing knives
                    SLASHING,            // swords, daggers
                    RENDING,             // Tearing weapons (e.g., serrated)
                    CLEAVING,            // Wide area heavy blows (e.g., axes)
                    BLUNT,               // Maces, hammers, bullets, sling

                    RANGED,              // Bows, crossbows
                    THROWN,              // Knives, axes, spears

                    MAGIC,               // Spellcasting focus
                    HYBRID,              // Multi-type weapons
                    OTHER
                }
                public enum HandType
                {
                    ONE_HANDED,
                    DUAL_WIELDED,
                    TWO_HANDED,
                    NO_HANDS
                }
                public enum WeaponButt
                {
                    NONE,
                    POMMEL,            // Classic sword/knife pommel
                    SKULL_KRUSHER,     // Heavy, blunt end
                    SPIKE,             // Can be used for piercing back attacks

                    COUNTERWEIGHT,     // For balance (often decorative)
                    CAP,               // Flat or metal end cap
                    HOOKED,            // Hooked end for tripping or grappling
                    OTHER
                }

                public enum SwordType
                {
                    DAGGER,
                    THROWING_KNIFE,
                    SHORT_SWORD,
                    LONG_SWORD,
                    KATANA,
                    SCIMITAR,
                    RAPIER,
                    GREAT_SWORD
                }
                public enum AxeType
                {
                    HATCHET,
                    BATTLE_AXE,
                    GREAT_AXE,
                    THROWING_AXE
                }
                public enum HammerType
                {
                    CLUB,
                    MACE,
                    MORNINGSTAR,
                    MALLET,
                    HAMMER,
                    GREAT_HAMMER

                }
                public enum PoleArmType
                {
                    MAGIC_STAFF,
                    QUARTER_STAFF,
                    SPEAR,
                    HALBERD,
                    TRIDENT
                }
                public enum RangedType
                {
                    MAGIC,
                    BULLET,
                    ARROW
                }
                public enum ThrowableType
                {
                    ROCK,
                    KNIFE,
                    OTHER
                }
                public enum HybridType
                {
                    OTHER
                }
                [System.Flags]
                public enum WeaponEffects
                {
                    NONE = 0,

                    BLEED = 1 << 0,
                    SAVAGE_BLEED = 1 << 1,

                    FREEZE = 1 << 2,
                    VICIOUS_FROST = 1 << 3,

                    BURN = 1 << 4,
                    HELLS_TOUCH = 1 << 5,

                    SHOCKED = 1 << 6,
                    ELECTROCUTED = 1 << 7,

                    WET = 1 << 8,
                    DRENCHED = 1 << 9,

                    POISONED = 1 << 10,
                    CONTAGIOUS = 1 << 11,

                    TIRED = 1 << 12,
                    ASLEEP = 1 << 13,

                    INTIMIDATED = 1 << 14,
                    FRIGHTENED = 1 << 15,

                    LEECHING = 1 << 16
                }
                #endregion

            }
        }
    }
}