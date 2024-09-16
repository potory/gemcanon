using Codebase.Logic.Gameplay.Bubbles.Data.Abstract;
using Codebase.Logic.Gameplay.Bubbles.Data.Implementations;
using Codebase.Logic.Gameplay.Bubbles.Factories;
using Codebase.Logic.Gameplay.Bubbles.Factories.Implementations;
using Codebase.Logic.Gameplay.Bubbles.Services;
using Codebase.Logic.Gameplay.Bubbles.Services.Implementations;
using Zenject;

namespace Codebase.Logic.Gameplay.Installers
{
    public class BubblesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBubbleFactory>().To<BubbleFactory>().AsTransient();
            Container.Bind<IBubbleStrategyFactory>().To<BubbleStrategyFactory>().AsTransient();
            Container.Bind<IBubblePool>().To<BubblePool>().AsSingle();
            Container.Bind<IBubbleDataSource>().To<BubbleDataSource>().AsSingle();
        }
    }
}