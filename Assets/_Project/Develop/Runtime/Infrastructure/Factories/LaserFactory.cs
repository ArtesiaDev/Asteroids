using System.Threading.Tasks;
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

        public LaserFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }
        
        public async Task Prepare() =>
            await _assetProvider.Load<GameObject>(key: Laser);
    
        public async Task<Laser> Create(Vector3 position, Quaternion rotation)
        {
            var prefab = await _assetProvider.Load<GameObject>(key: Laser);
            return _container.InstantiatePrefabForComponent<Laser>(prefab, position, rotation, null);
        }
        
        public void Clear() =>
            _assetProvider.Release(key: Laser);
    }
}