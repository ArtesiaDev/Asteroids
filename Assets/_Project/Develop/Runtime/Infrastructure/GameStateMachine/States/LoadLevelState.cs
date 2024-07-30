using System.Collections;
using Develop.Runtime.Services.CoroutinePerformer;
using Develop.Runtime.Services.SceneLoader;
using UnityEngine;

namespace Develop.Runtime.Infrastructure.GameStateMachine.States
{
    public sealed class LoadLevelState: IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ICoroutinePerformer _coroutinePerformer;

        private LoadLevelState(IStateMachine stateMachine, ISceneLoader sceneLoader, ICoroutinePerformer coroutinePerformer)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _coroutinePerformer = coroutinePerformer;
        }
        
        public async void Enter()
        {
            await _sceneLoader.Load(Scene.Loading, OnLoaded);
        }

        public void EditorDebugEnter()
        {
            Debug.LogWarning($"The {nameof(LoadLevelState)} started in debug mode.");
        }

        private void OnLoaded()
        {
           // ToDo spawn everything what we need to load.

           _coroutinePerformer.StartPerform(WaitLoading());
        }

        public void Exit()
        {
            //ToDo loading factories Cache clear.
        }

        private IEnumerator WaitLoading()
        {
            yield return new WaitForSeconds(1f);
            _stateMachine.Enter<CoreState>();
        }
    }
}