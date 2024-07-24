using Develop.Runtime.Infrastructure.Factories;
using Zenject;

namespace Develop.Runtime.Installers
{
    public sealed class LaserFactoryInstaller: MonoInstaller
    {
        public override void InstallBindings() =>
            Container.Bind<LaserFactory>().AsSingle().NonLazy(); 
    }
}