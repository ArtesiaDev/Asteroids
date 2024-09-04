using System.Threading.Tasks;
using Develop.Backend.Analytics;
using Develop.Runtime.Services.AssetManagement;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Infrastructure.Factories
{
    public sealed class GameAnalyticsFactory
    {
        private const string GAME_ANALYTICS = "GameAnalytics";
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _container;

        public GameAnalyticsFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        public async Task Create()
        {
            var prefab = await _assetProvider.Load<GameObject>(key: GAME_ANALYTICS);
            var script = _container.InstantiatePrefabForComponent<GameAnalyticsInitialize>(prefab, Vector3.zero, Quaternion.identity, null);
            Object.DontDestroyOnLoad(script.gameObject);
        }

        public void Clear() =>
            _assetProvider.Release(key: GAME_ANALYTICS);
    }
}