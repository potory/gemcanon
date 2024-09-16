using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Codebase.Infrastructure.Exceptions;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Bubbles.Components;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase.Logic.Gameplay.Field
{
    public sealed class TargetNode
    {
        public static readonly IReadOnlyList<Vector3> Directions = new Vector3[]{
            new(-1, 0),
            new(1, 0),
            new(-0.5f, 1),
            new(0.5f, 1),
            new(0.5f, -1),
            new(-0.5f, -1)
        };
        
        public static readonly TargetNode Empty = new();
        public static readonly TargetNode ForbiddenAttach = new();
        
        public Vector3 Position { get; }
        public Bubble Bubble { get; }
        public TargetBubbleComponent Component { get; }

        public Color DebugColor = new(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1f);
        
        public Dictionary<Vector3, TargetNode> Neighbours { get; }
        
        public bool IsIsolated => Neighbours.All(n => n.Value == Empty);

        private TargetNode() {}
        public TargetNode(Bubble bubble)
        {
            Bubble = bubble;
            Position = bubble.Position;

            Neighbours = new Dictionary<Vector3, TargetNode>(Directions.Count);
            
            foreach (var direction in Directions)
            {
                Neighbours.Add(direction, Empty);
            }

            Component = bubble.GameObject.GetComponent<TargetBubbleComponent>();
        }

        public bool TryFindEmptyPositionAround(Vector3 point, [NotNullWhen(true)] out Vector3? emptyPosition)
        {
            var currentPosition = Bubble.Position;

            var minDelta = float.MaxValue;
            Vector3? closestEmpty = null;

            foreach (var direction in Directions)
            {
                if (Neighbours[direction] != Empty)
                    continue;

                var neighbourPosition = currentPosition + direction;
                var delta = (neighbourPosition - point).magnitude;

                if (delta < minDelta)
                {
                    minDelta = delta;
                    closestEmpty = currentPosition + direction;
                }
            }

            if (closestEmpty == null)
            {
                emptyPosition = null;
                return false;
            }

            emptyPosition = closestEmpty.Value;
            return true;
        }

        public bool IsAt(Vector3 position)
        {
            var difference = Bubble.Position - position;
            return difference.magnitude < 0.1f;
        }

        public override string ToString() => this == Empty ? "None" : base.ToString();
    }
}