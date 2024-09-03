using System;
using Develop.Backend.Ads;
using Develop.Backend.Analytics;
using Develop.Runtime.Core.Spawn;
using Develop.Runtime.EventSignals;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Meta.Core
{
    public class CoreBackendPresenter : IInitializable, IDisposable
    {
        public event Action PlayerReincarnated;

        private ILaserSignals _laserSignals;
        private IBulletSignals _bulletSignals;
        private IPlayerSignals _playerSignals;
        private IPlayerSignalsHandler _playerSignalsHandler;
        private IGamePlayAnalytics _analytics;
        private IAdsService _ads;
        private PlayerSpawner _playerSpawner;
        private CoreModel _model;
        private EndPanelView _view;

        private bool _wasShown;


        [Inject]
        private void Construct(ILaserSignals laserSignals, IBulletSignals bulletSignals, IGamePlayAnalytics analytics,
            CoreModel model, EndPanelView view, IPlayerSignals playerSignals, IAdsService ads,
            IPlayerSignalsHandler playerSignalsHandler, PlayerSpawner playerSpawner)
        {
            _laserSignals = laserSignals;
            _bulletSignals = bulletSignals;
            _analytics = analytics;
            _model = model;
            _view = view;
            _playerSignals = playerSignals;
            _ads = ads;
            _playerSignalsHandler = playerSignalsHandler;
            _playerSpawner = playerSpawner;
        }


        public void Initialize()
        {
            _laserSignals.LaserShot += OnLaserShot;
            _bulletSignals.BulletShot += OnBulletShot;
            _playerSignals.PlayerDied += OnPlayerDiedBackend;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += GiveReward;
            PlayerReincarnated += _playerSignalsHandler.OnPlayerReincarnated;
        }

        public void Dispose()
        {
            _laserSignals.LaserShot -= OnLaserShot;
            _bulletSignals.BulletShot -= OnBulletShot;
            _playerSignals.PlayerDied -= OnPlayerDiedBackend;
            IronSourceRewardedVideoEvents.onAdRewardedEvent -= GiveReward;
            PlayerReincarnated -= _playerSignalsHandler.OnPlayerReincarnated;
        }

        public void ToMenuBackend()
        {
            _wasShown = false;
            _analytics.LogGameSFinished(_model.LaserUsedCount, _model.BulletUsedCount, _model.Score);
        }

        public void ReloadGameBackend()
        {
            _wasShown = false;
            _analytics.LogGameSFinished(_model.LaserUsedCount, _model.BulletUsedCount, _model.Score);
            _analytics.LogGameStarted();
        }

        public void StartRewardedVideo()
        {
            if (!_wasShown)
            {
                _ads.ShowRewarded();
                _wasShown = true;
            }
            else _view.ShowAdsBanMessage();
        }

        private void OnLaserShot()
        {
            _model.LaserUsedCountChange(_model.LaserUsedCount + 1);
            _analytics.LaserUsed();
        }

        private void OnBulletShot()
        {
            _model.BulletUsedCountChange(_model.BulletUsedCount + 1);
        }

        private void OnPlayerDiedBackend()
        {
            if (!_wasShown)
                _ads.LoadRewarded();
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