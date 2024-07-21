using Develop.Runtime.Infrastructure.GameStateMachine.States;
using Zenject;

namespace Develop.Runtime.Infrastructure.Factories
{
    public sealed class StateFactory
    {
        private readonly DiContainer _container;

        public StateFactory(DiContainer container)
        {
            _container = container;
        }
        public T Create<T>() where T : IState
        {
            return _container.Resolve<T>();
        }
    }
}