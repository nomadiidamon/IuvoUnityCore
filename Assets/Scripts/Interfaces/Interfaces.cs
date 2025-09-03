using IuvoUnity._BaseClasses;
using System;

namespace IuvoUnity
{
    namespace Interfaces
    {
        #region Serialization

        public interface ISerializer
        {
            string Serialize<T>(T data);
            T Deserialize<T>(string data);
        }


        #endregion
        #region Lifecycle: Creation, Destruction, Initialization, Start

        public interface ICreate : IuvoInterfaceBase
        {
            public abstract void Create();
            public abstract void OnCreate();
        }

        public interface IDestroy : IuvoInterfaceBase
        {
            public abstract void Destroy();
            public abstract void OnDestroy();
        }

        public interface ICreatable : IuvoInterfaceBase, ICreate, IDestroy { }

        public interface IAwake : IuvoInterfaceBase
        {
            public abstract void Awake();
            public abstract void OnAwake();
        }

        public interface IInitialize : IuvoInterfaceBase
        {
            public abstract void Initialize();
        }

        public interface IStart : IuvoInterfaceBase
        {
            public abstract void Start();
            public abstract void OnStart();
        }

        #endregion

        #region Update Cycle

        public interface IUpdate : IuvoInterfaceBase
        {
            public abstract void OnUpdate();
            public abstract void Update();

        }

        public interface IFixedUpdate : IuvoInterfaceBase
        {
            public abstract void OnFixedUpdate();
            public abstract void FixedUpdate();
        }

        public interface ILateUpdate : IuvoInterfaceBase
        {
            public abstract void OnLateUpdate();
            public abstract void LateUpdate();
        }

        #endregion

        #region Add/Remove & Configuration

        public interface IAdd : IuvoInterfaceBase
        {
            public abstract void Add();
            public abstract void OnAdd();
        }

        public interface IRemove : IuvoInterfaceBase
        {
            public abstract void Remove();
            public abstract void OnRemove();
        }

        public interface IAddable : IuvoInterfaceBase, IAdd, IRemove { }

        public interface IConfigure : IuvoInterfaceBase
        {
            public abstract void Configure();
            public abstract void OnConfigure();
        }

        public interface IReconfigure : IuvoInterfaceBase
        {
            public abstract void Reconfigure();
            public abstract void OnReconfigure();
        }

        public interface IConfigurable : IuvoInterfaceBase, IConfigure, IReconfigure 
        {
            public bool Configured { get; set; }
            public bool Reconfigured { get; set; }
        }

        #endregion

        #region Change & Notification

        public interface IChange : IuvoInterfaceBase
        {
            public abstract void Change();
            public abstract void OnChange();
        }

        public interface INotifyOfChange : IuvoInterfaceBase
        {
            public abstract void OnNotifyOfChange<T>() where T : IChange;
        }

        public interface INotifiable : IuvoInterfaceBase, INotifyOfChange, IChange
        {
            public abstract void NotifyOfChange<T>();
        }

        #endregion

        #region State Control: Activate, Enable, Toggle, Pause

        public interface IActivate : IuvoInterfaceBase
        {
            public abstract void Activate();
            public abstract void OnActivate();
        }

        public interface IDeactivate : IuvoInterfaceBase
        {
            public abstract void Deactivate();
            public abstract void OnDeactivate();
        }

        public interface IActivatable : IuvoInterfaceBase, IActivate, IDeactivate { }

        public interface IEnable : IuvoInterfaceBase
        {
            public abstract void Enable();
            public abstract void OnEnable();
        }

        public interface IDisable : IuvoInterfaceBase
        {
            public abstract void Disable();
            public abstract void OnDisable();
        }

        public interface IEnableable : IuvoInterfaceBase, IEnable, IDisable { }

        public interface ITogglable : IuvoInterfaceBase, IEnableable, IActivatable
        {
            public bool IsEnabled { get; set; }
            public bool IsActive { get; set; }
        }

        public interface IPausable : IuvoInterfaceBase, ITogglable
        {
            public bool IsPaused { get; set; }
            public abstract void OnPause();
            public abstract void OnResume();
        }

        #endregion

        #region Reset & Registration

        public interface IReset : IuvoInterfaceBase
        {
            public abstract void OnReset();
        }

        public interface IResetData : IuvoInterfaceBase
        {
            public Type ResetData { get; set; }
        }

        public interface IResetable : IuvoInterfaceBase, IReset, IResetData { }

        public interface IRegister : IuvoInterfaceBase
        {
            public abstract void Register();
        }

        public interface IUnregister : IuvoInterfaceBase
        {
            public abstract void Unregister();
        }

        public interface IRegisterable : IuvoInterfaceBase, IRegister, IUnregister { }

        #endregion

        #region Input

        public interface IInputAction : IuvoInterfaceBase
        {
            void HandleInput();
        }

        #endregion
    }
}
