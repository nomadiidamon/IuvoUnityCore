using IuvoUnity.DataStructs;
using UnityEngine;

namespace IuvoUnity
{
    namespace BaseClasses
    {

        namespace Items
        {
            namespace Weapons
            {
                namespace MeleeWeapons
                {
                    namespace Hammers
                    {

                        public enum HammerHandle
                        {
                            SHAFT,
                            SCALED_HANDLE,
                            INTEGRATED_HANDLE
                        }
                        public class HammerHandlePart : HandlePart<HammerHandle>
                        {
                            public HammerHandlePart(HammerHandle handleType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(handleType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum HammerPommel
                        {
                            FLANGED,
                            ROUNDED,
                            ORNATE
                        }
                        public class HammerPommelPart : PommelPart<HammerPommel>
                        {
                            public HammerPommelPart(HammerPommel pommelType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(pommelType, partName, partShape, relativeTransformToWeapon) { }
                        }


                        // Guard
                        public enum HammerGuard
                        {
                            BASIC,
                            REINFORCED,
                            DECORATIVE
                        }
                        public class HammerGuardPart : GuardPart<HammerGuard>
                        {
                            public HammerGuardPart(HammerGuard guardType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(guardType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        // Head
                        public enum HammerHead
                        {
                            STANDARD,
                            WEDGED,
                            HEAVY,
                            SPIKED
                        }
                        public class HammerHeadPart : HeadPart<HammerHead>
                        {
                            public HammerHeadPart(HammerHead headType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(headType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        // CounterWeight
                        public enum HammerCounterWeight
                        {
                            NONE,
                            SMALL,
                            LARGE
                        }
                        public class HammerCounterWeightPart : CounterWeightPart<HammerCounterWeight>
                        {
                            public HammerCounterWeightPart(HammerCounterWeight counterWeightType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(counterWeightType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        // Connector
                        public enum HammerConnector
                        {
                            BASIC,
                            STRONG,
                            FLEXIBLE
                        }
                        public class HammerConnectorPart : ConnectorPart<HammerConnector>
                        {
                            public HammerConnectorPart(HammerConnector connectorType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(connectorType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        // Spacer
                        public enum HammerSpacer
                        {
                            THIN,
                            MEDIUM,
                            THICK
                        }
                        public class HammerSpacerPart : SpacerPart<HammerSpacer>
                        {
                            public HammerSpacerPart(HammerSpacer spacerType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(spacerType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        // Sheathe
                        public enum HammerSheathe
                        {
                            NONE,
                            LEATHER,
                            METAL
                        }
                        public class HammerSheathePart : SheathePart<HammerSheathe>
                        {
                            public HammerSheathePart(HammerSheathe sheatheType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(sheatheType, partName, partShape, relativeTransformToWeapon) { }
                        }
                    }

                }
            }
        }

    }
}