using Develop.Runtime.Meta.EventSignals;
using Zenject;

namespace Develop.Runtime.Installers.CoreContext
{
    public sealed class EventSignalsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerSignals>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AsteroidSignals>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LaserSignals>().AsSingle().NonLazy();
        }
    }
}