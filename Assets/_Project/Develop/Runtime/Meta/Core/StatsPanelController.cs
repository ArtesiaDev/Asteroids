using System;
using Cysharp.Threading.Tasks;
using Develop.Runtime.Meta.EventSignals;
using Zenject;

namespace Develop.Runtime.Meta.Core
{
    public class StatsPanelController: IInitializable, IDisposable
    {
        private IPlayerSignals _playerSignals;
        private ILaserSignals _laserSignals;
        private StatsPanelView _view;
        
        [Inject]
        private void Construct(IPlayerSignals playerSignals, ILaserSignals laserSignals, StatsPanelView view)
        {
            _playerSignals = playerSignals;
            _laserSignals = laserSignals;
            _view = view;
        }
        
        public void Initialize()
        {
            _playerSignals.PlayerMoved += OnPlayerMoved;
            _playerSignals.PlayerSteered += OnPlayerSteered;
            _laserSignals.LaserAmmunitionChanged += OnLaserAmmunitionChanged;
            _laserSignals.LaserCooldownChanged += OnLaserCooldownChanged;
        }

        public void Dispose()
        {
            _playerSignals.PlayerMoved -= OnPlayerMoved;
            _playerSignals.PlayerSteered -= OnPlayerSteered;
            _laserSignals.LaserAmmunitionChanged -= OnLaserAmmunitionChanged;
            _laserSignals.LaserCooldownChanged -= OnLaserCooldownChanged;
        }
        
        private void OnPlayerMoved()
        {
            _view.RerenderCoordinates();
            _view.RerenderVelocity();
        }

        private void OnPlayerSteered() =>
            _view.RerenderRotation();

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