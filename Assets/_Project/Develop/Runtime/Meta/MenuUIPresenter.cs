using Develop.Runtime.Infrastructure.GameStateMachine;
using Develop.Runtime.Infrastructure.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Meta
{
    public class MenuUIPresenter
    {
        private IStateMachine _stateMachine;
        private MenuUIView _view;
        
        [Inject]
        private void Construct(IStateMachine stateMachine, MenuUIView view)
        {
            _stateMachine = stateMachine;
            _view = view;
        }
        
        public void StartGame() =>
            _stateMachine.Enter<LoadLevelState>();

        public void OpenSettings() =>
            _view.SwitchPanelsRendering(false, true);

        public void ExitGame() =>
            Application.Quit();

        public void BackToMenu() =>
            _view.SwitchPanelsRendering(true, false);

    }
}