using Codebase.Logic.States.Abstract;
using Zenject;

namespace Codebase.Infrastructure.Game
{
    public class GameStateFactory
    {
        private readonly DiContainer _dependencies;
        
        public GameStateFactory(DiContainer dependencies) => 
            _dependencies = dependencies;

        public TState CreateState<TState>() where TState : IExitableState 
            => _dependencies.Instantiate<TState>();
    }
}