using Develop.Backend.Ads;
using Zenject;

namespace Develop.Runtime.Installers.ProjectContext
{
    public class BackendInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IronSourceAdsInitialize>().AsSingle().NonLazy();
            Container.BindInterfacesTo<IronSourceAdsService>().AsSingle();
            
            Container.BindInterfacesTo<BannerCallbacks>().AsSingle();
            Container.BindInterfacesTo<InterstitialCallbacks>().AsSingle();
            Container.BindInterfacesTo<RewardedCallbacks>().AsSingle();
        }
    }
}