using Develop.Runtime.Services.SceneLoader;
using UnityEngine;

namespace Develop.Runtime.Infrastructure.GameStateMachine.States
{
    public sealed class MenuState: IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        private MenuState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public async void Enter()
        {
            await _sceneLoader.Load(Scene.Menu, OnLoaded);
        }

        private void OnLoaded()
        {
           _stateMachine.Enter<LoadLevelState>();
        }

        public void Exit()
        {
            Debug.Log("[MenuState] Exit");
        }
    }
}