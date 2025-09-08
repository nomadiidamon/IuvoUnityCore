using System.Collections.Generic;
using UnityEngine;
using IuvoUnity.Debug;
using IuvoUnity.Editor;

namespace IuvoUnity
{
    namespace StateMachine
    {
        [System.Serializable]
        public class GenericStateMachine : MonoBehaviour
        {
            [Expandable]
            public GenericState currentState;
            [Expandable]
            public GenericState defaultState;
            [Expandable]
            public GenericState previousState;

            [Header("States")]
            public List<GenericState> States = new List<GenericState>();

            [Tooltip("Queue that tracks the last (historySize) states entered")]
            public Queue<GenericState> History = new Queue<GenericState>(); // is only runtime
            [SerializeField] private int historySize = 10; // default history size

            // called whenever a state is entered
            private void AddToHistory(GenericState state)
            {
                if (state != null)
                {
                    History.Enqueue(state);
                    if (History.Count > historySize) History.Dequeue();

                }
            }

            // toggles the state machine to force enter the previous or default state if 
            private bool forceEnterPreviousState = true;
            private bool forceEnterDefaultState = false;

            public bool ForcePrevState() => forceEnterPreviousState;
            public void SetForcePrevState(bool value)
            {
                if (forceEnterDefaultState)
                {
                    IuvoDebug.DebugLogError("Cannot force previous state when default state is forced. Please disable forceEnterDefaultState first.");
                    return;
                }
                forceEnterPreviousState = value;
            }

            public bool ForceDefaultState() => forceEnterDefaultState;
            public void SetForceDefaultState(bool value)
            {
                if (forceEnterPreviousState)
                {
                    IuvoDebug.DebugLogError("Cannot force default state when previous state is forced. Please disable forceEnterPrevState first.");
                    return;
                }
                forceEnterDefaultState = value;
            }

            public bool IsInState(GenericState state) => currentState == state;
            public bool WasInState(GenericState state) => previousState == state;
            public bool IsInState(string stateName) => currentState != null && currentState.stateName == stateName;

            public void Awake()
            {
                if (defaultState == null && States.Count > 0)
                {
                    defaultState = States[0];
                    IuvoDebug.DebugLogWarning("Default state not set. Assigning first state in States list as default.");
                }
                else if (States.Count == 0)
                {
                    IuvoDebug.DebugLogError("No states defined in the state machine. Please add states to the States list.");
                }
            }

            public void Start()
            {
                currentState = null;
                previousState = null;
                ChangeState(defaultState, true);
            }

            private void HandleContinue(UpdateMode mode)
            {
                if (currentState != null && currentState.updateMode == mode)
                {
                    if (currentState.CanContinue(this))
                        currentState.OnContinue?.Invoke(this);
                    else
                        IuvoDebug.DebugLogWarning(
                            $"Cannot continue state {currentState.stateName} due to unmet conditions.");
                }
            }

            public virtual void Update() => HandleContinue(UpdateMode.Update);

            public virtual void FixedUpdate() => HandleContinue(UpdateMode.FixedUpdate);

            public virtual void LateUpdate() => HandleContinue(UpdateMode.LateUpdate);

            public virtual bool TryChangeState(GenericState newState)
            {
                if (newState != null)
                {
                    bool canEnterNew = newState.CanEnter(this);
                    return ChangeState(newState, canEnterNew);
                }
                else
                {
                    IuvoDebug.DebugLogWarning("State Transition Denied Due To Unmet Conditions.");
                    return false;
                }
            }


            /// <summary>
            /// Attempts to change to a new state, handling all conditions and events.
            /// Interrupt->Exit->Enter sequence is followed if all conditions are met.
            /// </summary>
            /// <param name="newState"></param>
            public virtual bool ChangeState(GenericState newState, bool canEnterNew)
            {
                if (newState == null || newState == currentState)
                {
                    IuvoDebug.DebugLogWarning("State Transition Denied: New state is null or same as current state.");
                    return false;
                }

                if (currentState == null)
                {
                    if (canEnterNew)
                    {
                        currentState = newState;
                        currentState.OnEnter.Invoke(this);
                        AddToHistory(currentState);
                        return true;
                    }
                    else
                    {
                        IuvoDebug.DebugLogWarning($"Cannot enter state {newState.stateName} due to unmet enter conditions.");
                        return false;
                    }
                }

                if (!currentState.CanInterrupt(this))
                {
                    IuvoDebug.DebugLogWarning($"Cannot interrupt {currentState.stateName} with {newState.stateName} due to unmet interrupt conditions.");
                    return false;
                }

                if (!canEnterNew)
                {
                    IuvoDebug.DebugLogWarning($"Cannot enter {newState.stateName} due to unmet enter conditions.");
                    return false;
                }

                if (!currentState.CanExit(this))
                {
                    IuvoDebug.DebugLogWarning($"Cannot exit from {currentState.stateName} to {newState.stateName} due to unmet exit conditions.");
                    return false;
                }

                currentState.OnInterrupt.Invoke(this);
                IuvoDebug.DebugLog($"{currentState.stateName} is being interrupted by {newState.stateName}.");
                currentState.OnExit.Invoke(this);
                IuvoDebug.DebugLog($"{currentState.stateName} exited successfully.");
                IuvoDebug.DebugLog($"Transitioning from {currentState.stateName} to {newState.stateName}.");
                previousState = currentState;
                currentState = newState;

                AddToHistory(currentState);
                if (canEnterNew)
                {
                    currentState.OnEnter.Invoke(this);
                }
                else
                {
                    HandleFallbackLogic();
                    return false; // Fallback logic handled, return false to indicate transition failure
                }
                return true;
            }

            void HandleFallbackLogic()
            {
                IuvoDebug.DebugLogWarning($"Cannot re-enter previous state {previousState.stateName} due to unmet enter conditions.");

                if (forceEnterPreviousState && previousState != null)
                {
                    IuvoDebug.DebugLogWarning($"Forcing entry into previous state {previousState.stateName}.");
                    currentState = previousState;
                    AddToHistory(currentState);
                    currentState.OnEnter.Invoke(this);
                }
                else if (forceEnterDefaultState && defaultState != null)
                {
                    IuvoDebug.DebugLogWarning($"Forcing entry into default state {defaultState.stateName}.");
                    currentState = defaultState;
                    AddToHistory(currentState);
                    currentState.OnEnter.Invoke(this);
                }
                else
                {
                    IuvoDebug.DebugLogWarning("No valid state to revert to, remaining in current state.");
                }
            }


        }
    }
}
