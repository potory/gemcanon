using Codebase.Logic.Gameplay.Bubbles.Components;
using Codebase.Logic.Gameplay.Field.Tags;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Codebase.Logic.Gameplay.Bubbles.Strategies.Implementations
{
    public class TargetBubbleStrategy : IBubbleStrategy
    {
        private readonly FieldTargetParentTag _parent;
        
        private Rigidbody2D _rigidBody;
        private SpringJoint2D _springJoint;
        private TargetBubbleComponent _targetBubbleComponent;

        public TargetBubbleStrategy(FieldTargetParentTag parent)
        {
            _parent = parent;
        }

        public void Apply(Bubble bubble)
        {
            bubble.Component.transform.SetParent(_parent.transform);
            
            _rigidBody = bubble.Component.gameObject.AddComponent<Rigidbody2D>();
            _springJoint = bubble.Component.gameObject.AddComponent<SpringJoint2D>();
            _targetBubbleComponent = bubble.Component.gameObject.AddComponent<TargetBubbleComponent>();
        }

        public void Remove(Bubble bubble)
        {
            Object.Destroy(_targetBubbleComponent);
            Object.Destroy(_springJoint);
        }
    }
}