using Codebase.Logic.Gameplay.Services;
using TMPro;
using UnityEngine;
using Zenject;

namespace Codebase.UI.Displays
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;

        [Inject]
        private void Construct(ISessionScoreService sessionScoreService)
        {
            sessionScoreService.ScoreChange += OnScoreChange;
        }

        private void Start() => OnScoreChange(0);
        private void OnScoreChange(int value) => _label.text = $"Счёт: {value}";
    }
}