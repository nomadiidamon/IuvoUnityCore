//using System;
//using UnityEngine;
//using UnityEngine.AI;


//namespace IuvoUnity
//{
//    namespace _StateMachine
//    {
//        [CreateAssetMenu(fileName = "GenericMoveState", menuName = "StateMachine/States/MoveStates")]
//        public class GenericMoveState : GenericState
//        {
//            public float speed = 5f;
//            public Vector3 currentPosition;
//            public Vector3 targetPosition;
//            public Animator animator;
//            public float crossFadeTime = 0.1f;
//            public string currentAnimationName = "Move";
//            NavMeshAgent agent;

//            public void OnEnable()
//            {
//                OnE
//            }

//            public override void OnEnter(GenericStateMachine stateMachine)
//            {
//                Debug.Log("Entering Move State");
//                currentPosition = stateMachine.transform.position;
//                agent.speed = speed;
//                animator.CrossFade(currentAnimationName, crossFadeTime);
//                if (Vector3.Distance(agent.destination, targetPosition)  > 0.1f)
//                {
//                    agent.SetDestination(targetPosition);
//                }
//                if (!agent.autoRepath)
//                {
//                    agent.autoRepath = true;
//                }
//            }

//            public override void OnUpdate(GenericStateMachine stateMachine)
//            {
//                currentPosition = stateMachine.transform.position;
//                if (IsConditionMet(stateMachine))
//                {
//                    stateMachine.TryChangeState(stateMachine.defaultState);
//                }
//            }

//            public override void OnFixedUpdate(GenericStateMachine stateMachine)
//            {

//            }

//            public override void OnExit(GenericStateMachine stateMachine)
//            {
//                Debug.Log("Exiting Move State");
//            }

//            public override bool IsConditionMet(GenericStateMachine stateMachine)
//            {
//                float distance = Vector3.Distance(currentPosition, targetPosition);
//                if (distance < 0.5f)
//                {
//                    return true;
//                }
//                else
//                {
//                    agent.SetDestination(targetPosition);
//                    agent.CalculatePath(targetPosition, agent.path);
//                    return false;
//                }
//            }

//        }
//    }
//}