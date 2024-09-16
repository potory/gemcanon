using TMPro;
using UnityEngine;

namespace Codebase.UI.Menus.Score
{
    public class LeaderboardRecordElement : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerNameLabel;
        [SerializeField] private TextMeshProUGUI _scoreLabel;

        public void Set(string playerName, int score)
        {
            _playerNameLabel.text = playerName;
            _scoreLabel.text = score.ToString();
        }
    }
}