using System.Threading.Tasks;
using Develop.Backend;
using Develop.Runtime.Core.ShootingObjects;
using Develop.Runtime.Services.AssetManagement;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Infrastructure.Factories
{
    public sealed class LaserFactory
    {
        private const string Laser = "Laser";
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _container;
        private readonly IAnalyticsService _analyticsService;

        public LaserFactory(DiContainer container, IAssetProvider assetProvider, IAnalyticsService analyticsService)
        {
            _container = container;
            _assetProvider = assetProvider;
            _analyticsService = analyticsService;
        }

        public async Task Prepare() =>
            await _assetProvider.Load<GameObject>(key: Laser);

        public async Task<Laser> Create(Vector3 position, Quaternion rotation)
        {
            var prefab = await _assetProvider.Load<GameObject>(key: Laser);
            _analyticsService.LogLaserUsed();
            return _container.InstantiatePrefabForComponent<Laser>(prefab, position, rotation, null);
        }

        public void Clear() =>
            _assetProvider.Release(key: Laser);
    }
}