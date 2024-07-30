using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Develop.Runtime.Core.Spawn;
using Develop.Runtime.Meta.EventSignals;
using Develop.Runtime.Services.AssetManagement;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Infrastructure.Factories
{
    public sealed class AsteroidsFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _container;
        private readonly IPlayerSignalsHandler _playerSignalsHandler;
        private readonly IAsteroidSignalsHandler _asteroidSignalsHandler;
        private Transform _parent;

        public AsteroidsFactory(DiContainer container, IAssetProvider assetProvider,
            IPlayerSignalsHandler playerSignalsHandler, IAsteroidSignalsHandler asteroidSignalsHandler)
        {
            _container = container;
            _assetProvider = assetProvider;
            _playerSignalsHandler = playerSignalsHandler;
            _asteroidSignalsHandler = asteroidSignalsHandler;
        }

        public async Task PrepareAll<T>(Dictionary<T, string> prefabs) where T : Enum
        {
            foreach (var prefab in prefabs)
                await Prepare(prefab.Value);
        }

        public void CreateRoot() =>
            _parent = new GameObject("[Asteroids]").transform;

        public async Task<Asteroid> Create(string prefabKey, Vector2 position, Quaternion rotation)
        {
            var prefab = await _assetProvider.Load<GameObject>(key: prefabKey);
            var asteroid = _container.InstantiatePrefabForComponent<Asteroid>(prefab, position, rotation, _parent);

            asteroid.PlayerDied += _playerSignalsHandler.OnPlayerDied;
            asteroid.AsteroidDied += _asteroidSignalsHandler.OnAsteroidDied;

            return asteroid;
        }

        public void ClearAll<T>(Dictionary<T, string> prefabs) where T : Enum
        {
            foreach (var prefab in prefabs)
                Clear(prefab.Value);
        }

        private async Task Prepare(string prefabKey) =>
            await _assetProvider.Load<GameObject>(key: prefabKey);

        private void Clear(string prefabKey) =>
            _assetProvider.Release(key: prefabKey);
    }
}