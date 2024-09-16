using System;
using Codebase.Data;
using Codebase.Logic.Gameplay.Bubbles.Components;
using Codebase.Logic.Gameplay.Bubbles.Data.Abstract;
using Codebase.Logic.Gameplay.Bubbles.Factories;
using Codebase.Logic.Gameplay.Bubbles.Strategies.Implementations;

namespace Codebase.Logic.Gameplay.Shooting.Handlers.Implementations
{
    public class ReloadHandler : IReloadHandler
    {
        private readonly IAimHandler _aimHandler;
        private readonly IBubbleFactory _bubbleFactory;
        private readonly IBubbleStrategyFactory _strategyFactory;
        private readonly IBubbleDataSource _bubbleDataSource;
        public event Action Reloaded;
        public event Action<BubbleData> NextPicked;

        public BubbleData Next { get; private set; }

        public ReloadHandler(IAimHandler aimHandler, IBubbleFactory bubbleFactory,
            IBubbleStrategyFactory strategyFactory, IBubbleDataSource bubbleDataSource)
        {
            _aimHandler = aimHandler;
            _bubbleFactory = bubbleFactory;
            _strategyFactory = strategyFactory;
            _bubbleDataSource = bubbleDataSource;

            Next = _bubbleDataSource.GetRandom();
        }

        public void Handle()
        {
            var bubble = _bubbleFactory.CreateBubble(Next);
            var strategy = _strategyFactory.CreateStrategy<LoadedBubbleStrategy>();
            
            bubble.ApplyStrategy(strategy);
            
            _aimHandler.SetBubble(bubble.Component.GetComponent<LoadedBubbleComponent>());
            Reloaded?.Invoke();

            Next = _bubbleDataSource.GetRandom();
            NextPicked?.Invoke(Next);
        }
    }
}