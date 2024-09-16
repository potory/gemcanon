using Codebase.Logic.Gameplay.Shooting.Components;
using Codebase.Logic.Gameplay.Shooting.Handlers;
using Codebase.Logic.Gameplay.Shooting.Handlers.Implementations;
using Codebase.Logic.Gameplay.Shooting.Services;
using Codebase.Logic.Gameplay.Shooting.Services.Implementations;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Gameplay.Installers
{
    public class ShootingInstaller : MonoInstaller
    {
        [SerializeField] private TrajectoryRendererComponent _trajectoryRenderer;
        
        public override void InstallBindings()
        {
            Container.Bind<TrajectoryRendererComponent>().FromInstance(_trajectoryRenderer).AsSingle();
            
            InstallServices();
            InstallHandlers();
        }

        private void InstallHandlers()
        {
            Container.Bind<IHitHandler>().To<HitHandler>().AsSingle();
            Container.Bind<IMovingHandler>().To<MovingHandler>().AsSingle();
            Container.Bind<IReloadHandler>().To<ReloadHandler>().AsSingle();
            Container.Bind<IHintsHandler>().To<HintsHandler>().AsTransient();
            Container.Bind<ILaunchHandler>().To<LaunchHandler>().AsTransient();
            Container.BindInterfacesAndSelfTo<AimHandler>().AsSingle();
        }

        private void InstallServices()
        {
            Container.Bind<IPlaceholderService>().To<PlaceholderService>().AsTransient();
            Container.Bind<IHighlightService>().To<HighlightService>().AsTransient();
            Container.Bind<IHitDetectionService>().To<HitDetectionService>().AsSingle();
            Container.Bind<IPhysicsService>().To<PhysicsService>().AsTransient();
        }
    }
}