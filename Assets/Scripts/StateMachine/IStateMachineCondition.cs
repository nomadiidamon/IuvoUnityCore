using IuvoUnity.BaseClasses;

namespace IuvoUnity
{
	namespace StateMachine
	{
		public interface IStateMachineCondition : IuvoInterfaceBase
		{
            bool EnterConditionsMet(GenericStateMachine stateMachine);
            bool ContinueConditionsMet(GenericStateMachine stateMachine);
            bool ExitConditionsMet(GenericStateMachine stateMachine);
            bool InterruptConditionsMet(GenericStateMachine stateMachine);
        }
	}
}