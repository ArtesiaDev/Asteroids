using System;
using GameAnalyticsSDK;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Configs
{
    public class PlayerConfig : IInitializable, IDisposable, ISteeringConfig, IMoveConfig, IBulletShootingConfig,
        ILaserShootingConfig, IPlayerSpawnConfig
    {
        private const string PLAYER_CONFIG = "PlayerConfig";

        public float SteeringSpeed { get; private set; } = 200f;

        public float MoveThrustPower { get; private set; } = 1.5f;
        public float MaxSpeed { get; private set; } = 10f;

        public float BulletFireRate { get; private set; } = 0.3f;
        [field: Range(0.8f, 10f)] public float BulletOffsetCoefficient { get; private set; } = 0.8f;
        public float BulletSpeed { get; private set; } = 10f;

        public float LaserCooldown { get; private set; } = 5f;
        [field: Range(0.8f, 10f)] public float LaserOffsetCoefficient { get; private set; } = 0.8f;
        public int LaserAmmunition { get; private set; } = 2;
        public float LaserLifeTime { get; private set; } = 0.1f;
        public float LaserReloadTime { get; private set; } = 10f;

        public Vector2 SpawnPoint { get; private set; }
        public float SpawnClearRadius { get; private set; } = 1f;

        public void Initialize()
        {
            ConfigsUpdate();
            GameAnalytics.OnRemoteConfigsUpdatedEvent += ConfigsUpdate;
        }

        public void Dispose() =>
            GameAnalytics.OnRemoteConfigsUpdatedEvent -= ConfigsUpdate;

        private void ConfigsUpdate()
        {
            var jsonConfig = GameAnalytics.GetRemoteConfigsValueAsString(PLAYER_CONFIG);
            if (jsonConfig == null) return;

            var config = JObject.Parse(jsonConfig);

            SteeringSpeed = (float)config[nameof(SteeringSpeed)];

            MoveThrustPower = (float)config[nameof(MoveThrustPower)];
            MaxSpeed = (float)config[nameof(MaxSpeed)];

            BulletFireRate = (float)config[nameof(BulletFireRate)];
            BulletFireRate = (float)config[nameof(BulletFireRate)];
            BulletOffsetCoefficient = (float)config[nameof(BulletOffsetCoefficient)];
            BulletSpeed = (float)config[nameof(BulletSpeed)];

            LaserCooldown = (float)config[nameof(LaserCooldown)];
            LaserOffsetCoefficient = (float)config[nameof(LaserOffsetCoefficient)];
            LaserAmmunition = (int)config[nameof(LaserAmmunition)];
            LaserLifeTime = (float)config[nameof(LaserLifeTime)];
            LaserReloadTime = (float)config[nameof(LaserReloadTime)];

            SpawnPoint = new Vector2((float)config[nameof(SpawnPoint)]?[nameof(Vector2.x)],
                (float)config[nameof(SpawnPoint)]?[nameof(Vector2.y)]);
            SpawnClearRadius = (float)config[nameof(SpawnPoint)];
        }
    }
}