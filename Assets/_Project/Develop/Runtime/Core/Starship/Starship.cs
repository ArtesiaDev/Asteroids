using System;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Infrastructure.Factories;
using Develop.Runtime.Meta.EventSignals;
using Develop.Runtime.Services.Input;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Starship
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(EdgeCollider2D))]
    public class Starship : MonoBehaviour
    {
        public event Action PlayerDied;
        
        [SerializeField] private PlayerConfig _config;

        private Rigidbody2D _rb;
        private EdgeCollider2D _collider;
        private MoverSystem _moverSystem;
        private SteeringSystem _steeringSystem;
        private TeleportationSystem _teleportationSystem;
        private BulletShooting _bulletShooting;
        private BulletFactory _bulletFactory;
        private LaserShooting _laserShooting;
        private LaserFactory _laserFactory;
        private IInput _input;
        private IPlayerSignalsHandler _playerSignalsHandler;
        private ILaserSignalsHandler _laserSignalsHandler;

        [Inject]
        private void Construct(BulletFactory bulletFactory, LaserFactory laserFactory, IInput input,
            IPlayerSignalsHandler playerSignalsHandler, ILaserSignalsHandler laserSignalsHandler)
        {
            _bulletFactory = bulletFactory;
            _laserFactory = laserFactory;
            _input = input;
            _playerSignalsHandler = playerSignalsHandler;
            _laserSignalsHandler = laserSignalsHandler;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<EdgeCollider2D>();
            PlayerDied += _playerSignalsHandler.OnPlayerDied;
            _moverSystem = new MoverSystem(_config, _input, _rb, transform);
            _moverSystem.PlayerMoved += _playerSignalsHandler.OnPlayerMoved;
            _steeringSystem = new SteeringSystem(_config, _input, transform);
            _steeringSystem.PlayerSteered += _playerSignalsHandler.OnPlayerSteered;
            _teleportationSystem = new TeleportationSystem(transform);
            _bulletShooting = new BulletShooting(_config, _input, transform, _bulletFactory);
            _bulletShooting.Initialize();
            _laserShooting = new LaserShooting(_config, _input, _laserFactory, this);
            _laserShooting.Initialize();
            _laserShooting.LaserAmmunitionChanged += _laserSignalsHandler.OnLaserAmmunitionChanged;
            _laserShooting.LaserCooldownChanged += _laserSignalsHandler.OnLaserCooldownChanged;
        }

        private void FixedUpdate()
        {
            _moverSystem.FixedTick();
            _steeringSystem.FixedTick();
            _teleportationSystem.FixedTick();
            _bulletShooting.FixedTick();
            _laserShooting.FixedTick();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            PlayerDied?.Invoke();
            Destroy(gameObject);
        }

        private void OnDisable()
        {
             PlayerDied -= _playerSignalsHandler.OnPlayerDied;
            _bulletShooting.Dispose();
            _laserShooting.Dispose();
            _moverSystem.PlayerMoved -= _playerSignalsHandler.OnPlayerMoved;
            _steeringSystem.PlayerSteered -= _playerSignalsHandler.OnPlayerSteered;
            _laserShooting.LaserAmmunitionChanged -= _laserSignalsHandler.OnLaserAmmunitionChanged;
            _laserShooting.LaserCooldownChanged -= _laserSignalsHandler.OnLaserCooldownChanged;
        }
    }
}