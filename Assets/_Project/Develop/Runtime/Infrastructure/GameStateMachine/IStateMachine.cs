using Develop.Runtime.Infrastructure.GameStateMachine.States;

namespace Develop.Runtime.Infrastructure.GameStateMachine
{
    public interface IStateMachine
    {
        public void Enter<T>() where T : IState;
    }
}