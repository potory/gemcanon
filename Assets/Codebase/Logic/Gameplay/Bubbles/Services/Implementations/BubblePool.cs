using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Bubbles.Services.Implementations
{
    public class BubblePool : IBubblePool
    {
        private readonly Queue<Bubble> _cache = new();
        private readonly GameObject _parent = new("BubblePool");

        public void Add(Bubble bubble)
        {
            if (_cache.Contains(bubble)) 
                return;

            _cache.Enqueue(bubble);
            
            bubble.GameObject.transform.SetParent(_parent.transform);
            bubble.GameObject.SetActive(false);
        }

        public bool TryGetFree([NotNullWhen(true)] out Bubble bubble)
        {
            if (_cache.Any())
            {
                bubble = _cache.Dequeue();
                bubble.GameObject.SetActive(true);
                return true;
            }

            bubble = null;
            return false;
        }
    }
}