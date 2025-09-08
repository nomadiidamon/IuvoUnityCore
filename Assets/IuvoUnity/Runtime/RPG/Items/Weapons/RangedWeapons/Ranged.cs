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
                namespace Weapons
                {
                    namespace RangedWeapons
                    {

                        public enum RangedGrip
                        {
                            NATURAL_GRIP,
                            WRAPPED_GRIP,
                            PLAIN_GRIP,
                            STUDDED_GRIP,
                            INDENTED_GRIP,
                        }
                        public class RangedGripPart : HandlePart<RangedGrip>
                        {
                            public RangedGripPart(RangedGrip gripType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(gripType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum RangedPommel
                        {
                            WIREFRAME_GUN_STOCK,
                            CAST_IRON_STOCK,
                            STEEL_STOCK
                        }
                        public class RangedPommelPart : PommelPart<RangedPommel>
                        {
                            public RangedPommelPart(RangedPommel pommelType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(pommelType, partName, partShape, relativeTransformToWeapon) { }
                        }


                        public enum ConnectorType
                        {
                            SIMPLE_CONNECTOR,
                            LOCKING_CONNECTOR,
                            FLEXIBLE_CONNECTOR
                        }
                        public class ConnectorPart : ConnectorPart<ConnectorType>
                        {
                            public ConnectorPart(ConnectorType connectorType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(connectorType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum SpacerType
                        {
                            THIN_SPACER,
                            THICK_SPACER,
                            ELASTIC_SPACER
                        }
                        public class SpacerPart : SpacerPart<SpacerType>
                        {
                            public SpacerPart(SpacerType spacerType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(spacerType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum SheatheType
                        {
                            LEATHER_SHEATHE,
                            METAL_SHEATHE,
                            WOODEN_SHEATHE
                        }
                        public class SheathePart : SheathePart<SheatheType>
                        {
                            public SheathePart(SheatheType sheatheType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(sheatheType, partName, partShape, relativeTransformToWeapon) { }
                        }
                    }

                }
            }
        }
    }
}
