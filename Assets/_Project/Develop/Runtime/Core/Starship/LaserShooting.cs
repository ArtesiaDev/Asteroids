using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Core.ShootingObjects;
using Develop.Runtime.EventSignals;
using Develop.Runtime.Infrastructure.Factories;
using Develop.Runtime.Services.Input.InputActions;
using R3;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class LaserShooting : IFixedTickable, IInitializable, IDisposable
    {
        private readonly ReactiveProperty<int> _laserAmmunition = new ReactiveProperty<int>();
        private readonly ReactiveProperty<float> _laserCooldown = new ReactiveProperty<float>();

        private readonly ILaserShootingConfig _config;
        private readonly ILaserShootAction _input;
        private readonly LaserFactory _laserFactory;
        private readonly Starship _starship;
        private readonly ILaserSignalsHandler _laserSignalsHandler;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public LaserShooting(ILaserShootingConfig config, ILaserShootAction input, LaserFactory laserFactory,
            Starship starship, ILaserSignalsHandler laserSignalsHandler)
        {
            _config = config;
            _input = input;
            _laserFactory = laserFactory;
            _starship = starship;
            _laserSignalsHandler = laserSignalsHandler;
        }

        public async void Initialize()
        {
            _laserAmmunition.Subscribe(value => _laserSignalsHandler.OnLaserAmmunitionChanged(value)).AddTo(_disposable);
            _laserCooldown.Subscribe(value => _laserSignalsHandler.OnLaserCooldownChanged(value)).AddTo(_disposable);
            _laserAmmunition.Value = _config.Ammunition;
            await _laserFactory.Prepare();
            await Reload();
        }

        public void Dispose()
        {
            _laserFactory.Clear();
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
           _disposable.Dispose();
        }

        public void FixedTick() =>
            Shoot();

        private async void Shoot()
        {
            if (_input.LaserShoot)
            {
                if (_laserAmmunition.Value > 0 && _laserCooldown.Value == 0)
                {
                   _laserAmmunition.Value--;

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
                catch {break;}

                _laserAmmunition.Value++;
            }
        }

        private async UniTask Cooldown()
        {
            _laserCooldown.Value = _config.Cooldown;
            
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_config.Cooldown), DelayType.DeltaTime,
                    PlayerLoopTiming.FixedUpdate, _cancellationTokenSource.Token);
            }
            catch { }

            _laserCooldown.Value = 0f;
        }

        private async Task<Laser> CreateLaser()
        {
            var position = _starship.transform.position + _starship.transform.up * _config.LaserOffsetCoefficient;
            return await _laserFactory.Create(position, _starship.transform.rotation);
        }
    }
}