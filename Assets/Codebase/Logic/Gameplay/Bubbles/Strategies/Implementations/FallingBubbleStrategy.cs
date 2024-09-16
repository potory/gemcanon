using System.Collections;
using Codebase.Infrastructure.Abstract;
using Codebase.Logic.Gameplay.Bubbles.Factories;
using Codebase.Logic.Gameplay.Services;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Bubbles.Strategies.Implementations
{
    public class FallingBubbleStrategy : IBubbleStrategy
    {
        private readonly IScreenService _screenService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IBubbleStrategy _destroyStrategy;
        
        private Rigidbody2D _rigidBody;
        private Collider2D _collider;

        public FallingBubbleStrategy(IScreenService screenService, ICoroutineRunner coroutineRunner, 
            IBubbleStrategyFactory factory)
        {
            _screenService = screenService;
            _coroutineRunner = coroutineRunner;
            _destroyStrategy = factory.CreateStrategy<DestroyedBubbleStrategy>();
        }

        public void Apply(Bubble bubble)
        {
            _rigidBody = bubble.Component.GetComponent<Rigidbody2D>();
            _collider = bubble.Component.GetComponent<Collider2D>();
            
            _collider.enabled = false;
            _rigidBody.velocity += new Vector2(1, 1);
            
            _rigidBody.gravityScale = 1;
            _rigidBody.constraints = RigidbodyConstraints2D.None;

            _coroutineRunner.StartCoroutine(WaitForDestroy(bubble));
        }

        private IEnumerator WaitForDestroy(Bubble bubble)
        {
            while (bubble.Position.y > -_screenService.GetScreenWorldSize().y / 2)
            {
                yield return null;
            }

            bubble.ApplyStrategy(_destroyStrategy);
        }

        public void Remove(Bubble bubble)
        {
            _collider.enabled = true;
        }
    }
}