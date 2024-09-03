using System.Threading.Tasks;
using Develop.Runtime.Core.Starship;
using Develop.Runtime.Services.AssetManagement;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Infrastructure.Factories
{
    public sealed class PlayerFactory
    {
        private const string PLAYER = "Player";
        private readonly IAssetProvider _assetProvider;
        private readonly DiContainer _container;

        public PlayerFactory(DiContainer container, IAssetProvider assetProvider)
        {
            _container = container;
            _assetProvider = assetProvider;
        }

        public async Task Prepare() =>
            await _assetProvider.Load<GameObject>(key: PLAYER);


        public async Task<Starship> Create(Vector3 position, Quaternion rotation)
        {
            var prefab = await _assetProvider.Load<GameObject>(key: PLAYER);
            return _container.InstantiatePrefabForComponent<Starship>(prefab, position, rotation, null);
        }

        public void Clear() =>
            _assetProvider.Release(key: PLAYER);
    }
}