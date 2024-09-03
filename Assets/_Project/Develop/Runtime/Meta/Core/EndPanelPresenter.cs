using System;
using Develop.Runtime.EventSignals;
using Develop.Runtime.Infrastructure.GameStateMachine;
using Develop.Runtime.Infrastructure.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Meta.Core
{
    public class EndPanelPresenter : IInitializable, IDisposable
    {
        private IPlayerSignals _playerSignals;
        private IStateMachine _stateMachine;
        private IPlayerSignalsHandler _playerSignalsHandler;
        private EndPanelView _view;
        private CoreModel _model;
        

        [Inject]
        private void Construct(IStateMachine stateMachine, IPlayerSignals playerSignals, EndPanelView view, CoreModel model)
        {
            _stateMachine = stateMachine;
            _playerSignals = playerSignals;
            _view = view;
            _model = model;
        }

        public void Initialize() =>
            _playerSignals.PlayerDied += OnPlayerDied;

        public void Dispose() =>
            _playerSignals.PlayerDied -= OnPlayerDied;

        public void ToMenu()
        {
            Time.timeScale = 1;
            _stateMachine.Enter<MenuState>();
        }

        public void ReloadGame()
        {
            Time.timeScale = 1;
            _stateMachine.Enter<LoadLevelState>();
        }

        private void OnPlayerDied()
        {
            Time.timeScale = 0;
            _view.SwitchPanelsRendering(false, true);
            _view.RenderFinalScore(_model.Score);
        }
    }
}