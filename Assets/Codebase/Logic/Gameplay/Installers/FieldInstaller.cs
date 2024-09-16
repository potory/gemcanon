using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Game.Settings.Field;
using Codebase.Logic.Gameplay.Field;
using Codebase.Logic.Gameplay.Field.Implementations;
using Zenject;

namespace Codebase.Logic.Gameplay.Installers
{
    public class FieldInstaller : MonoInstaller
    {
        private GameSettings _gameSettings;

        [Inject]
        private void Construct(GameSettings gameSettings) => _gameSettings = gameSettings;
        
        public override void InstallBindings()
        {
            switch (_gameSettings.FieldSettings)
            {
                case RandomFieldSettings:
                    Container.Bind<IFieldGenerator>().To<RandomFieldGenerator>().AsSingle();
                    break;
                case PredefinedFieldSettings:
                    Container.Bind<IFieldGenerator>().To<PredefinedFieldGenerator>().AsSingle();
                    break;
            }

            Container.BindInterfacesAndSelfTo<TargetField>().AsSingle();
        }
    }
}