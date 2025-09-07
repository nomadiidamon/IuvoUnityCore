using IuvoUnity.Debug;
using UnityEngine;

namespace IuvoUnity
{
    namespace StateMachine
    {
        public class StateMachineAnalyzer : MonoBehaviour
        {
            public static StateMachineAnalyzer Instance;
            [SerializeField] GenericStateMachine toWatch;
            [SerializeField] Transform target;

            void Awake()
            {
                if (Instance == null)
                {
                    Instance = this;
                }

            }

            public void Start()
            {
                if (toWatch == null)
                {
                    IuvoDebug.DebugLogWarning("StateMachineAnalyzer: No state machine assigned to watch. Adding a machine to this to watch");
                    toWatch = this.gameObject.AddComponent<GenericStateMachine>();
                }
                if (target == null)
                {
                    IuvoDebug.DebugLogWarning("StateMachineAnalyzer: No target assigned to follow. Default is this");
                    target = this.transform;
                }
            }

            void Update()
            {
                if (target != null && toWatch != null)
                {
                    if (toWatch is HumanoidStateMachine humanoidStateMachine)
                    {
                        humanoidStateMachine.AssignTargetDestination(target.position);
                    }

                }
            }
        }
    }
}