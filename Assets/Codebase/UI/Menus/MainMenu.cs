using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Game.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI.Menus
{
    public class MainMenu : MonoBehaviour
    {
        [Header("Submenus")]
        [SerializeField] private GameSettingsMenu _gameSettingsMenu;
        [SerializeField] private LeaderboardMenu _leaderboardMenu;
        [SerializeField] private ExitConfirmMenu _exitConfirmMenu;

        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _aboutButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _exitButton;

        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        private void Start()
        {
            _startButton.onClick.AddListener(() => OpenSubMenu(_gameSettingsMenu));
            _leaderboardButton.onClick.AddListener(() => OpenSubMenu(_leaderboardMenu));
            _exitButton.onClick.AddListener(() => OpenSubMenu(_exitConfirmMenu));
            
            _aboutButton.onClick.AddListener(() => _gameStateMachine.Enter<AboutScreenState>());
        }

        private void OpenSubMenu(Component menu)
        {
            gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
        }
    }
}