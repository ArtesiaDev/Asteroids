using System;
using System.Collections;
using System.Collections.Generic;
using Develop.Runtime.Core.Configs;
using Develop.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Develop.Runtime.Core.Spawn
{
    public class AsteroidSpawner : MonoBehaviour
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

        private async void Awake()
        {
            InitializeAsteroids();
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
                    await _factory.Create(_asteroids[aster], spawnPosition, Quaternion.identity);
                    break;
                }
            }
        }

        private void InitializeAsteroids()
        {
            _asteroids = new Dictionary<AsteroidTypes, string>()
            {
                { AsteroidTypes.AsterHuge1, "AsterHuge1" },
                { AsteroidTypes.AsterHuge2, "AsterHuge2" },
                { AsteroidTypes.AsterHuge3, "AsterHuge3" },
                { AsteroidTypes.AsterMed1, "AsterMed1" },
                { AsteroidTypes.AsterMed2, "AsterMed2" },
                { AsteroidTypes.AsterMed3, "AsterMed3" },
                { AsteroidTypes.AsterSmall1, "AsterSmall1" },
                { AsteroidTypes.AsterSmall2, "AsterSmall2" },
                { AsteroidTypes.AsterSmall3, "AsterSmall3" },
            };
        }

        private Vector2 GetRandomPositionInView()
        {
            var pos = new Vector3(Random.Range(0f, Screen.width), Random.Range(0f, Screen.height));
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