using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codebase.Infrastructure.Abstract;
using Codebase.Infrastructure.Exceptions;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Bubbles.Factories;
using Codebase.Logic.Gameplay.Bubbles.Strategies.Implementations;
using Codebase.Logic.Gameplay.Field;
using Codebase.Logic.Gameplay.Field.Implementations;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Logic.Gameplay.Shooting.Handlers.Implementations
{
    public class HitHandler : IHitHandler
    {
        private readonly ITargetField _targetField;
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly TargetChainHandler _nodeChainHandler = new();
        
        private readonly IBubbleStrategyFactory _strategyFactory;

        public event Action<Bubble> Destroyed;

        public HitHandler(ICoroutineRunner coroutineRunner, ITargetField targetField, 
            IBubbleStrategyFactory strategyFactory)
        {
            _coroutineRunner = coroutineRunner;
            _targetField = targetField;

            _strategyFactory = strategyFactory;
        }

        public event Action Complete;

        public void Handle(TargetNode target, Impact impact, Bubble bubble)
        {
            _coroutineRunner.StartCoroutine(HandleShotRoutine(target, impact, bubble));
        }

        private IEnumerator HandleShotRoutine(TargetNode target, Impact impact, Bubble bubble)
        {
            if (target != TargetNode.Empty)
                yield return HandleHit(target, impact, bubble);
            else
                HandleMiss(bubble);
            
            Complete?.Invoke();
        }

        private IEnumerator HandleHit(TargetNode target, Impact impact, Bubble bubble)
        {
            if (impact == Impact.Standard)
            {
                if (!target.TryFindEmptyPositionAround(bubble.Position, out var position))
                    throw new InvalidTrajectoryException();
    
                bubble.SetPosition(position.Value);
                ShakeCamera(0.25f);
            }
            else
            {
                var position = target.Bubble.Position;
                
                DestroyNode(target);
                DestroyObject(target);

                bubble.SetPosition(position);
                ShakeCamera(2);
            }
                
            var node = _targetField.Add(bubble);
            var chain = _nodeChainHandler.Create(node);
            
            if (chain.Count < 3)
            {
                yield return ShakeNeighbours(node);
                yield break;
            }
            
            yield return DestroyChain(chain);
            RemoveIsolated();
        }

        private static void HandleMiss(Bubble bubble)
        {
            Object.Destroy(bubble.GameObject);
        }

        private IEnumerator DestroyChain(IReadOnlyList<TargetNode> chain)
        {
            foreach (var currentNode in chain)
            {
                DestroyNode(currentNode);
            }
            
            foreach (var currentNode in chain)
            {
                yield return DestroyObject(currentNode);
            }
        }

        private object DestroyObject(TargetNode bubbleNode)
        {
            Destroyed?.Invoke(bubbleNode.Bubble);
            bubbleNode.Bubble.ApplyStrategy(_strategyFactory.CreateStrategy<DestroyedBubbleStrategy>());

            return new WaitForSeconds(0.05f);
        }

        private void DestroyNode(TargetNode currentNode)
        {
            foreach (var node in _targetField.Nodes)
            {
                foreach (var direction in TargetNode.Directions)
                {
                    if (node.Neighbours[direction] == currentNode) 
                        node.Neighbours[direction] = TargetNode.Empty;
                }
            }

            _targetField.Remove(currentNode);
        }

        private void RemoveIsolated()
        {
            for (var index = _targetField.Nodes.Count - 1; index >= 0; index--)
            {
                var currentNode = _targetField.Nodes[index];
                
                if (!currentNode.IsIsolated)
                    continue;

                Debug.Log($"<b>Isolated</b>: {currentNode.Bubble.GameObject}");

                currentNode.Bubble.ApplyStrategy(_strategyFactory.CreateStrategy<FallingBubbleStrategy>());
                Destroyed?.Invoke(currentNode.Bubble);

                _targetField.Remove(currentNode);
            }
        }

        private static IEnumerator ShakeNeighbours(TargetNode node)
        {
            const float maxFrequency = 15;
            const float maxVelocity = 10;

            var neighbours = node.Neighbours
                .Select(pair => pair.Value).ToArray();

            for (var index = 0; index < TargetNode.Directions.Count; index++)
            {
                var neighbour = neighbours[index];
                
                if (neighbour == TargetNode.Empty)
                    continue;
                
                var direction = TargetNode.Directions[index];

                neighbour.Component.SetFrequency(maxFrequency);
                neighbour.Component.SetVelocity(direction * maxVelocity);
            }

            float t = 0;

            while (t < 1)
            {
                foreach (var neighbour in neighbours)
                {
                    if (neighbour == TargetNode.Empty)
                        continue;

                    neighbour.Component.SetFrequency(maxFrequency - t * maxFrequency);
                }

                yield return null;
                t += Time.deltaTime;
            }
            
            foreach (var neighbour in neighbours)
            {
                if (neighbour == TargetNode.Empty)
                    continue;
                
                neighbour.Component.SetFrequency(0);
            }
        }

        private static void ShakeCamera(float strength)
        {
            Camera.main!.DOShakeRotation(0.5f, strength);
        }
    }
}