using System.Collections;
using Codebase.Infrastructure.Abstract;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Infrastructure.Implementations
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [Header("Components")]
        
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private AudioSource _audioSource;
        
        [Header("Settings")]
        
        [SerializeField] private int _fadeSpeed = 10;
        [SerializeField] private bool _animate = true;
        
        [Header("Animated elements")]
        
        [SerializeField] private RectTransform _canonTransform;
        [SerializeField] private RectTransform _titleTransform;
        [SerializeField] private RectTransform _ribbonTransform;
        [SerializeField] private RectTransform _ribbonText;

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
            
            StartCoroutine(PlayAnimation());
        }

        private IEnumerator PlayAnimation()
        {
            if (!_animate)
                yield break;

            AnimateCanon();
            AnimateTitle();
            AnimateRibbon();
            AnimateRibbonText();
        }

        public void Hide() => StartCoroutine(FadeIn());
    
        public void SetProgress(float value) => 
            _progressSlider.SetValueWithoutNotify(value * 100);

        private IEnumerator FadeIn()
        {
            if (_fadeSpeed <= 0)
            {
                gameObject.SetActive(false);
                yield break;
            }

            while (_canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= (float)_fadeSpeed / 100;
                yield return new WaitForSeconds(0.03f);
            }
        }

        private void AnimateCanon()
        {
            var sourceScale = _canonTransform.localScale;
            
            _canonTransform.localScale = Vector3.zero;
            _canonTransform.DOScale(sourceScale, 0.5f).SetEase(Ease.InOutBounce);
        }

        private void AnimateTitle()
        {
            var sourcePosition = _titleTransform.localPosition;
            
            _titleTransform.localPosition = Vector3.zero;
            _titleTransform.DOLocalMove(sourcePosition, 0.5f).SetEase(Ease.InOutElastic);
        }

        private void AnimateRibbon()
        {
            var sourceSize = _ribbonTransform.sizeDelta;

            _ribbonTransform.sizeDelta = new Vector2(sourceSize.y, sourceSize.y);
            _ribbonTransform.DOSizeDelta(sourceSize, 0.5f).SetEase(Ease.InOutQuad);
        }

        private void AnimateRibbonText()
        {
            var sourceScale = _ribbonText.localScale;
            
            _ribbonText.localScale = Vector3.zero;
            _ribbonText.DOScale(sourceScale, 0.5f).SetEase(Ease.InOutBounce);
        }
    }
}