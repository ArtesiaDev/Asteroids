using Develop.Runtime.Services.SceneLoader;
using UnityEngine;

namespace Develop.Runtime.Infrastructure.GameStateMachine.States
{
    public sealed class CoreState: IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        private CoreState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public async void Enter()
        {
            await _sceneLoader.Load(Scene.Core, OnLoaded);
        }

        public void EditorDebugEnter()
        {
            Debug.LogWarning($"The {nameof(CoreState)} started in debug mode.");
        }

        private void OnLoaded()
        {
        }

        public void Exit()
        {
            // ToDo any saves if it needs.
        }
    }
}