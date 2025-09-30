using UnityEngine;
using IuvoUnity.Singletons;
using IuvoUnity.BaseClasses;
using System.Collections;

namespace IuvoUnity
{
    namespace Interfaces
    {
        public interface IActivator <T> : IuvoInterfaceBase where T :IActivatable
        {
            public abstract void Activate(T activatable);
        }
        public interface IActivatable : IuvoInterfaceBase
        { 
            public abstract void OnActivate();        
        }

        public interface IDeactivator <T> : IuvoInterfaceBase where T : IDeactivatable
        {
            public abstract void Deactivate(T deactivatable);
        }
        public interface IDeactivatable : IuvoInterfaceBase
        {
            public abstract void OnDeactivate();
        }



        public interface IEnabler<T> : IuvoInterfaceBase where T : IEnableable
        {
            public abstract void Enable(T enableable);
        }
        public interface IEnableable : IuvoInterfaceBase
        {
            public abstract void OnEnable();
        }

        public interface IDisabler<T> : IuvoInterfaceBase where T : IDisableable
        {
            public abstract void Disable(T disable);
        }
        public interface IDisableable : IuvoInterfaceBase
        {
            public abstract void OnDisable();
        }


        public interface IToggler<T> : IuvoInterfaceBase where T : ITogglable
        {
            public abstract void Toggle(T togglable);
        }
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

            public void PauseFor(float seconds)
            {
                if (!IsPaused)
                {
                    IsPaused = true;
                    OnPause();
                    CoroutineManager.RunCoroutine(ResumeAfterDelay(seconds));
                }
            }

            public IEnumerator ResumeAfterDelay(float seconds)
            {
                yield return CoroutineManager.GetWaitForSeconds(seconds);
                IsPaused = false;
                OnResume();
            }
        }
    }
}