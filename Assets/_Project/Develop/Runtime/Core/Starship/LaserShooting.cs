using System;
using System.Collections;
using System.Threading.Tasks;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Core.ShootingObjects;
using Develop.Runtime.Infrastructure.Factories;
using Develop.Runtime.Meta.EventSignals;
using Develop.Runtime.Services.Input.InputActions;
using UnityEngine;
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
        private Coroutine _cooldown;

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
            await _laserFactory.Prepare();
            _starship.StartCoroutine(Reload());
        }

        public void Dispose()
        {
            _laserFactory.Clear();
            _starship.StopAllCoroutines();
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
                if (_currentLaserShots > 0 && _cooldown == null)
                {
                    _currentLaserShots--;

                    var laser = await CreateLaser();

                    laser.Destroy(_config.LaserLifeTime);

                    _cooldown = _starship.StartCoroutine(Cooldown());
                }
            }
        }

        private IEnumerator Reload()
        {
            while (true)
            {
                yield return new WaitForSeconds(_config.ReloadTime);
                _currentLaserShots++;
            }
        }

        private IEnumerator Cooldown()
        {
            LaserCooldownChanged?.Invoke(_config.Cooldown);
            yield return new WaitForSeconds(_config.Cooldown);
            _cooldown = null;
        }

        private async Task<Laser> CreateLaser()
        {
            var position = _starship.transform.position + _starship.transform.up * _config.LaserOffsetCoefficient;
            return await _laserFactory.Create(position, _starship.transform.rotation);
        }
    }
}