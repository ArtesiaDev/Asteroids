using Develop.Runtime.Infrastructure.Factories;
using Zenject;

namespace Develop.Runtime.Installers.CoreContext
{
    public sealed class SceneFactoriesInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AsteroidsFactory>().AsSingle().NonLazy();
            Container.Bind<LaserFactory>().AsSingle().NonLazy(); 
            Container.Bind<BulletFactory>().AsSingle().NonLazy(); 
        }
    }
}