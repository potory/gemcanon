using System;
using System.Linq;
using Codebase.Infrastructure.Abstract;
using Codebase.UI.Menus.Score;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI.Menus
{
    public class LeaderboardMenu : MonoBehaviour
    {
        [Header("Submenu")]
        [SerializeField] private MainMenu _mainMenu;
        
        [Header("Buttons")]
        [SerializeField] private Button _backButton;
        
        [Header("Components")]
        [SerializeField] private Transform _layout;

        [Header("Elements")]
        [SerializeField] private TextMeshProUGUI _noScoreRecordsLabel;
        [SerializeField] private LeaderboardRecordElement _template;
        [SerializeField] private Image _separator;

        private IScoreService _scoreService;
        
        [Inject]
        private void Construct(IScoreService scoreService) => 
            _scoreService = scoreService;

        private void Start()
        {
            _backButton.onClick.AddListener(() =>
            {
                _mainMenu.gameObject.SetActive(true);
                gameObject.SetActive(false);
            });

            var topRecords = _scoreService.GetAll()
                .OrderByDescending(r => r.Score).Take(5).ToArray();

            if (!topRecords.Any())
            {
                _noScoreRecordsLabel.gameObject.SetActive(true);
                AddSeparator();
                return;
            }

            foreach (var record in topRecords)
            {
                AddRecord(record);
                AddSeparator();
            }
        }

        private void AddRecord(ScoreRecord record)
        {
            var element = Instantiate(_template, _layout);
            element.Set(record.Name, record.Score);
            element.gameObject.SetActive(true);
        }

        private void AddSeparator()
        {
            Instantiate(_separator, _layout);
        }
    }
}