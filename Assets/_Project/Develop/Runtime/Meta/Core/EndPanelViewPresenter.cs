using System;
using Develop.Runtime.Infrastructure.GameStateMachine;
using Develop.Runtime.Infrastructure.GameStateMachine.States;
using Develop.Runtime.Meta.EventSignals;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Meta.Core
{
    public class EndPanelViewPresenter : IInitializable, IDisposable
    {
        private IPlayerSignals _playerSignals;
        private IStateMachine _stateMachine;
        private EndPanelView _view;
        private CoreUIModel _model;

        [Inject]
        private void Construct(IStateMachine stateMachine, IPlayerSignals playerSignals, EndPanelView view,
            CoreUIModel model)
        {
            _stateMachine = stateMachine;
            _playerSignals = playerSignals;
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _playerSignals.PlayerDied += OnPlayerDied;
        }

        public void Dispose()
        {
            _playerSignals.PlayerDied -= OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            Time.timeScale = 0;
            _view.SwitchPanelsRendering();
            _view.RenderFinalScore(_model.Score);
        }

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
    }
}