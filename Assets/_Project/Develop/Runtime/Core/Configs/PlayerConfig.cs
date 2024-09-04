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
            
            SteeringSpeed = config.Value<int>(nameof(SteeringSpeed));

            MoveThrustPower = config.Value<int>(nameof(MoveThrustPower));
            MaxSpeed = config.Value<int>(nameof(MaxSpeed));

            BulletFireRate = config.Value<int>(nameof(BulletFireRate));
            BulletOffsetCoefficient = config.Value<int>(nameof(BulletOffsetCoefficient));
            BulletSpeed = config.Value<int>(nameof(BulletSpeed));

            LaserCooldown = config.Value<int>(nameof(LaserCooldown));
            LaserOffsetCoefficient = config.Value<int>(nameof(LaserOffsetCoefficient));
            LaserAmmunition = config.Value<int>(nameof(LaserAmmunition));
            LaserLifeTime = config.Value<int>(nameof(LaserLifeTime));
            LaserReloadTime = config.Value<int>(nameof(LaserReloadTime));

            SpawnPoint = new Vector2(config.Value<float>($"{nameof(SpawnPoint)}.x"),
                config.Value<float>($"{nameof(SpawnPoint)}.y"));
            SpawnClearRadius = config.Value<int>(nameof(SpawnClearRadius));
        }
    }
}