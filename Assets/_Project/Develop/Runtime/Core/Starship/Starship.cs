using Develop.Runtime.Core.Configs;
using Develop.Runtime.Infrastructure.Factories;
using Develop.Runtime.Services.Input;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Starship : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _config;

        private Rigidbody2D _rb;
        private MoverSystem _moverSystem;
        private SteeringSystem _steeringSystem;
        private TeleportationSystem _teleportationSystem;
        private BulletShooting _bulletShooting;
        private BulletFactory _bulletFactory;
        private LaserShooting _laserShooting;
        private LaserFactory _laserFactory;
        private IInput _input;

        [Inject]
        private void Construct(BulletFactory bulletFactory, LaserFactory laserFactory, IInput input)
        {
            _bulletFactory = bulletFactory;
            _laserFactory = laserFactory;
            _input = input;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _moverSystem = new MoverSystem(_config, _input, _rb, transform);
            _steeringSystem = new SteeringSystem(_config, _input, transform);
            _teleportationSystem = new TeleportationSystem(transform);
            _bulletShooting = new BulletShooting(_config, _input, transform, _bulletFactory);
            _bulletShooting.Initialize();
            _laserShooting = new LaserShooting(_config, _input, _laserFactory, this);
            _laserShooting.Initialize();
        }

        private void FixedUpdate()
        {
            _moverSystem.FixedTick();
            _steeringSystem.FixedTick();
            _teleportationSystem.FixedTick();
            _bulletShooting.FixedTick();
            _laserShooting.FixedTick();
        }

        private void OnDisable()
        {
            _bulletShooting.Dispose();
            _laserShooting.Dispose();
        }
    }
}