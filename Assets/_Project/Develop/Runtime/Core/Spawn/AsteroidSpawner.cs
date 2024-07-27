using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Develop.Runtime.Core.Spawn
{
    public class AsteroidSpawner : Spawner
    {
        [SerializeField] private EnemyConfig _config;

        private Dictionary<AsteroidTypes, string> _asteroids;
        private AsteroidsFactory _factory;
        private Camera _camera;

        [Inject]
        private void Construct(AsteroidsFactory factory)
        {
            _factory = factory;
        }

        public async Task <Asteroid> CreateAsteroid(AsteroidTypes aster, Vector2 spawnPosition) => 
            await _factory.Create(_asteroids[aster], spawnPosition, Quaternion.identity);

        private async void Awake()
        {
            _asteroids = InitializeAsteroids();
            _camera = Camera.main;
            _factory.CreateRoot();
            await _factory.PrepareAll(_asteroids);
            StartCoroutine(SpawnMeteorRoutine());
        }

        private void OnDisable()
        {
            _factory.ClearAll(_asteroids);
            StopAllCoroutines();
        }

        private IEnumerator SpawnMeteorRoutine()
        {
            while (true)
            {
                SpawnMeteor();
                yield return new WaitForSeconds(_config.SpawnInterval);
            }
        }

        private async void SpawnMeteor()
        {
            while (true)
            {
                var spawnPosition = GetRandomPositionInView();
                
                var aster = ChooseAsteroidType();

                var spawnRadius = SetSpawnRadius((int)aster);

                if (IsPositionFree(spawnPosition, spawnRadius))
                {
                    await CreateAsteroid(aster, spawnPosition);
                    break;
                }
            }
        }

        private Vector2 GetRandomPositionInView()
        {
            var borderX = Screen.width * _config.SpawnBorderX / 100;
            var borderY = Screen.height * _config.SpawnBorderY / 100;
            
            var pos = new Vector3(Random.Range(borderX, Screen.width - borderX), Random.Range(borderY, Screen.height - borderY));
            return _camera.ScreenToWorldPoint(pos);
        }

        private AsteroidTypes ChooseAsteroidType() 
            => (AsteroidTypes)Random.Range(1, 10);

        private float SetSpawnRadius(int prefab)
        {
            switch (prefab)
            {
                case 1: case 2: case 3: return _config.HugeSpawnRadius; 
                case 4: case 5: case 6: return _config.MedSpawnRadius; 
                case 7: case 8: case 9: return _config.SmallSpawnRadius;
                default: throw new Exception("Unknown type");
            }
        }

        private bool IsPositionFree(Vector2 position, float spawnRadius)
        {
            var colliders = Physics2D.OverlapCircle(position, spawnRadius);
            return colliders == null;
        }
    }
}