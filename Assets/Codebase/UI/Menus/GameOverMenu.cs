using System;
using System.Linq;
using Codebase.Infrastructure.Abstract;
using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Game.States;
using Codebase.Logic.Gameplay.Handlers;
using Codebase.Logic.Gameplay.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI.Menus
{
    public class GameOverMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _scoreLabel;
        [SerializeField] private TextMeshProUGUI _gameOverResultLabel;
        [SerializeField] private TMP_InputField _playerNameInput;

        private IScoreService _scoreService;
        private ISessionScoreService _sessionScoreService;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameOverHandler gameOverHandler, GameStateMachine gameStateMachine,
            ISessionScoreService sessionScoreService, IScoreService scoreService)
        {
            _scoreService = scoreService;
            _sessionScoreService = sessionScoreService;
            _gameStateMachine = gameStateMachine;
            
            gameOverHandler.GameOver += OnGameOver;
            
            gameObject.SetActive(false);
            
            Debug.Log($"<b>{nameof(GameOverMenu)}</b> initialized");
        }

        private void OnGameOver(GameResult result)
        {
            SetResult(_sessionScoreService.Score, result);
            gameObject.SetActive(true);
        }

        private void SetResult(int score, GameResult result)
        {
            _scoreLabel.text = $"Счёт: {score:000}";

            if (result == GameResult.Win)
            {
                _gameOverResultLabel.text = "Вы победили!";
                _gameOverResultLabel.color = new Color(0.36f, 0.87f, 0.22f);
            }
            else
            {
                _gameOverResultLabel.text = "Вы проиграли!";
                _gameOverResultLabel.color = new Color(0.87f, 0.27f, 0.32f);
            }
        }

        private void Start()
        {
            _restartButton.onClick.AddListener(OnRestartButtonPress);
            _exitButton.onClick.AddListener(OnExitButtonPress);
        }

        private void OnRestartButtonPress()
        {
            var playerName = _playerNameInput.text;
            
            if (!string.IsNullOrEmpty(playerName)) 
                _scoreService.Add(playerName, _sessionScoreService.Score);
            
            Debug.Log(string.Join('\n', _scoreService.GetAll().Select(s => $"{s.Name}: {s.Score}")));
        
            _gameStateMachine.Enter<RestartState>();
        }

        private void OnExitButtonPress()
        {
            var playerName = _playerNameInput.text;
            
            if (!string.IsNullOrEmpty(playerName)) 
                _scoreService.Add(playerName, _sessionScoreService.Score);

            Debug.Log(_scoreService.GetAll().Select(s => $"{s.Name}: {s.Score}\n"));
            
            _gameStateMachine.Enter<MainMenuState>();
        }
    }
}