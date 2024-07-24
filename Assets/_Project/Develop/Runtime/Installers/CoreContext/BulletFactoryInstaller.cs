using Develop.Runtime.Infrastructure.Factories;
using Zenject;

namespace Develop.Runtime.Installers.CoreContext
{
    public sealed class BulletFactoryInstaller: MonoInstaller
    {
        public override void InstallBindings() =>
            Container.Bind<BulletFactory>().AsSingle().NonLazy(); 
    }
}