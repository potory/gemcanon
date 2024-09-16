using Codebase.Logic.Gameplay.Bubbles.Components;
using Codebase.Logic.Gameplay.Shooting.Tags;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Gameplay.Bubbles.Strategies.Implementations
{
    public class LoadedBubbleStrategy : IBubbleStrategy
    {
        private readonly DiContainer _dependencies;
        private readonly LoadedBubbleParentTag _parent;
        
        private LoadedBubbleComponent _component;

        public LoadedBubbleStrategy(DiContainer dependencies, LoadedBubbleParentTag parent)
        {
            _dependencies = dependencies;
            _parent = parent;
        }

        public void Apply(Bubble bubble)
        {
            _component = _dependencies.InstantiateComponent<LoadedBubbleComponent>(bubble.Component.gameObject);
            
            bubble.Component.transform.SetParent(_parent.transform);
            bubble.Component.transform.localPosition = Vector3.zero;
        }

        public void Remove(Bubble bubble)
        {
            Object.Destroy(_component);
        }
    }
}