using System;
using Codebase.Infrastructure.Abstract;
using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Game.Settings.Field;
using Codebase.Infrastructure.Game.Settings.Turns;
using Codebase.Infrastructure.Game.Settings.WinCondition;
using Codebase.Infrastructure.Game.States;
using Codebase.UI.Menus.Settings;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Codebase.UI.Menus
{
    public class GameSettingsMenu : MonoBehaviour
    {
        [Header("Submenus")]
        [SerializeField] private MainMenu _mainMenu;
        
        [Header("Elements")]
        [SerializeField] private FieldSettingsElement _fieldSettings;
        [SerializeField] private TurnsSettingsElement _turnsSettings;
        [SerializeField] private WinConditionSettingsElement _winConditionSettings;

        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _cancelButton;

        private GameStateMachine _gameStateMachine;
        private GameSettingsSource _settingsSource;
        private IFieldDataSource _fieldDataSource;

        [Inject]
        private void Construct(GameSettingsSource settingsSource, IFieldDataSource fieldDataSource, 
            GameStateMachine gameStateMachine)
        {
            _settingsSource = settingsSource;
            _gameStateMachine = gameStateMachine;
            _fieldDataSource = fieldDataSource;
        }

        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartButtonPress);
            _cancelButton.onClick.AddListener(OnCancelButtonPress);
            
            _fieldSettings.PredefinedFieldSettings.SetOptions(_fieldDataSource.GetFieldsNames());
        }

        private void OnCancelButtonPress()
        {
            gameObject.SetActive(false);
            _mainMenu.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.RemoveAllListeners();
        }
        
        private void OnStartButtonPress()
        {
            var gameSettings = CreateGameSettings();

            _settingsSource.Set(gameSettings);
            _gameStateMachine.Enter<GameplayState>();
        }

        private GameSettings CreateGameSettings()
        {
            TurnsSettings turnsSettings;

            if (_turnsSettings.Type == TurnsSettingsElement.TurnsType.Limited)
                turnsSettings = new FiniteTurnsSettings(_turnsSettings.LimitedTurnsSettings.TurnsCount);
            else
                turnsSettings = new InfiniteTurnsSettings();

            FieldSettings fieldSettings;

            switch (_fieldSettings.Type)
            {
                case FieldSettingsElement.FieldType.Random:
                    fieldSettings = new RandomFieldSettings(_fieldSettings.RandomFieldSettings.Size);
                    break;
                case FieldSettingsElement.FieldType.Predefined:
                    fieldSettings = new PredefinedFieldSettings(_fieldSettings.PredefinedFieldSettings.FieldName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            WinConditionSettings winConditionSettings;

            switch (_winConditionSettings.Type)
            {
                case WinConditionSettingsElement.WinConditionType.TopThirtyPercent:
                    winConditionSettings = new TopThirtyPercentWinConditionSettings();
                    break;
                case WinConditionSettingsElement.WinConditionType.Complete:
                    winConditionSettings = new CompleteWinConditionSettings();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new GameSettings(turnsSettings, fieldSettings, winConditionSettings);
        }
    }
}