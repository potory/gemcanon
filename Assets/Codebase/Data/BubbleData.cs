using UnityEngine;

namespace Codebase.Data
{
    [CreateAssetMenu(menuName = "BubbleShooter/BubbleData", fileName = "BubbleData")]
    public class BubbleData : ScriptableObject
    {
        [SerializeField] private int _bubbleTypeId;
        [SerializeField] private Sprite _sprite;

        public int BubbleTypeId => _bubbleTypeId;
        public Sprite Sprite => _sprite;
    }
}