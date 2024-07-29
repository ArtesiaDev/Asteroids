using Develop.Runtime.Core.Spawn;
using Develop.Runtime.Meta;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Installers.CoreContext
{
    public sealed class MonobehInstaller : MonoInstaller
    {
        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        [SerializeField] private Score _score;
        
        public override void InstallBindings()
        {
            Container.Bind<AsteroidSpawner>().FromInstance(_asteroidSpawner).AsSingle();
            Container.BindInterfacesAndSelfTo<Score>().FromInstance(_score).AsSingle();
        }
    }
    
}