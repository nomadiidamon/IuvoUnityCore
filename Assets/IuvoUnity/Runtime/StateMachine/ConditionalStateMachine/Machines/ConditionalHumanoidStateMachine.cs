using UnityEngine;

namespace IuvoUnity
{
    namespace StateMachines
    {
        namespace CSM
        {
            [System.Serializable]
            public class ConditionalHumanoidStateMachine : ConditionalStateMachine
            {
                //[Header("States")]
                //public GenericIdleState idle;
                //public GenericMoveState move;

                public new void Start()
                {
                    //if (idle != null)
                    //{
                    //    states.Add(idle);
                    //}
                    //if (move != null)
                    //{
                    //    states.Add(move);
                    //}
                    base.Start();

                }

                public override void Update()
                {
                    base.Update();
                    // do human stuff
                }

                public override void FixedUpdate()
                {
                    base.FixedUpdate();
                    // do human stuff
                }

                public virtual void AssignTargetDestination(Vector3 newDestination)
                {
                    //if (move != null && move.targetPosition != newDestination)
                    //{
                    //    move.targetPosition = newDestination;
                    //    TryChangeState(move);
                    //}
                }
            }
        }
    }
}