using IuvoUnity.Interfaces;
using IuvoUnity.Events;
using System.Collections.Generic;
using UnityEngine;

namespace IuvoUnity
{
    namespace StateMachines
    {
        namespace CSM
        {
            public enum ConditionalStateMachineUpdateMode
            {
                Update,
                FixedUpdate,
                LateUpdate,
                None
            }

            [System.Serializable]
            [CreateAssetMenu(fileName = "GenericState", menuName = "IuvoUnity/StateMachine/GenericState", order = 1)] // asset creation menu
            public class ConditionalState : ScriptableObject, IStateMachineCondition
            {
                public string stateName;

                // Custom event class combining Unity and System callbacks.
                // Invoke() automatically calls both UnityEvent and C# event and checks for listeners using ?.
                public FlexibleEvent<ConditionalStateMachine> OnInterrupt = new FlexibleEvent<ConditionalStateMachine>();
                public FlexibleEvent<ConditionalStateMachine> OnExit = new FlexibleEvent<ConditionalStateMachine>();
                public FlexibleEvent<ConditionalStateMachine> OnEnter = new FlexibleEvent<ConditionalStateMachine>();
                public FlexibleEvent<ConditionalStateMachine> OnContinue = new FlexibleEvent<ConditionalStateMachine>();

                // Lists of conditions for entering, continuing, exiting, and interrupting the state
                public List<IStateACondition> stateInterruptConditions = new List<IStateACondition>();
                public List<IStateACondition> stateExitConditions = new List<IStateACondition>();
                public List<IStateACondition> stateEnterConditions = new List<IStateACondition>();
                public List<IStateACondition> stateContinueConditions = new List<IStateACondition>();

                // Update mode for the state, determines how it is updated in the state machine
                public ConditionalStateMachineUpdateMode updateMode = ConditionalStateMachineUpdateMode.None;

                // public checks that run conditions
                public bool CanInterrupt(ConditionalStateMachine stateMachine)
                {
                    return InterruptConditionsMet(stateMachine);
                }
                public bool CanExit(ConditionalStateMachine stateMachine)
                {
                    return ExitConditionsMet(stateMachine);
                }
                public bool CanEnter(ConditionalStateMachine stateMachine)
                {
                    return EnterConditionsMet(stateMachine);
                }
                public bool CanContinue(ConditionalStateMachine stateMachine)
                {
                    return ContinueConditionsMet(stateMachine);
                }

                protected bool AreConditionsMet(List<IStateACondition> conditions)
                {
                    if (conditions.Count == 0) return true;
                    foreach (var c in conditions)
                    {
                        if (c == null) continue;
                        if (!c.IsConditionMet()) return false;
                    }
                    return true;
                }

                /// <summary>
                /// Default implementations of the condition checks, override in derived classes
                /// </summary>
                /// <param name="stateMachine"></param>
                /// <returns></returns>
                public virtual bool InterruptConditionsMet(ConditionalStateMachine stateMachine)
                {
                    return AreConditionsMet(stateInterruptConditions);
                }
                public virtual bool ExitConditionsMet(ConditionalStateMachine stateMachine)
                {
                    return AreConditionsMet(stateExitConditions);
                }
                public virtual bool EnterConditionsMet(ConditionalStateMachine stateMachine)
                {
                    return AreConditionsMet(stateEnterConditions);
                }
                public virtual bool ContinueConditionsMet(ConditionalStateMachine stateMachine)
                {
                    return AreConditionsMet(stateContinueConditions);
                }

            }
        }
    }
}