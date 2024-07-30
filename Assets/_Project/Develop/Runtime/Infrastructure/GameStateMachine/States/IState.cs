namespace Develop.Runtime.Infrastructure.GameStateMachine.States
{
    public interface IState
    {
        public void Enter();
        public void EditorDebugEnter();
        public void Exit();
    }
}