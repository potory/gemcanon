using Codebase.UI;
using Codebase.UI.Displays;
using Codebase.UI.Menus;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Codebase.Logic.Gameplay.Installers
{
    public class UserInterfaceInstaller : MonoInstaller
    {
        [SerializeField] private ScoreDisplay _scoreDisplay;
        [SerializeField] private NextBubbleDisplay _nextBubbleDisplay;
        [FormerlySerializedAs("_gameOverPopupComponent")] [SerializeField] private GameOverMenu _gameOverMenu;

        public override void InstallBindings()
        {
            Container.Bind<ScoreDisplay>().FromInstance(_scoreDisplay).AsSingle();
            Container.Bind<NextBubbleDisplay>().FromInstance(_nextBubbleDisplay).AsSingle();
            Container.Bind<GameOverMenu>().FromInstance(_gameOverMenu).AsSingle();
        }
    }
}