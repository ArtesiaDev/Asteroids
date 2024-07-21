using Develop.Runtime.Services.AssetManagement;
using Develop.Runtime.Services.SceneLoader;
using Zenject;

namespace Develop.Runtime.Installers
{
    public sealed class ServicesInstaller: MonoInstaller
    {
        public override void InstallBindings() =>
            BindServices();

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle().NonLazy();
            Container.Bind<ISceneLoader>().To<AsyncSceneLoader>().AsSingle();
        }
    }
}