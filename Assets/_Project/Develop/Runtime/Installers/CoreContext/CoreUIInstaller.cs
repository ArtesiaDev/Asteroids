using Develop.Runtime.Meta.Core;
using UnityEngine;
using Zenject;

namespace Develop.Runtime.Installers.CoreContext
{
    public sealed class CoreUIInstaller: MonoInstaller
    {
        [SerializeField] private StatsPanelView _statsPanelView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private EndPanelView _endPanelView;
        public override void InstallBindings()
        {
            Container.Bind<StatsPanelView>().FromInstance(_statsPanelView).AsSingle();
            Container.BindInterfacesAndSelfTo<StatsPanelController>().AsSingle();
            Container.Bind<ScoreView>().FromInstance(_scoreView).AsSingle();
            Container.BindInterfacesAndSelfTo<ScoreController>().AsSingle();
            Container.Bind<EndPanelView>().FromInstance(_endPanelView).AsSingle();
            Container.BindInterfacesAndSelfTo<EndPanelPresenter>().AsSingle();
            Container.Bind<CoreUIModel>().AsSingle();
        }
    }
}