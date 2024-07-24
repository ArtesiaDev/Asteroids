using Develop.Runtime.Infrastructure.Factories;
using Zenject;

namespace Develop.Runtime.Installers.ProjectContext
{
    public sealed class FactoriesInstaller: MonoInstaller
    {
        public override void InstallBindings() =>
            BindFactories();

        private void BindFactories()
        {
            Container.Bind<StateFactory>().AsSingle().NonLazy();
        }
    }
}