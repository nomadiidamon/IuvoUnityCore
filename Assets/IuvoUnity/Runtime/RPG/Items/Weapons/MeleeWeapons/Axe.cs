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
                    namespace Axes
                    {
                        public enum AxeHandle
                        {
                            SHAFT,
                            SCALED_HANDLE,
                            INTEGRATED_HANDLE
                        }
                        public class AxeHandlePart : HandlePart<AxeHandle>
                        {
                            public AxeHandlePart(AxeHandle handleType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(handleType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum AxePommel
                        {
                            SPIKED,
                            FLAT,
                            HOOKED
                        }
                        public class AxePommelPart : PommelPart<AxePommel>
                        {
                            public AxePommelPart(AxePommel pommelType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(pommelType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum AxeGuard
                        {
                            CROSSGUARD,
                            BASKET,
                            NONE
                        }
                        public class AxeGuardPart : GuardPart<AxeGuard>
                        {
                            public AxeGuardPart(AxeGuard guardType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(guardType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum AxeBlade
                        {
                            SINGLE_EDGE,
                            DOUBLE_EDGE,
                            BEVELED,
                            CURVED
                        }
                        public class AxeBladePart : BladePart<AxeBlade>
                        {
                            public AxeBladePart(AxeBlade bladeType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(bladeType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum AxeHead
                        {
                            WEDGE,
                            HAMMER,
                            PICK
                        }
                        public class AxeHeadPart : HeadPart<AxeHead>
                        {
                            public AxeHeadPart(AxeHead headType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(headType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum AxeCounterWeight
                        {
                            ROUND,
                            FLAT,
                            SPIKED
                        }
                        public class AxeCounterWeightPart : CounterWeightPart<AxeCounterWeight>
                        {
                            public AxeCounterWeightPart(AxeCounterWeight counterWeightType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(counterWeightType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum AxeConnector
                        {
                            RIVET,
                            WELD,
                            PIN
                        }
                        public class AxeConnectorPart : ConnectorPart<AxeConnector>
                        {
                            public AxeConnectorPart(AxeConnector connectorType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(connectorType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum AxeSpacer
                        {
                            THIN,
                            THICK,
                            DECORATIVE
                        }
                        public class AxeSpacerPart : SpacerPart<AxeSpacer>
                        {
                            public AxeSpacerPart(AxeSpacer spacerType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(spacerType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum AxeSheathe
                        {
                            LEATHER,
                            METAL,
                            WOOD
                        }
                        public class AxeSheathePart : SheathePart<AxeSheathe>
                        {
                            public AxeSheathePart(AxeSheathe sheatheType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(sheatheType, partName, partShape, relativeTransformToWeapon) { }
                        }
                    }

                }
            }
        }

    }
}