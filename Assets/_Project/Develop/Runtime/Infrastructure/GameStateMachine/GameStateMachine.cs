using System;
using System.Collections.Generic;
using System.ComponentModel;
using Develop.Backend.Ads;
using Develop.Runtime.Infrastructure.Factories;
using Develop.Runtime.Infrastructure.GameStateMachine.States;
using UnityEngine.SceneManagement;
using Zenject;
using Scene = Develop.Runtime.Services.SceneLoader.Scene;

namespace Develop.Runtime.Infrastructure.GameStateMachine
{
    public sealed class GameStateMachine : IInitializable, IDisposable, IStateMachine
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;
        private readonly StateFactory _factory;
        private readonly IronSourceAdsInitialize _ads;
        private readonly GameAnalyticsFactory _gameAnalytics;

        public GameStateMachine(StateFactory factory, IronSourceAdsInitialize ads, GameAnalyticsFactory gameAnalytics)
        {
            _factory = factory;
            _ads = ads;
            _gameAnalytics = gameAnalytics;
        }

        public async void Initialize()
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(MenuState)] = _factory.Create<MenuState>(),
                [typeof(LoadLevelState)] = _factory.Create<LoadLevelState>(),
                [typeof(CoreState)] = _factory.Create<CoreState>(),
            };
            _ads.Initialize();
            EnterStartState();
            await _gameAnalytics.Create();
        }

        public void Dispose() =>
            _gameAnalytics.Clear();

        public void Enter<T>() where T : IState
        {
            _currentState?.Exit();
            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }

        private void EditorDebugEnter<T>() where T : IState
        {
            _currentState = _states[typeof(T)];
            _currentState.EditorDebugEnter();
        }

        private void EnterStartState()
        {
            var scene = SceneManager.GetActiveScene().name;

            if (Enum.TryParse(scene, out Scene currentSceneEnum))
            {
                switch (currentSceneEnum)
                {
                    case Scene.Menu:
                        Enter<MenuState>();
                        break;
                    case Scene.Loading:
                        EditorDebugEnter<LoadLevelState>();
                        break;
                    case Scene.Core:
                        EditorDebugEnter<CoreState>();
                        break;
                }
            }
            else throw new InvalidEnumArgumentException();
        }
    }
}