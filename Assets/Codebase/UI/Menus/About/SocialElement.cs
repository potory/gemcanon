using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Codebase.UI.Menus.About
{
    public class SocialElement : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _icon;
        
        public event Action Clicked;

        public void SetIcon(Sprite icon) => _icon.sprite = icon;

        public void OnPointerClick(PointerEventData eventData)
        {
            var sequence = DOTween.Sequence();

            sequence.Append(transform.DOScale(Vector3.one * 0.75f, 0.1f).SetEase(Ease.OutCubic))
                .Append(transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutCubic));

            sequence.Play();
            
            Clicked?.Invoke();
        }
    }
}