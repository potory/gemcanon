using System;
using System.Collections.Generic;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Bubbles.Components;
using Codebase.Logic.Gameplay.Bubbles.Factories;
using Codebase.Logic.Gameplay.Bubbles.Strategies.Implementations;
using Codebase.Logic.Gameplay.Field.Tags;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Gameplay.Field.Implementations
{
    public sealed class TargetField : IInitializable, ITargetField
    {
        private readonly IBubbleStrategyFactory _strategyFactory;
        
        private readonly FieldTargetParentTag _parent;
        private readonly IFieldGenerator _generator;

        private readonly Vector2Int _fieldSize;
        private readonly List<TargetNode> _nodes = new();

        public event Action Created;

        public IReadOnlyList<TargetNode> Nodes => _nodes;
        public Vector3 Origin => _parent.transform.position;

        private Bounds _bounds;
        private int _targetIndex;

        public TargetField(IBubbleStrategyFactory strategyFactory, IFieldGenerator generator, 
            FieldTargetParentTag parent)
        {
            _generator = generator;
            _parent = parent;

            _strategyFactory = strategyFactory;
        }

        public void Initialize()
        {
            var bubbles = _generator.Create();

            foreach (var bubble in bubbles)
            {
                Add(bubble);
            }
            
            Created?.Invoke();
            Debug.Log($"<b>Field created</b>");
        }

        public TargetNode Add(Bubble bubble)
        {
            var target = SetupTarget(bubble);
            var node = CreateNodeFrom(bubble);

            bubble.GameObject.name = _targetIndex.ToString();
            _targetIndex++;

            _nodes.Add(node);
            _bounds.Encapsulate(target.Collider.bounds);

            return node;
        }

        public void Remove(TargetNode node)
        {
            _nodes.Remove(node);
            RecalculateBounds();
        }

        private void RecalculateBounds()
        {
            _bounds = new Bounds();

            foreach (var targetNode in _nodes)
            {
                _bounds.Encapsulate(targetNode.Component.Collider.bounds);
            }
        }

        public bool FindNodeAt(Vector3 point, out TargetNode node)
        {
            if (!_bounds.Contains(point))
            {
                node = TargetNode.Empty;
                return false;
            }

            foreach (var currentNode in _nodes)
            {
                var delta = currentNode.Position - point;

                if (delta.magnitude > Contracts.HitDetectionRadius)
                    continue;

                node = currentNode;
                return true;
            }

            node = TargetNode.Empty;
            return false;
        }

        private TargetBubbleComponent SetupTarget(Bubble bubble)
        {
            bubble.Component.transform.SetParent(_parent.transform);
            bubble.ApplyStrategy(_strategyFactory.CreateStrategy<TargetBubbleStrategy>());
            
            var targetComponent = bubble.Component.GetComponent<TargetBubbleComponent>();
            targetComponent.SetOrigin(bubble.Position);
            
            return targetComponent;
        }

        private TargetNode CreateNodeFrom(Bubble bubble)
        {
            var currentNode = new TargetNode(bubble);
            SetNeighbours(currentNode);

            return currentNode;
        }

        private void SetNeighbours(TargetNode currentNode)
        {
            var position = currentNode.Bubble.Position;

            foreach (var node in _nodes)
            {
                foreach (var direction in TargetNode.Directions)
                {
                    if (!node.IsAt(position + direction * Contracts.BubbleRadius * 2))
                        continue;
                            
                    currentNode.Neighbours[direction] = node;
                    node.Neighbours[-direction] = currentNode;
                }
            }
        }
    }
}