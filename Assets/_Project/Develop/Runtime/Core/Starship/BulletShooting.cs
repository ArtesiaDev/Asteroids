using System;
using System.Threading.Tasks;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Core.ShootingObjects;
using Develop.Runtime.EventSignals;
using Develop.Runtime.Infrastructure.Factories;
using Develop.Runtime.Services.Input.InputActions;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class BulletShooting : IFixedTickable, IInitializable, IDisposable
    {
        private event Action BulletShot;

        private readonly IBulletShootingConfig _config;
        private readonly IBulletShootAction _input;
        private readonly IBulletSignalsHandler _handler;
        private readonly Transform _transform;
        private readonly BulletFactory _bulletFactory;

        private float _nextFireTime;

        public BulletShooting(IBulletShootingConfig config, IBulletShootAction input, Transform transform,
            BulletFactory bulletFactory, IBulletSignalsHandler handler)
        {
            _config = config;
            _input = input;
            _transform = transform;
            _bulletFactory = bulletFactory;
            _handler = handler;
        }

        public async void Initialize()
        {
            BulletShot += _handler.OnBulletShot;
            _bulletFactory.CreateRoot();
            await _bulletFactory.Prepare();
        }

        public void Dispose()
        {
            BulletShot -= _handler.OnBulletShot;
            _bulletFactory.Clear();
        }

        public void FixedTick() =>
            Shoot();

        private async void Shoot()
        {
            if (_input.BulletShoot && Time.time > _nextFireTime)
            {
                UpdateNextFireTime();

                var bullet = await CreateBullet();
                BulletShot?.Invoke();

                ShootBullet(bullet);
            }
        }

        private void UpdateNextFireTime()
            => _nextFireTime = Time.time + _config.BulletFireRate;

        private async Task<Bullet> CreateBullet()
        {
            var position = _transform.position + _transform.up * _config.BulletOffsetCoefficient;
            return await _bulletFactory.Create(position, Quaternion.identity);
        }

        private void ShootBullet(Bullet bullet)
        {
            Vector2 thrust = _transform.up * _config.BulletSpeed;
            bullet.Shoot(thrust, ForceMode2D.Impulse);
        }
    }
}