using Codebase.Logic.Gameplay.Bubbles.Services;
using DG.Tweening;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Bubbles.Strategies.Implementations
{
    public class DestroyedBubbleStrategy : IBubbleStrategy
    {
        private readonly IBubblePool _bubblePool;

        public DestroyedBubbleStrategy(IBubblePool bubblePool)
        {
            _bubblePool = bubblePool;
        }
        
        public void Apply(Bubble bubble)
        {
            var rigidBody = bubble.Component.GetComponent<Rigidbody2D>();
            Object.Destroy(rigidBody);
            
            bubble.GameObject?.transform.DOScale(Vector3.zero, 0.1f)
                .OnComplete(() => OnComplete(bubble));
        }

        public void Remove(Bubble bubble)
        {
            bubble.GameObject.transform.localScale = Vector3.one;
        }

        private void OnComplete(Bubble bubble)
        {
            _bubblePool.Add(bubble);
        }
    }
}