using Develop.Runtime.Infrastructure.Factories;
using Zenject;

namespace Develop.Runtime.Installers.CoreContext
{
    public sealed class AsteroidFactoryInstaller: MonoInstaller
    {
        public override void InstallBindings() =>
            Container.Bind<AsteroidsFactory>().AsSingle().NonLazy();
    }
}