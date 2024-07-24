using System;
using System.Collections;
using System.Threading.Tasks;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Core.ShootingObjects;
using Develop.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class LaserShooting : IFixedTickable, IInitializable, IDisposable
    {
        private readonly PlayerConfig _config;
        private readonly LaserFactory _laserFactory;
        private readonly Starship _starship;

        private int _currentLaserShots;
        private Coroutine _cooldown;

        public LaserShooting(PlayerConfig config, LaserFactory laserFactory, Starship starship)
        {
            _config = config;
            _laserFactory = laserFactory;
            _starship = starship;
            _currentLaserShots = _config.Ammunition;
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
        }

        public void FixedTick() =>
            Shoot();

        private async void Shoot()
        {
            if (Input.GetKey(KeyCode.Q))
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