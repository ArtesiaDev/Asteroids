using System;
using Develop.Runtime.Services.SceneLoader;

namespace Develop.Runtime.Infrastructure.GameStateMachine.States
{
    public sealed class LoadLevelState: IState
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

        private void OnLoaded()
        {
           // ToDo spawn everything what we need to load.
           
           _stateMachine.Enter<CoreState>();
        }

        public void Exit()
        {
            //ToDo loading factories Cashe clear.
        }
    }
}