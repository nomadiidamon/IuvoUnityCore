using IuvoUnity.BaseClasses;
using IuvoUnity.StateMachines.CSM;

namespace IuvoUnity
{
	namespace Interfaces
	{
		public interface IStateMachineCondition : IuvoInterfaceBase
		{
            bool EnterConditionsMet(ConditionalStateMachine stateMachine);
            bool ContinueConditionsMet(ConditionalStateMachine stateMachine);
            bool ExitConditionsMet(ConditionalStateMachine stateMachine);
            bool InterruptConditionsMet(ConditionalStateMachine stateMachine);
        }
	}
}