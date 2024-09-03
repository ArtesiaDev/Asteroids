using System;
using Develop.Backend.Ads;
using Develop.Runtime.Core.Spawn;
using Develop.Runtime.EventSignals;
using Develop.Runtime.Infrastructure.GameStateMachine;
using Develop.Runtime.Infrastructure.GameStateMachine.States;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Meta.Core
{
    public class EndPanelPresenter : IInitializable, IDisposable
    {
        public event Action PlayerReincarnated;

        private IPlayerSignals _playerSignals;
        private IStateMachine _stateMachine;
        private IAdsService _ads;
        private IPlayerSignalsHandler _playerSignalsHandler;
        private EndPanelView _view;
        private CoreUIModel _model;
        private PlayerSpawner _playerSpawner;

        [Inject]
        private void Construct(IStateMachine stateMachine, IPlayerSignals playerSignals, EndPanelView view,
            CoreUIModel model, IAdsService ads, PlayerSpawner playerSpawner, IPlayerSignalsHandler playerSignalsHandler)
        {
            _stateMachine = stateMachine;
            _playerSignals = playerSignals;
            _view = view;
            _model = model;
            _ads = ads;
            _playerSpawner = playerSpawner;
            _playerSignalsHandler = playerSignalsHandler;
        }

        public void Initialize()
        {
            _playerSignals.PlayerDied += OnPlayerDied;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += GiveReward;
            PlayerReincarnated += _playerSignalsHandler.OnPlayerReincarnated;
        }

        public void Dispose()
        {
            _playerSignals.PlayerDied -= OnPlayerDied;
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= GiveReward;
            PlayerReincarnated -= _playerSignalsHandler.OnPlayerReincarnated;
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

        public void StartRewardedVideo() =>
            _ads.ShowRewarded();

        private void OnPlayerDied()
        {
            Time.timeScale = 0;
            _ads.LoadRewarded();
            _view.SwitchPanelsRendering(false, true);
            _view.RenderFinalScore(_model.Score);
        }

        private void GiveReward(IronSourcePlacement placement, IronSourceAdInfo adInfo)
        {
            PlayerReincarnated?.Invoke();
            _view.SwitchPanelsRendering(true, false);
            _playerSpawner.SpawnPlayer();
            Time.timeScale = 1;
        }
    }
}