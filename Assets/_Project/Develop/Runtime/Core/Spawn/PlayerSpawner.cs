using Develop.Runtime.Core.Configs;
using Develop.Runtime.Infrastructure.Factories;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Core.Spawn
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _config;
        private PlayerFactory _factory;

        [Inject]
        private void Construct(PlayerFactory factory)
        {
            _factory = factory;
        }

        public GameObject PlayerPrefab { get; private set; }
        public Rigidbody2D PlayerRb { get; private set; }

        private async void Awake()
        {
            await _factory.Prepare();
            SpawnPlayer();
        }

        private void OnDisable()
        {
            _factory.Clear();
        }

        public async void SpawnPlayer()
        {
            ClearPosition(_config.SpawnPoint, _config.ClearRadius);
            PlayerPrefab = (await _factory.Create(_config.SpawnPoint, Quaternion.identity)).gameObject;
            PlayerRb = PlayerPrefab.GetComponent<Rigidbody2D>();
        }

        private void ClearPosition(Vector2 position, float spawnRadius)
        {
            var colliders = Physics2D.OverlapCircleAll(position, spawnRadius);
            if (colliders == null) return;

            foreach (var col in colliders)
            {
                Destroy(col.gameObject);
            }
        }
    }
}