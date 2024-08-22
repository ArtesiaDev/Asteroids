using Develop.Runtime.Meta;
using Develop.Runtime.Meta.Menu;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Installers.MenuContext
{
    public sealed class UIInstaller : MonoInstaller
    {
        [SerializeField] private MenuUIView _menuUIView;
        public override void InstallBindings()
        {
            Container.Bind<MenuUIPresenter>().AsSingle().NonLazy();
            Container.Bind<MenuUIView>().FromInstance(_menuUIView).AsSingle().NonLazy();
        }
    }
}