using UnityEngine;
using IuvoUnity.Configurations;
using IuvoUnity.BaseClasses.Items;

namespace IuvoUnity
{
    namespace Configurations
    {
        [CreateAssetMenu(fileName = "ItemConfiguration", menuName = "IuvoUnity/Configs/ItemConfiguration", order = 2)]
        public class ItemConfiguration : BaseConfig<Item>
        {
            [SerializeField] ItemTypeComponent itemTypeComponent;
            [SerializeField] CurrencyComponent currencyComponent;
            [SerializeField] WeightValueComponent WeightComponent;
            [SerializeField] DurabilityComponent durabilityComponent;


            [SerializeField] public MeshRenderer meshRenderer;


            private void OnEnable()
            {
                if (string.IsNullOrEmpty(configName))
                {
                    configName = "{name}";
                }
            }

            public override void Configure(Item configurable)
            {
                base.Configure(configurable);
            }

            public override void Reconfigure(Item reconfigurable)
            {
                base.Reconfigure(reconfigurable);
            }

            public override void PrintInfo()
            {
                base.PrintInfo();
            }
        }

    }
}