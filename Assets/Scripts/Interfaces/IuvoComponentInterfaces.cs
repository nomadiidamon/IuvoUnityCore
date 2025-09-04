using IuvoUnity.BaseClasses;
using IuvoUnity._BaseClasses._ECS;
using System;

namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface ICreateEntity : IuvoInterfaceBase
        {
            public abstract void OnCreate(IuvoEntity entity);
        }
        public interface IDestroyEntity : IuvoInterfaceBase
        {
            public abstract void OnDestroy(IuvoEntity entity);
        }
        public interface ICreatableEntity : IuvoInterfaceBase, ICreateEntity, IDestroyEntity
        {
            public abstract void Create(IuvoEntity entity);
            public abstract void Destroy(IuvoEntity entity);
        }

        public interface IAwakeEntity : IuvoInterfaceBase
        {
            public abstract void OnAwake(IuvoEntity entity);
        }
        public interface IInitializeEntity : IuvoInterfaceBase
        {
            public abstract void Initialize(IuvoEntity entity);
        }
        public interface IStartEntity : IuvoInterfaceBase
        {
            public abstract void OnStart(IuvoEntity entity);
        }

        public interface IUpdatableEntity : IuvoInterfaceBase
        {
            public abstract void Update(IuvoEntity entity);
        }
        public interface IUpdateEntity : IUpdatableEntity
        {
            public abstract void OnUpdate(IuvoEntity entity);
        }
        public interface IFixedUpdateEntity : IUpdatableEntity
        {
            public abstract void OnFixedUpdate(IuvoEntity entity);
        }
        public interface ILateUpdateEntity : IUpdatableEntity
        {
            public abstract void OnLateUpdate(IuvoEntity entity);
        }

        public interface IAddEntity : IuvoInterfaceBase
        {
            public abstract void OnAdd(IuvoEntity entity);
        }
        public interface IRemoveEntity : IuvoInterfaceBase
        {
            public abstract void OnRemove(IuvoEntity entity);
        }
        public interface IAddableEntity : IuvoInterfaceBase, IAddEntity, IRemoveEntity
        {

        }

        public interface IConfigureEntity : IuvoInterfaceBase
        {
            public abstract void OnConfigure(IuvoEntity entity);
        }
        public interface IReconfigureEntity : IuvoInterfaceBase
        {
            public abstract void OnReconfigure(IuvoEntity entity);
        }
        public interface IConfigurableEntity : IuvoInterfaceBase, IConfigureEntity, IReconfigureEntity
        {

        }

        public interface IChangeEntity : IuvoInterfaceBase
        {
            public abstract void OnChange(IChange change);
        }
        public interface INotifyOfChangeEntity : IuvoInterfaceBase
        {
            public abstract void OnNotifyOfChange<T>(IuvoEntity entity) where T : IChange;
        }
        public interface INotifiableEntity : IuvoInterfaceBase, INotifyOfChangeEntity, IChangeEntity
        {
            public abstract void NotifyOfChange<T>(IuvoEntity entity);
        }

        public interface IActivateEntity : IuvoInterfaceBase
        {
            public abstract void OnActivate(IuvoEntity entity);
        }
        public interface IDeactivateEntity : IuvoInterfaceBase
        {
            public abstract void OnDeactivate(IuvoEntity entity);
        }
        public interface IActivatableEntity : IuvoInterfaceBase, IActivateEntity, IDeactivateEntity
        {

        }

        public interface IEnableEntity : IuvoInterfaceBase
        {
            public abstract void OnEnable(IuvoEntity entity);
        }
        public interface IDisableEntity : IuvoInterfaceBase
        {
            public abstract void OnDisable(IuvoEntity entity);
        }
        public interface IEnableableEntity : IuvoInterfaceBase, IEnableEntity, IDisableEntity
        {

        }

        public interface ITogglableEntity : IuvoInterfaceBase, IEnableableEntity, IActivatableEntity
        {
            public bool IsEnabled { get; set; }
            public bool IsActive { get; set; }
        }
        public interface IPausableEntity : IuvoInterfaceBase, ITogglableEntity
        {
            public bool IsPaused { get; set; }
            public abstract void OnPause(IuvoEntity entity);
            public abstract void OnResume(IuvoEntity entity);
        }

        public interface IResetEntity : IuvoInterfaceBase
        {
            public abstract void OnReset(IuvoEntity entity);
        }
        public interface IResetDataEntity : IuvoInterfaceBase
        {
            public Type ResetData { get; set; }
        }
        public interface IResettableEntity : IuvoInterfaceBase, IResetEntity, IResetDataEntity
        {
        }

        public interface IRegisterEntity : IuvoInterfaceBase
        {
            public abstract void Register(IuvoEntity entity);
        }
        public interface IUnregisterEntity : IuvoInterfaceBase
        {
            public abstract void Unregister(IuvoEntity entity);
        }
        public interface IRegisterableEntity : IuvoInterfaceBase, IRegisterEntity, IUnregisterEntity
        {
        }
    }


}