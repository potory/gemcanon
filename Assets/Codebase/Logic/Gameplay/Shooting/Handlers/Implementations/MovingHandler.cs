using System;
using System.Collections;
using Codebase.Infrastructure.Abstract;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Bubbles.Factories;
using Codebase.Logic.Gameplay.Bubbles.Strategies;
using Codebase.Logic.Gameplay.Bubbles.Strategies.Implementations;
using Codebase.Logic.Gameplay.Field;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting.Handlers.Implementations
{
    public class MovingHandler : IMovingHandler
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IBubbleStrategy _strategy;

        public event Action<TargetNode, Impact, Bubble> ReachedDesignation;

        public MovingHandler(ICoroutineRunner coroutineRunner, IBubbleStrategyFactory strategyFactory)
        {
            _coroutineRunner = coroutineRunner;
            _strategy = strategyFactory.CreateStrategy<FlyingBubbleStrategy>();
        }
        
        public void Handle(Trajectory trajectory, Impact impact, Bubble bubble)
        {
            bubble.ApplyStrategy(_strategy);
            _coroutineRunner.StartCoroutine(MovingRoutine(trajectory, impact, bubble));
        }
        
        private IEnumerator MovingRoutine(Trajectory trajectory, Impact impact, Bubble bubble)
        {
            var transform = bubble.GameObject.transform;
            yield return PerformMovement(trajectory, transform);
            
            ReachedDesignation?.Invoke(trajectory.Target, impact, bubble);
        }

        private static IEnumerator PerformMovement(Trajectory trajectory, Transform transform)
        {
            float t = 0;

            while (t < trajectory.Equation.LimitT)
            {
                var position = trajectory.Equation.Evaluate(t);
                transform.position = position;

                t += Time.deltaTime;
                yield return null;
            }
        }
    }
}