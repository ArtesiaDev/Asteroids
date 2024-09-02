using System;
using Cysharp.Threading.Tasks;
using Develop.Runtime.Core.Spawn;
using Develop.Runtime.EventSignals;
using R3;
using Zenject;

namespace Develop.Runtime.Meta.Core
{
    public class StatsPanelController : IInitializable, IDisposable
    {
        private IPlayerSignals _playerSignals;
        private ILaserSignals _laserSignals;
        private StatsPanelView _view;
        private PlayerSpawner _playerSpawner;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        [Inject]
        private void Construct(IPlayerSignals playerSignals, ILaserSignals laserSignals, StatsPanelView view,
            PlayerSpawner  playerSpawner)
        {
            _playerSignals = playerSignals;
            _laserSignals = laserSignals;
            _view = view;
            _playerSpawner = playerSpawner;
        }

        public void Initialize()
        {
            _playerSignals.PlayerMoved += OnPlayerMoved;
            _playerSignals.PlayerSteered += OnPlayerSteered;
            _laserSignals.LaserAmmunition.Subscribe(OnLaserAmmunitionChanged).AddTo(_disposable);
            _laserSignals.LaserCooldown.Subscribe(OnLaserCooldownChanged).AddTo(_disposable);
        }

        public void Dispose()
        {
            _playerSignals.PlayerMoved -= OnPlayerMoved;
            _playerSignals.PlayerSteered -= OnPlayerSteered;
            _disposable.Dispose();
        }

        private void OnPlayerMoved()
        {
            _view.RerenderCoordinates(_playerSpawner.PlayerPrefab.transform.position.x,
                _playerSpawner.PlayerPrefab.transform.position.y);
            _view.RerenderVelocity(_playerSpawner.PlayerRb.velocity.magnitude);
        }

        private void OnPlayerSteered() =>
            _view.RerenderRotation(_playerSpawner.PlayerPrefab.transform.eulerAngles.z);

        private void OnLaserAmmunitionChanged(int currentLaserShots) =>
            _view.RerenderLaserAmmunition(currentLaserShots);

        private async void OnLaserCooldownChanged(float cooldown) =>
            await CooldownTimer(cooldown);

        private async UniTask CooldownTimer(float time)
        {
            var step = 0.1f;
            var delay = 0.003f;

            for (var i = time; i >= 0; i -= step)
            {
                _view.RerenderLaserCooldown(i);
                await UniTask.WaitForSeconds(step - delay);
            }
        }
    }
}