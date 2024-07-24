using System;
using System.Threading.Tasks;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Core.ShootingObjects;
using Develop.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    public class BulletShooting : IFixedTickable, IInitializable, IDisposable
    {
        private readonly IBulletShootingConfig _config;
        private readonly Transform _transform;
        private readonly BulletFactory _bulletFactory;

        private float _nextFireTime;

        public BulletShooting(IBulletShootingConfig config, Transform transform, BulletFactory bulletFactory)
        {
            _config = config;
            _transform = transform;
            _bulletFactory = bulletFactory;
        }

        public async void Initialize()
        {
            await _bulletFactory.Prepare();
            _bulletFactory.CreateRoot();
        }

        public void Dispose() =>
            _bulletFactory.Clear();

        public void FixedTick() =>
            Shoot();

        private async void Shoot()
        {
            if (Input.GetKey(KeyCode.E) && Time.time > _nextFireTime)
            {
                UpdateNextFireTime();

                var bullet = await CreateBullet();

                ShootBullet(bullet);
            }
        }

        private void UpdateNextFireTime() 
            => _nextFireTime = Time.time + _config.FireRate;

        private async Task<Bullet> CreateBullet()
        {
            var position = _transform.position + _transform.up * _config.BulletOffsetCoefficient;
            return await _bulletFactory.Create(position, Quaternion.identity);
        }

        private void ShootBullet(Bullet bullet)
        {
            Vector2 thrust = _transform.up * _config.Speed;
            bullet.Shoot(thrust, ForceMode2D.Impulse);
        }
    }
}