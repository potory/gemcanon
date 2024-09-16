using Codebase.Data;
using Codebase.Logic.Gameplay.Services;
using Codebase.Logic.Gameplay.Shooting.Handlers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI.Displays
{
    public class NextBubbleDisplay : MonoBehaviour
    {
        [SerializeField] private Image _bubbleImage;
        [SerializeField] private TextMeshProUGUI _countLabel;
        
        private ITurnsService _turnsService;

        [Inject]
        private void Construct(ITurnsService turnsService, IReloadHandler reloadHandler)
        {
            _turnsService = turnsService;

            reloadHandler.NextPicked += OnNextPicked;
        }

        private void OnNextPicked(BubbleData nextData) => 
            Set(nextData.Sprite, _turnsService.TurnsLeft);

        private void Set(Sprite sprite, int count)
        {
            const float animationTime = 0.25f;
            
            var sequence = DOTween.Sequence();

            sequence.Append(transform.DOScale(Vector3.zero, animationTime / 2))
                .AppendCallback(() =>
                {
                    _bubbleImage.sprite = sprite;
                    _countLabel.text = count.ToString();
                })
                .Append(transform.DOScale(Vector3.one, animationTime / 2).SetEase(Ease.OutBounce));

            sequence.Play();
        }
    }
}