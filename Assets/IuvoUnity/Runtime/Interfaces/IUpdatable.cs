using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface IUpdate : IuvoInterfaceBase
        {
            public void Update();
        }
        public interface IFixedUpdate : IuvoInterfaceBase
        {
            public abstract void FixedUpdate();
        }
        public interface ILateUpdate : IuvoInterfaceBase
        {
            public abstract void LateUpdate();
        }

        public interface IUpdatable : IUpdate, IFixedUpdate, ILateUpdate
        {
            StateMachines.CSM.ConditionalStateMachineUpdateMode updateMode { get; set; }
            bool HasUpdateMode() => updateMode != StateMachines.CSM.ConditionalStateMachineUpdateMode.None;
            virtual void OnUpdate() { }
            void HandleUpdate(StateMachines.CSM.ConditionalStateMachineUpdateMode updateMode)
            {
                if ((this.updateMode & updateMode) != 0)
                {
                    switch (updateMode)
                    {
                        case StateMachines.CSM.ConditionalStateMachineUpdateMode.Update:
                            Update();
                            break;
                        case StateMachines.CSM.ConditionalStateMachineUpdateMode.FixedUpdate:
                            FixedUpdate();
                            break;
                        case StateMachines.CSM.ConditionalStateMachineUpdateMode.LateUpdate:
                            LateUpdate();
                            break;
                    }
                }
                OnUpdate();
            }
        }
    }
}