using Develop.Backend;
using Develop.Runtime.Services.SceneLoader;
using UnityEngine;

namespace Develop.Runtime.Infrastructure.GameStateMachine.States
{
    public sealed class MenuState: IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAnalyticsService _analyticsService;

        private MenuState(IStateMachine stateMachine, ISceneLoader sceneLoader, IAnalyticsService analyticsService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _analyticsService = analyticsService;
        }
        
        public async void Enter()
        {
            await _sceneLoader.Load(Scene.Menu, OnLoaded);
        }

        public void EditorDebugEnter()
        {
        }

        private void OnLoaded()
        {
            _analyticsService.LogGameStart();
        }

        public void Exit()
        {
        }
    }
}