using IuvoUnity.DataStructs;
using System;
using UnityEngine;

namespace IuvoUnity
{
    namespace BaseClasses
    {
        namespace RPG
        {
            namespace Items
            {
                namespace Weapons
                {

                    #region Weapon Parts
                    public interface IWeaponPart : IDataStructBase
                    {
                        public NameData partName { get; set; }
                        public Mesh partShape { get; set; }
                        public TransformData relativeTransformToWeapon { get; set; }
                    }
                    public abstract class WeaponPart<TPartType> : IWeaponPart where TPartType : Enum
                    {

                        public TPartType PartType { get; private set; }

                        private NameData _partName;
                        private Mesh _partShape;
                        private TransformData _relativeTransformToWeapon;

                        public virtual NameData partName
                        {
                            get => _partName;
                            set => _partName = value;
                        }

                        public virtual Mesh partShape
                        {
                            get => _partShape;
                            set => _partShape = value;
                        }

                        public virtual TransformData relativeTransformToWeapon
                        {
                            get => _relativeTransformToWeapon;
                            set => _relativeTransformToWeapon = value;
                        }

                        protected WeaponPart(TPartType partType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                        {
                            PartType = partType;
                            _partName = partName;
                            _partShape = partShape;
                            _relativeTransformToWeapon = relativeTransformToWeapon;
                        }
                    }
                    public enum WeaponPartTypes
                    {
                        POMMEL = 1,
                        HANDLE,
                        GUARD,
                        BLADE,
                        HEAD,
                        COUNTER_WEIGHT,
                        CONNECTOR,
                        SPACER,
                        SHEATHE
                    }

                    public abstract class PommelPart<TPommelType> : WeaponPart<TPommelType> where TPommelType : Enum
                    {
                        public TPommelType PommelType { get; private set; }

                        // Backing fields for IWeaponPart
                        private NameData _partName;
                        private Mesh _partShape;
                        private TransformData _relativeTransformToWeapon;

                        // Implement IWeaponPart members
                        public override NameData partName
                        {
                            get => _partName;
                            set => _partName = value;
                        }

                        public override Mesh partShape
                        {
                            get => _partShape;
                            set => _partShape = value;
                        }

                        public override TransformData relativeTransformToWeapon
                        {
                            get => _relativeTransformToWeapon;
                            set => _relativeTransformToWeapon = value;
                        }

                        public PommelPart(TPommelType pommelType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                            : base(pommelType, partName, partShape, relativeTransformToWeapon)
                        {
                            PommelType = pommelType;
                            _partName = partName;
                            _partShape = partShape;
                            _relativeTransformToWeapon = relativeTransformToWeapon;
                        }
                    }
                    public abstract class HandlePart<THandleType> : WeaponPart<THandleType> where THandleType : Enum
                    {
                        public THandleType HandleType { get; private set; }

                        // Backing fields for interface properties
                        private NameData _partName;
                        private Mesh _partShape;
                        private TransformData _relativeTransformToWeapon;

                        // Implement IWeaponPart members
                        public override NameData partName
                        {
                            get => _partName;
                            set => _partName = value;
                        }

                        public override Mesh partShape
                        {
                            get => _partShape;
                            set => _partShape = value;
                        }

                        public override TransformData relativeTransformToWeapon
                        {
                            get => _relativeTransformToWeapon;
                            set => _relativeTransformToWeapon = value;
                        }

                        public HandlePart(THandleType handleType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                            : base(handleType, partName, partShape, relativeTransformToWeapon)

                        {
                            HandleType = handleType;
                            _partName = partName;
                            _partShape = partShape;
                            _relativeTransformToWeapon = relativeTransformToWeapon;
                        }
                    }
                    public abstract class GuardPart<TGuardType> : WeaponPart<TGuardType> where TGuardType : Enum
                    {
                        public TGuardType GuardType { get; private set; }

                        private NameData _partName;
                        private Mesh _partShape;
                        private TransformData _relativeTransformToWeapon;

                        public override NameData partName
                        {
                            get => _partName;
                            set => _partName = value;
                        }

                        public override Mesh partShape
                        {
                            get => _partShape;
                            set => _partShape = value;
                        }

                        public override TransformData relativeTransformToWeapon
                        {
                            get => _relativeTransformToWeapon;
                            set => _relativeTransformToWeapon = value;
                        }

                        public GuardPart(TGuardType guardType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                             : base(guardType, partName, partShape, relativeTransformToWeapon)
                        {
                            GuardType = guardType;
                            _partName = partName;
                            _partShape = partShape;
                            _relativeTransformToWeapon = relativeTransformToWeapon;
                        }
                    }
                    public abstract class BladePart<TBladeType> : WeaponPart<TBladeType> where TBladeType : Enum
                    {
                        public TBladeType BladeType { get; private set; }

                        private NameData _partName;
                        private Mesh _partShape;
                        private TransformData _relativeTransformToWeapon;

                        public override NameData partName
                        {
                            get => _partName;
                            set => _partName = value;
                        }

                        public override Mesh partShape
                        {
                            get => _partShape;
                            set => _partShape = value;
                        }

                        public override TransformData relativeTransformToWeapon
                        {
                            get => _relativeTransformToWeapon;
                            set => _relativeTransformToWeapon = value;
                        }

                        public BladePart(TBladeType bladeType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                             : base(bladeType, partName, partShape, relativeTransformToWeapon)
                        {
                            BladeType = bladeType;
                            _partName = partName;
                            _partShape = partShape;
                            _relativeTransformToWeapon = relativeTransformToWeapon;
                        }
                    }
                    public abstract class HeadPart<THeadType> : WeaponPart<THeadType> where THeadType : Enum
                    {
                        public THeadType HeadType { get; private set; }

                        private NameData _partName;
                        private Mesh _partShape;
                        private TransformData _relativeTransformToWeapon;

                        public override NameData partName
                        {
                            get => _partName;
                            set => _partName = value;
                        }

                        public override Mesh partShape
                        {
                            get => _partShape;
                            set => _partShape = value;
                        }

                        public override TransformData relativeTransformToWeapon
                        {
                            get => _relativeTransformToWeapon;
                            set => _relativeTransformToWeapon = value;
                        }

                        public HeadPart(THeadType headType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                              : base(headType, partName, partShape, relativeTransformToWeapon)
                        {
                            HeadType = headType;
                            _partName = partName;
                            _partShape = partShape;
                            _relativeTransformToWeapon = relativeTransformToWeapon;
                        }
                    }
                    public abstract class CounterWeightPart<TCounterWeightType> : WeaponPart<TCounterWeightType> where TCounterWeightType : Enum
                    {
                        public TCounterWeightType CounterWeightType { get; private set; }

                        private NameData _partName;
                        private Mesh _partShape;
                        private TransformData _relativeTransformToWeapon;

                        public override NameData partName
                        {
                            get => _partName;
                            set => _partName = value;
                        }

                        public override Mesh partShape
                        {
                            get => _partShape;
                            set => _partShape = value;
                        }

                        public override TransformData relativeTransformToWeapon
                        {
                            get => _relativeTransformToWeapon;
                            set => _relativeTransformToWeapon = value;
                        }

                        public CounterWeightPart(TCounterWeightType counterWeightType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(counterWeightType, partName, partShape, relativeTransformToWeapon)
                        {
                            CounterWeightType = counterWeightType;
                            _partName = partName;
                            _partShape = partShape;
                            _relativeTransformToWeapon = relativeTransformToWeapon;
                        }
                    }
                    public abstract class ConnectorPart<TConnectorType> : WeaponPart<TConnectorType> where TConnectorType : Enum
                    {
                        public TConnectorType ConnectorType { get; private set; }

                        private NameData _partName;
                        private Mesh _partShape;
                        private TransformData _relativeTransformToWeapon;

                        public override NameData partName
                        {
                            get => _partName;
                            set => _partName = value;
                        }

                        public override Mesh partShape
                        {
                            get => _partShape;
                            set => _partShape = value;
                        }

                        public override TransformData relativeTransformToWeapon
                        {
                            get => _relativeTransformToWeapon;
                            set => _relativeTransformToWeapon = value;
                        }

                        public ConnectorPart(TConnectorType connectorType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(connectorType, partName, partShape, relativeTransformToWeapon)
                        {
                            ConnectorType = connectorType;
                            _partName = partName;
                            _partShape = partShape;
                            _relativeTransformToWeapon = relativeTransformToWeapon;
                        }
                    }
                    public abstract class SpacerPart<TSpacerType> : WeaponPart<TSpacerType> where TSpacerType : Enum
                    {
                        public TSpacerType SpacerType { get; private set; }

                        private NameData _partName;
                        private Mesh _partShape;
                        private TransformData _relativeTransformToWeapon;

                        public override NameData partName
                        {
                            get => _partName;
                            set => _partName = value;
                        }

                        public override Mesh partShape
                        {
                            get => _partShape;
                            set => _partShape = value;
                        }

                        public override TransformData relativeTransformToWeapon
                        {
                            get => _relativeTransformToWeapon;
                            set => _relativeTransformToWeapon = value;
                        }

                        public SpacerPart(TSpacerType spacerType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                              : base(spacerType, partName, partShape, relativeTransformToWeapon)
                        {
                            SpacerType = spacerType;
                            _partName = partName;
                            _partShape = partShape;
                            _relativeTransformToWeapon = relativeTransformToWeapon;
                        }
                    }
                    public abstract class SheathePart<TSheatheType> : WeaponPart<TSheatheType> where TSheatheType : Enum
                    {
                        public TSheatheType SheatheType { get; private set; }

                        private NameData _partName;
                        private Mesh _partShape;
                        private TransformData _relativeTransformToWeapon;

                        public override NameData partName
                        {
                            get => _partName;
                            set => _partName = value;
                        }

                        public override Mesh partShape
                        {
                            get => _partShape;
                            set => _partShape = value;
                        }

                        public override TransformData relativeTransformToWeapon
                        {
                            get => _relativeTransformToWeapon;
                            set => _relativeTransformToWeapon = value;
                        }

                        public SheathePart(TSheatheType sheatheType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                        : base(sheatheType, partName, partShape, relativeTransformToWeapon)
                        {
                            SheatheType = sheatheType;
                            _partName = partName;
                            _partShape = partShape;
                            _relativeTransformToWeapon = relativeTransformToWeapon;
                        }
                    }

                    #endregion

                }
            }
        }
    }
}