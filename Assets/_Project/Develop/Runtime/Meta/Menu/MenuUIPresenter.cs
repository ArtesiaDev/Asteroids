using Develop.Backend;
using Develop.Runtime.Infrastructure.GameStateMachine;
using Develop.Runtime.Infrastructure.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Meta.Menu
{
    public class MenuUIPresenter
    {
        private IStateMachine _stateMachine;
        private IAnalyticsService _analyticsService;
        private MenuUIView _view;

        [Inject]
        private void Construct(IStateMachine stateMachine, MenuUIView view, IAnalyticsService analyticsService)
        {
            _stateMachine = stateMachine;
            _view = view;
            _analyticsService = analyticsService;
        }
        
        public void StartGame() =>
            _stateMachine.Enter<LoadLevelState>();

        public void OpenSettings() =>
            _view.SwitchPanelsRendering(false, true);

        public void ExitGame()
        {
            _analyticsService.LogGameEnd(2, 2);
            Application.Quit();
        }

        public void BackToMenu() =>
            _view.SwitchPanelsRendering(true, false);

    }
}