using System.Threading.Tasks;
using Develop.Runtime.Core.ShootingObjects;
using Develop.Runtime.Services.AssetManagement;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Infrastructure.Factories
{
    public sealed class BulletFactory
    {
        private const string BULLET = "Bullet";
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _container;
        private Transform _parent;

        public BulletFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }
        
        public async Task Prepare() =>
            await _assetProvider.Load<GameObject>(key: BULLET);

        public void CreateRoot() =>
            _parent = new GameObject("[Bullets]").transform;
    
        public async Task<Bullet> Create(Vector3 position, Quaternion rotation)
        {
            var prefab = await _assetProvider.Load<GameObject>(key: BULLET);
            return _container.InstantiatePrefabForComponent<Bullet>(prefab, position, rotation, _parent);
        }
        
        public void Clear() =>
            _assetProvider.Release(key: BULLET);
    }
}