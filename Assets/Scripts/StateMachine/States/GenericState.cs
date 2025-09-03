using IuvoUnity.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace IuvoUnity
{
    namespace StateMachine
    {
        public enum UpdateMode
        {
            Update,
            FixedUpdate,
            LateUpdate,
            None
        }

        [System.Serializable]
        [CreateAssetMenu(fileName = "GenericState", menuName = "IuvoUnity/StateMachine/GenericState", order = 1)] // asset creation menu
        public class GenericState : ScriptableObject, IStateMachineCondition
        {
            public string stateName;

            // Custom event class combining Unity and System callbacks.
            // Invoke() automatically calls both UnityEvent and C# event and checks for listeners using ?.
            public FlexibleEvent<GenericStateMachine> OnInterrupt = new FlexibleEvent<GenericStateMachine>();
            public FlexibleEvent<GenericStateMachine> OnExit = new FlexibleEvent<GenericStateMachine>();
            public FlexibleEvent<GenericStateMachine> OnEnter = new FlexibleEvent<GenericStateMachine>();
            public FlexibleEvent<GenericStateMachine> OnContinue = new FlexibleEvent<GenericStateMachine>();

            // Lists of conditions for entering, continuing, exiting, and interrupting the state
            public List<IStateACondition> stateInterruptConditions = new List<IStateACondition>();
            public List<IStateACondition> stateExitConditions = new List<IStateACondition>();
            public List<IStateACondition> stateEnterConditions = new List<IStateACondition>();
            public List<IStateACondition> stateContinueConditions = new List<IStateACondition>();

            // Update mode for the state, determines how it is updated in the state machine
            public UpdateMode updateMode = UpdateMode.None;

            // public checks that run conditions
            public bool CanInterrupt(GenericStateMachine stateMachine)
            {
                return InterruptConditionsMet(stateMachine);
            }
            public bool CanExit(GenericStateMachine stateMachine)
            {
                return ExitConditionsMet(stateMachine);
            }
            public bool CanEnter(GenericStateMachine stateMachine)
            {
                return EnterConditionsMet(stateMachine);
            }
            public bool CanContinue(GenericStateMachine stateMachine)
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
            public virtual bool InterruptConditionsMet(GenericStateMachine stateMachine)
            {
                return AreConditionsMet(stateInterruptConditions);
            }
            public virtual bool ExitConditionsMet(GenericStateMachine stateMachine)
            {
                return AreConditionsMet(stateExitConditions);
            }
            public virtual bool EnterConditionsMet(GenericStateMachine stateMachine)
            {
                return AreConditionsMet(stateEnterConditions);
            }
            public virtual bool ContinueConditionsMet(GenericStateMachine stateMachine)
            {
                return AreConditionsMet(stateContinueConditions);
            }

        }
    }
}