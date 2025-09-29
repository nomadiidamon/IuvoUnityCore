using IuvoUnity.Debug;
using UnityEngine;

namespace IuvoUnity
{
    namespace StateMachines
    {
        namespace CSM
        {
            public class ConditionalStateMachineAnalyzer : MonoBehaviour
            {
                public static ConditionalStateMachineAnalyzer Instance;
                [SerializeField] ConditionalStateMachine toWatch;
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
                        toWatch = this.gameObject.AddComponent<ConditionalStateMachine>();
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
                        if (toWatch is ConditionalHumanoidStateMachine humanoidStateMachine)
                        {
                            humanoidStateMachine.AssignTargetDestination(target.position);
                        }

                    }
                }
            }
        }
    }
}