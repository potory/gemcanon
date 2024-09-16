using UnityEngine;

namespace Codebase.Logic.Gameplay.Bubbles.Components
{
    public class BaseBubbleComponent : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _highlightRenderer;
        [SerializeField] private SpriteRenderer _baseRenderer;
        
        [SerializeField] private int _bubbleType;

        public int BubbleType => _bubbleType;

        public Bubble Entity { get; private set; }

        public void SetEntity(Bubble entity) => 
            Entity = entity;

        public void SetSprite(Sprite sprite) =>
            _baseRenderer.sprite = sprite;
        

        public void EnableHighlight(Color color)
        {
            _highlightRenderer.enabled = true;
            _highlightRenderer.color = color;
        }

        public void DisableHighlight()
        {
            _highlightRenderer.enabled = false;
        }
    }
}