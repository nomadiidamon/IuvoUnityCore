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
            StateMachine.UpdateMode updateMode { get; set; }
            bool HasUpdateMode() => updateMode != StateMachine.UpdateMode.None;
            virtual void OnUpdate() { }
            void HandleUpdate(StateMachine.UpdateMode updateMode)
            {
                if ((this.updateMode & updateMode) != 0)
                {
                    switch (updateMode)
                    {
                        case StateMachine.UpdateMode.Update:
                            Update();
                            break;
                        case StateMachine.UpdateMode.FixedUpdate:
                            FixedUpdate();
                            break;
                        case StateMachine.UpdateMode.LateUpdate:
                            LateUpdate();
                            break;
                    }
                }
                OnUpdate();
            }
        }
    }
}