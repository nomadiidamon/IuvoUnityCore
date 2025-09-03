//using System;
//using UnityEngine;

//namespace IuvoUnity
//{
//    namespace _StateMachine
//    {
//        [CreateAssetMenu(fileName = "IdleState", menuName = "StateMachine/States/IdleStates")]
//        public class GenericIdleState : GenericState
//        {

//            public override void OnEnter(GenericStateMachine stateMachine)
//            {
//                Debug.Log("Entering Idle State");
//                animator = stateMachine.GetComponent<Animator>();
//                animator.CrossFade(animationName, crossFadeTime);
//                stateMachine.currentAnim = animationName;
//            }

//            public override void OnUpdate(GenericStateMachine stateMachine)
//            {
//            }

//            public override void OnFixedUpdate(GenericStateMachine stateMachine)
//            {

//            }

//            public override void OnExit(GenericStateMachine stateMachine)
//            {
//                Debug.Log("Exiting Idle State");
//            }

//            public override bool IsConditionMet(GenericStateMachine stateMachine)
//            {
//                throw new NotImplementedException();
//            }
//        }
//    }
//}