using Codebase.Logic.Gameplay.Bubbles.Strategies;
using Zenject;

namespace Codebase.Logic.Gameplay.Bubbles.Factories.Implementations
{
    public class BubbleStrategyFactory : IBubbleStrategyFactory
    {
        private readonly DiContainer _container;

        public BubbleStrategyFactory(DiContainer container)
        {
            _container = container;
        }

        public IBubbleStrategy CreateStrategy<TStrategy>() where TStrategy : IBubbleStrategy => 
            _container.Instantiate<TStrategy>();
    }
}