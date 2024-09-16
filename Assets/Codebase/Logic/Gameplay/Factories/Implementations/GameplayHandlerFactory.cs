using Codebase.Logic.Gameplay.Handlers;
using Codebase.Logic.Gameplay.Shooting.Handlers;
using Zenject;

namespace Codebase.Logic.Gameplay.Factories.Implementations
{
    public class GameplayHandlerFactory : IGameplayHandlerFactory
    {
        private readonly DiContainer _container;

        public GameplayHandlerFactory(DiContainer container) => 
            _container = container;

        public IAimHandler CreateAimHandler() => _container.Resolve<IAimHandler>();
        public IMovingHandler CreateMovingHandler() => _container.Resolve<IMovingHandler>();
        public IReloadHandler CreateReloadHandler() => _container.Resolve<IReloadHandler>();
        public IHitHandler CreateHitHandler() => _container.Resolve<IHitHandler>();
        public IHintsHandler CreateHintsHandler() => _container.Resolve<IHintsHandler>();
        public ILaunchHandler CreateLaunchHandler() => _container.Resolve<ILaunchHandler>();
        public IGameOverHandler CreateGameOverHandler() => _container.Resolve<IGameOverHandler>();
    }
}