using Cysharp.Threading.Tasks;
using Develop.Runtime.Services.SceneLoader;
using UnityEngine;

namespace Develop.Runtime.Infrastructure.GameStateMachine.States
{
    public sealed class LoadLevelState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        private LoadLevelState(IStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Enter()
        {
            await _sceneLoader.Load(Scene.Loading, OnLoaded);
        }

        public void EditorDebugEnter()
        {
            Debug.LogWarning($"The {nameof(LoadLevelState)} started in debug mode.");
        }

        private async void OnLoaded()
        {
            // ToDo spawn everything what we need to load.
            await WaitLoading();
        }

        public void Exit()
        {
            //ToDo loading factories Cache clear.
        }

        private async UniTask WaitLoading()
        {
            await UniTask.WaitForSeconds(1f);
            _stateMachine.Enter<CoreState>();
        }
    }
}