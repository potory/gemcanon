using Codebase.Data;
using Codebase.Infrastructure.Abstract;
using Codebase.Logic.Gameplay.Bubbles.Components;
using Codebase.Logic.Gameplay.Bubbles.Services;
using Zenject;

namespace Codebase.Logic.Gameplay.Bubbles.Factories.Implementations
{
    public class BubbleFactory : IBubbleFactory
    {
        private const string ResourcePath = "Prefabs/Shooter/Bubble";
        
        private readonly IBubblePool _bubblePool;
        private readonly DiContainer _container;

        private readonly BaseBubbleComponent _prefab;

        public BubbleFactory(IAssetProvider assetProvider, IBubblePool bubblePool, DiContainer container)
        {
            _bubblePool = bubblePool;
            _container = container;
            
            _prefab = assetProvider.GetAsset<BaseBubbleComponent>(ResourcePath);
        }

        public Bubble CreateBubble(BubbleData data)
        {
            if (!_bubblePool.TryGetFree(out var bubble))
                bubble = CreateNewBubble(data);
            else
                bubble.SetBubbleType(data.BubbleTypeId);
            
            bubble.Component.SetSprite(data.Sprite);
            return bubble;
        }

        private Bubble CreateNewBubble(BubbleData data)
        {
            var gameObject = _container.InstantiatePrefab(_prefab);
            var component = gameObject.GetComponent<BaseBubbleComponent>();

            return new Bubble(data.BubbleTypeId, component);
        }
    }
}