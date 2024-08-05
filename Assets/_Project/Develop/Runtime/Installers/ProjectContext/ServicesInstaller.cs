using Develop.Runtime.Services.AssetManagement;
using Develop.Runtime.Services.Input;
using Develop.Runtime.Services.SceneLoader;
using Zenject;
using Application = UnityEngine.Device.Application;

namespace Develop.Runtime.Installers.ProjectContext
{
    public sealed class ServicesInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetManagement();
            BindSceneLoader();
            BindInput();
        }

        private void BindSceneLoader() =>
            Container.Bind<ISceneLoader>().To<AsyncSceneLoader>().AsSingle();

        private void BindAssetManagement() =>
            Container.BindInterfacesTo<AssetProvider>().AsSingle().NonLazy();

        private void BindInput()
        {
            if (Application.isMobilePlatform)
                Container.BindInterfacesTo<MobileInput>().AsSingle().NonLazy();
            else Container.BindInterfacesTo<StandaloneInput>().AsSingle().NonLazy();
        }
    }
}