using Develop.Runtime.Services.SceneLoader;

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

        private void OnLoaded()
        {
        }

        public void Exit()
        {
            // ToDo any saves if it needs.
        }
    }
}