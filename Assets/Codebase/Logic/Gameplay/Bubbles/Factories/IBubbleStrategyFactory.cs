using Codebase.Logic.Gameplay.Bubbles.Strategies;

namespace Codebase.Logic.Gameplay.Bubbles.Factories
{
    public interface IBubbleStrategyFactory
    {
        public IBubbleStrategy CreateStrategy<TStrategy>() where TStrategy : IBubbleStrategy;
    }
}