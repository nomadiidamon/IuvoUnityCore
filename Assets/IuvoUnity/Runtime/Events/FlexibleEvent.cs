using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class FlexibleEvent
{
    // reference to c# delegates for cleanup
    //private List<Action> internalActions = new List<Action>();
    // C# delegate-based event
    private event Action internalEvent;

    // reference to Unity delegats for cleanup

    // UnityEvent exposed to the inspector
    public UnityEvent UnityEvent => unityEvent; 

    [SerializeField] private UnityEvent unityEvent = new UnityEvent();

    // Invoke both UnityEvent and C# event
    public void Invoke()
    {
        unityEvent?.Invoke();
        internalEvent?.Invoke();
    }

    // Add C# listener
    public void AddListener(Action listener)
    {
        internalEvent += listener;
        //internalActions.Add(listener);
    }

    // Remove C# listener
    public void RemoveListener(Action listener)
    {
        internalEvent -= listener;
        //internalActions.Remove(listener);
    }

    // Add UnityAction listener (if needed in code)
    public void AddUnityListener(UnityAction listener)
    {
        unityEvent.AddListener(listener);
    }

    // Remove UnityAction listener
    public void RemoveUnityListener(UnityAction listener)
    {
        unityEvent.RemoveListener(listener);
    }

    // should be called in OnDestroy of the owner to prevent memory leaks
    public void RemoveAllFlexibleEventListeners()
    {
        //foreach (var evt in internalActions)
        //{
        //    RemoveListener(evt);
        //}

        internalEvent = null; // Clear the C# event to prevent memory leaks

        unityEvent.RemoveAllListeners();
    }
}


[Serializable]
public class FlexibleEvent<T>
{
    private event Action<T> internalEvent;
    //private readonly List<Action<T>> internalActions = new List<Action<T>>();

    public UnityEvent<T> UnityEvent => unityEvent;
    [SerializeField] private UnityEvent<T> unityEvent = new UnityEvent<T>();

    public void Invoke(T arg)
    {
        unityEvent?.Invoke(arg);
        internalEvent?.Invoke(arg);
    }

    public void AddListener(Action<T> listener)
    {
        internalEvent += listener;
        //internalActions.Add(listener);
    }

    public void RemoveListener(Action<T> listener)
    {
        internalEvent -= listener;
        //internalActions.Remove(listener);
    }

    public void AddUnityListener(UnityAction<T> listener)
    {
        unityEvent.AddListener(listener);
    }

    public void RemoveUnityListener(UnityAction<T> listener)
    {
        unityEvent.RemoveListener(listener);
    }

    public void RemoveAllFlexibleEventListeners()
    {
        //foreach (var evt in internalActions)
        //    RemoveListener(evt);
        internalEvent = null; // Clear the C# event to prevent memory leaks

        unityEvent.RemoveAllListeners();
    }
}



[Serializable]
public class FlexibleEvent<T1, T2>
{
    private event Action<T1, T2> internalEvent;
    //private readonly List<Action<T1, T2>> internalActions = new List<Action<T1, T2>>();

    public UnityEvent<T1,T2> UnityEvent => unityEvent;
    [SerializeField] private UnityEvent<T1, T2> unityEvent = new UnityEvent<T1, T2>();

    public void Invoke(T1 arg1, T2 arg2)
    {
        unityEvent?.Invoke(arg1, arg2);
        internalEvent?.Invoke(arg1, arg2);
    }
    public void AddListener(Action<T1, T2> listener)
    {
        internalEvent += listener;
        //internalActions.Add(listener);
    }
    public void RemoveListener(Action<T1, T2> listener)
    {
        internalEvent -= listener;
        //internalActions.Remove(listener);
    }
    public void AddUnityListener(UnityAction<T1, T2> listener)
    {
        unityEvent.AddListener(listener);
    }
    public void RemoveUnityListener(UnityAction<T1, T2> listener)
    {
        unityEvent.RemoveListener(listener);
    }

    public void RemoveAllFlexibleEventListeners()
    {
        //foreach (var evt in internalActions)
        //    RemoveListener(evt);

        internalEvent = null; // Clear the C# event to prevent memory leaks

        unityEvent.RemoveAllListeners();
    }
}

[Serializable]
public class FlexibleDynamicEvent
{
    private event Action<object[]> internalEvent;

    [SerializeField] private UnityEventBase unityEvent;

    public UnityEventBase UnityEvent => unityEvent;

    public FlexibleDynamicEvent()
    {
        unityEvent = new UnityEvent(); // default no-arg
    }

    // Allows custom UnityEvent types (UnityEvent, UnityEvent<T>, UnityEvent<T1,T2>, etc.)
    public FlexibleDynamicEvent(UnityEventBase unityEventInstance)
    {
        unityEvent = unityEventInstance;
    }

    // Invoke both UnityEvent and C# event
    public void Invoke(params object[] args)
    {
        // Invoke UnityEvent via reflection
        if (unityEvent != null)
        {
            var method = unityEvent.GetType().GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
            method?.Invoke(unityEvent, args);
        }

        // Invoke internal C# event
        internalEvent?.Invoke(args);
    }

    public void AddListener(Action<object[]> listener)
    {
        internalEvent += listener;
    }

    public void RemoveListener(Action<object[]> listener)
    {
        internalEvent -= listener;
    }

    public void RemoveAllFlexibleEventListeners()
    {
        internalEvent = null;
        unityEvent?.RemoveAllListeners();
    }
}
