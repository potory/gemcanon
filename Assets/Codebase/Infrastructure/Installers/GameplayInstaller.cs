using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Game.Settings.Turns;
using Codebase.Infrastructure.Game.Settings.WinCondition;
using Codebase.Logic.Gameplay;
using Codebase.Logic.Gameplay.Factories;
using Codebase.Logic.Gameplay.Factories.Implementations;
using Codebase.Logic.Gameplay.Handlers.Implementations;
using Codebase.Logic.Gameplay.Services;
using Codebase.Logic.Gameplay.Services.Implementations;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        private GameSettings _gameSettings;

        [Inject]
        private void Construct(GameSettingsSource source) => 
            _gameSettings = source.CurrentSettings;

        public override void InstallBindings()
        {
            Container.Bind<GameSettings>().FromInstance(_gameSettings).AsSingle();
                
            Container.Bind<IInputService>().To<InputService>().AsTransient();
            Container.Bind<IScreenService>().To<ScreenService>().AsTransient();
            
            Container.Bind<ISessionScoreService>().To<SessionScoreService>().AsSingle();
            
            switch (_gameSettings.TurnsSettings)
            {
                case FiniteTurnsSettings:
                    Container.Bind<ITurnsService>().To<FiniteTurnService>().AsSingle();
                    Debug.Log($"<b>{nameof(FiniteTurnService)}</b> installed");
                    break;
                case InfiniteTurnsSettings:
                    Container.Bind<ITurnsService>().To<InfiniteTurnsService>().AsSingle();
                    Debug.Log($"<b>{nameof(InfiniteTurnsService)}</b> installed");
                    break;
            }

            switch (_gameSettings.WinConditionSettings)
            {
                case TopThirtyPercentWinConditionSettings: 
                    Container.BindInterfacesAndSelfTo<TopThirtyPercentGameStopService>().AsSingle();
                    Debug.Log($"<b>{nameof(TopThirtyPercentGameStopService)}</b> installed");
                    break;
                case CompleteWinConditionSettings:
                    Container.BindInterfacesAndSelfTo<CompleteGameStopService>().AsSingle();
                    Debug.Log($"<b>{nameof(CompleteGameStopService)}</b> installed");
                    break;
            }

            Container.Bind<IGameplayHandlerFactory>().To<GameplayHandlerFactory>().AsTransient();
            Container.BindInterfacesAndSelfTo<GameOverHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameLoop>().AsSingle().NonLazy();
        }
    }
}
