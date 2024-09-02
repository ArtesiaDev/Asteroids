using Develop.Runtime.Core.Spawn;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Installers.CoreContext
{
    public sealed class GameObjInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidSpawner _asteroidSpawner; 
        [SerializeField] private PlayerSpawner _playerSpawner;
        
        public override void InstallBindings()
        {
            Container.Bind<AsteroidSpawner>().FromInstance(_asteroidSpawner).AsSingle();
            Container.Bind<PlayerSpawner>().FromInstance(_playerSpawner).AsSingle();
        }
    }
    
}