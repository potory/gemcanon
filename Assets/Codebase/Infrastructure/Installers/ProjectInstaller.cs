using Codebase.Infrastructure.Abstract;
using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Implementations;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreen _loadingScreen;

        public override void InstallBindings()
        {
            RegisterInfrastructure();
            RegisterStates();
        }
        
        private void RegisterInfrastructure()
        {
            Container.Bind<ICoroutineRunner>().To<CoroutineRunner>()
                .FromNewComponentOn(gameObject).AsSingle().NonLazy();

            Container.Bind<ISceneLoader>().To<SceneLoader>()
                .AsTransient();

            Container.Bind<ILoadingScreen>().To<LoadingScreen>()
                .FromComponentInNewPrefab(_loadingScreen).AsSingle().NonLazy();

            Container.Bind<ISceneLoadingService>().To<SceneLoadingService>()
                .AsTransient();

            Container.Bind<IScoreService>().To<ScoreService>().AsSingle();
    
            Container.Bind<IAssetProvider>().To<ResourceAssetProvider>().AsTransient();
            Container.Bind<IFieldDataSource>().To<FieldDataSource>().AsTransient();

            Container.Bind<GameSettingsSource>().AsSingle();
        }

        private void RegisterStates()
        {
            Container.Bind<GameStateFactory>().AsTransient();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle().NonLazy();
        }
    }
}
