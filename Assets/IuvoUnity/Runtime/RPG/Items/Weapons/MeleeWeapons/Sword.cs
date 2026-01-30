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
                    namespace Swords
                    {

                        public enum SwordHandle
                        {
                            HILT,
                            WRAPPED_HILT,
                            CLOSED_GRIP,
                            SCALED_HANDLE,
                            INTEGRATED_HANDLE
                        }
                        public class SwordHandlePart : HandlePart<SwordHandle>
                        {
                            public SwordHandlePart(SwordHandle handleType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(handleType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum SwordPommel
                        {
                            ROUNDED, DIAMOND, SKULL_KRUSHER
                        }
                        public class SwordPommelPart : PommelPart<SwordPommel>
                        {
                            public SwordPommelPart(SwordPommel pommelType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(pommelType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum SwordGuard
                        {
                            CROSS,          // Classic crossguard
                            BASKET,         // Basket style guard (woven metal cage)
                            WINGED,         // Extended wings, often curved
                            RING,           // Circular ring guards on sides
                            SHELL,          // Shell-shaped guards
                            HALF_BASKET,    // Partial cage guard
                            SIMPLE_BAR      // Minimalist bar guard
                        }
                        public class SwordGuardPart : GuardPart<SwordGuard>
                        {
                            public SwordGuardPart(SwordGuard guardType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(guardType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum SwordBlade
                        {
                            STRAIGHT,           // Traditional straight blade
                            CURVED,             // Curved blade like a scimitar or sabre
                            TAPERED,            // Widens near the guard and tapers to point
                            DOUBLE_EDGED,       // Sharp edges on both sides
                            SINGLE_EDGED,       // One sharp edge only
                            FOLDED,             // Pattern-welded or folded steel
                            SERRATED,           // Sharp bittin into edge for shredding
                            BROAD,              // Wide blade for slashing
                            RAPIER,             // Thin, thrusting blade
                            TRI_SIDE_RAPIER,    // Rapier great at deep wounds 
                            KATANA              // Japanese style curved blade
                        }
                        public class SwordBladePart : BladePart<SwordBlade>
                        {
                            public SwordBladePart(SwordBlade bladeType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(bladeType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum SwordCounterWeight
                        {
                            NONE,           // No counterweight
                            ROUNDED,        // Rounded weight
                            FLANGED,        // Weight with flanges or fins
                            SPIKED,         // Spiky counterweight
                            BLOCK,          // Block shaped weight
                            CUSTOM          // Custom or ornamental shapes
                        }
                        public class SwordCounterWeightPart : CounterWeightPart<SwordCounterWeight>
                        {
                            public SwordCounterWeightPart(SwordCounterWeight weightType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(weightType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum SwordConnector
                        {
                            SIMPLE,         // Simple ring or collar
                            REINFORCED,     // Reinforced collar or ferrule
                            DECORATED,      // Decorated with engravings or patterns
                            SPIKED,         // Connector with spikes or projections
                            NONE            // No separate connector piece
                        }
                        public class SwordConnectorPart : ConnectorPart<SwordConnector>
                        {
                            public SwordConnectorPart(SwordConnector connectorType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(connectorType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum SwordSpacer
                        {
                            METAL_WASHER,       // Metal washer spacer
                            LEATHER_RING,       // Leather ring spacer
                            WOODEN_RING,        // Wooden ring spacer
                            DECORATIVE_RING,    // Ornamental ring spacer
                            NONE                // No spacer
                        }
                        public class SwordSpacerPart : SpacerPart<SwordSpacer>
                        {
                            public SwordSpacerPart(SwordSpacer spacerType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(spacerType, partName, partShape, relativeTransformToWeapon) { }
                        }

                        public enum SwordSheathe
                        {
                            LEATHER,            // Leather sheath
                            WOODEN,             // Wooden sheath
                            METAL,              // Metal sheath
                            DECORATED_LEATHER,  // Decorated leather sheath
                            ORNAMENTED_METAL    // Ornamental metal sheath
                        }
                        public class SwordSheathePart : SheathePart<SwordSheathe>
                        {
                            public SwordSheathePart(SwordSheathe sheatheType, NameData partName, Mesh partShape, TransformData relativeTransformToWeapon)
                                : base(sheatheType, partName, partShape, relativeTransformToWeapon) { }
                        }
                    }

                }
            }
        }

    }
}