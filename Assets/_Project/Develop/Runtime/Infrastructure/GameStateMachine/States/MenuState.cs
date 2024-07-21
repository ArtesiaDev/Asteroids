﻿using System;

namespace Develop.Runtime.Infrastructure.GameStateMachine.States
{
    public sealed class MenuState: IState
    {
        private readonly IStateMachine _stateMachine;

        private MenuState(IStateMachine stateMachine)
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