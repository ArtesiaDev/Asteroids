using System;
using System.Collections.Generic;
using Develop.Runtime.Infrastructure.Factories;
using Develop.Runtime.Infrastructure.GameStateMachine.States;
using Zenject;

namespace Develop.Runtime.Infrastructure.GameStateMachine
{
    public sealed class GameStateMachine : IInitializable, IStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;
        private readonly StateFactory _factory;

        public GameStateMachine(StateFactory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(MenuState)] = _factory.Create<MenuState>(),
                [typeof(LoadLevelState)] = _factory.Create<LoadLevelState>(),
                [typeof(CoreState)] = _factory.Create<CoreState>(),
            };
            Enter<MenuState>();
        }

        public void Enter<T>() where T : IState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }
    }
}