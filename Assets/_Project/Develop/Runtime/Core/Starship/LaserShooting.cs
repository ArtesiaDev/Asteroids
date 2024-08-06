using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Core.ShootingObjects;
using Develop.Runtime.EventSignals;
using Develop.Runtime.Infrastructure.Factories;
using Develop.Runtime.Services.Input.InputActions;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class LaserShooting : IFixedTickable, IInitializable, IDisposable
    {
        public event Action<int> LaserAmmunitionChanged;
        public event Action<float> LaserCooldownChanged;

        private readonly ILaserShootingConfig _config;
        private readonly ILaserShootAction _input;
        private readonly LaserFactory _laserFactory;
        private readonly Starship _starship;
        private readonly ILaserSignalsHandler _laserSignalsHandler;

        private int _currentLaserShots;
        private bool _isCooldown;
        private CancellationTokenSource _cancellationTokenSource;

        public LaserShooting(ILaserShootingConfig config, ILaserShootAction input, LaserFactory laserFactory,
            Starship starship, ILaserSignalsHandler laserSignalsHandler)
        {
            _config = config;
            _input = input;
            _laserFactory = laserFactory;
            _starship = starship;
            _currentLaserShots = _config.Ammunition;
            _laserSignalsHandler = laserSignalsHandler;
            LaserAmmunitionChanged += _laserSignalsHandler.OnLaserAmmunitionChanged;
            LaserCooldownChanged += _laserSignalsHandler.OnLaserCooldownChanged;
        }

        public async void Initialize()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await _laserFactory.Prepare();
            await Reload();
        }

        public void Dispose()
        {
            _laserFactory.Clear();
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            LaserAmmunitionChanged -= _laserSignalsHandler.OnLaserAmmunitionChanged;
            LaserCooldownChanged -= _laserSignalsHandler.OnLaserCooldownChanged;
        }

        public void FixedTick()
        {
            Shoot();
            LaserAmmunitionChanged?.Invoke(_currentLaserShots);
        }

        private async void Shoot()
        {
            if (_input.LaserShoot)
            {
                if (_currentLaserShots > 0 && !_isCooldown)
                {
                    _currentLaserShots--;

                    var laser = await CreateLaser();

                    laser.Destroy(_config.LaserLifeTime);

                    await Cooldown();
                }
            }
        }

        private async UniTask Reload()
        {
            while (_starship != null)
            {
                try
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_config.ReloadTime), DelayType.DeltaTime,
                        PlayerLoopTiming.FixedUpdate, _cancellationTokenSource.Token);
                }
                catch { break; }

                _currentLaserShots++;
            }
        }

        private async UniTask Cooldown()
        {
            _isCooldown = true;
            LaserCooldownChanged?.Invoke(_config.Cooldown);
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_config.Cooldown), DelayType.DeltaTime,
                    PlayerLoopTiming.FixedUpdate, _cancellationTokenSource.Token);
            }
            catch {}
            _isCooldown = false;
        }

        private async Task<Laser> CreateLaser()
        {
            var position = _starship.transform.position + _starship.transform.up * _config.LaserOffsetCoefficient;
            return await _laserFactory.Create(position, _starship.transform.rotation);
        }
    }
}