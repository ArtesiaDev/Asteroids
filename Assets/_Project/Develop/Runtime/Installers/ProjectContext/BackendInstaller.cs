using Develop.Backend;
using Zenject;

namespace Develop.Runtime.Installers.ProjectContext
{
    public class BackendInstaller: MonoInstaller
    {
        public override void InstallBindings() =>
            Container.BindInterfacesTo<FirebaseAnalyticsService>().AsSingle();
    }
}