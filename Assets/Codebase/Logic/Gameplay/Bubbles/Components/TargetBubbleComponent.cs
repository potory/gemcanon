using UnityEngine;

namespace Codebase.Logic.Gameplay.Bubbles.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpringJoint2D))]
    public class TargetBubbleComponent : MonoBehaviour
    {
        private Rigidbody2D _rigidBody;

        public Collider2D Collider { get; private set; }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _rigidBody.gravityScale = 0;
            _rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

            _springJoint = GetComponent<SpringJoint2D>();
            Collider = GetComponent<Collider2D>();
        }

        private SpringJoint2D _springJoint;

        public void SetOrigin(Vector3 position)
        {
            _springJoint.frequency = 0;
            _springJoint.connectedAnchor = position;
        }

        public void SetFrequency(float frequency) => 
            _springJoint.frequency = frequency;

        public void SetVelocity(Vector2 velocity) => 
            _rigidBody.velocity = velocity;
    }
}