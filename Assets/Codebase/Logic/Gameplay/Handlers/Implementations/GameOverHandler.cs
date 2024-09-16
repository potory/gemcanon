using System;
using Codebase.Logic.Gameplay.Bubbles.Factories;
using Codebase.Logic.Gameplay.Bubbles.Strategies.Implementations;
using Codebase.Logic.Gameplay.Field;

namespace Codebase.Logic.Gameplay.Handlers.Implementations
{
    public class GameOverHandler : IGameOverHandler
    {
        private readonly ITargetField _targetField;
        private readonly IBubbleStrategyFactory _strategyFactory;

        public event Action<GameResult> GameOver;

        public GameOverHandler(ITargetField targetField, IBubbleStrategyFactory strategyFactory)
        {
            _targetField = targetField;
            _strategyFactory = strategyFactory;
        }

        public void HandleWin()
        {
            DestroyTargetField();
            GameOver?.Invoke(GameResult.Win);
        }

        public void HandleLoose()
        {
            DestroyTargetField();
            GameOver?.Invoke(GameResult.Loose);
        }

        private void DestroyTargetField()
        {
            foreach (var node in _targetField.Nodes)
            {
                node.Bubble.ApplyStrategy(
                    _strategyFactory.CreateStrategy<FallingBubbleStrategy>());
            }
        }
    }
}