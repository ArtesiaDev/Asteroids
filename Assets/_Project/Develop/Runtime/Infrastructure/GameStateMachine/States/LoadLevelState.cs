using System;

namespace Develop.Runtime.Infrastructure.GameStateMachine.States
{
    public sealed class LoadLevelState: IState
    {
        private readonly IStateMachine _stateMachine;

        private LoadLevelState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public async void Enter()
        {
            throw new NotImplementedException();
        }

        private void OnLoaded()
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}