using Develop.Runtime.Core.Spawn;
using Develop.Runtime.Meta.Core;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Installers.CoreContext
{
    public sealed class MonobehInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        
        public override void InstallBindings()
        {
            Container.Bind<AsteroidSpawner>().FromInstance(_asteroidSpawner).AsSingle();
        }
    }
    
}