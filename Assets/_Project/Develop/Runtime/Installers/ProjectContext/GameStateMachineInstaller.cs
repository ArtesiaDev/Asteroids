﻿using Develop.Runtime.Infrastructure.GameStateMachine;
using Develop.Runtime.Infrastructure.GameStateMachine.States;
using Zenject;

namespace Develop.Runtime.Installers.ProjectContext
{
    public sealed class GameStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            BindGameStateMachine();

        private void BindGameStateMachine()
        {
            Container.BindInterfacesTo<GameStateMachine>().AsSingle().NonLazy();
            
            Container.Bind<MenuState>().AsSingle().NonLazy();
            Container.Bind<LoadLevelState>().AsSingle().NonLazy();
            Container.Bind<CoreState>().AsSingle().NonLazy();
        }
    }
}