using Codebase.Logic.Gameplay.Handlers;
using Codebase.Logic.Gameplay.Shooting.Handlers;

namespace Codebase.Logic.Gameplay.Factories
{
    public interface IGameplayHandlerFactory
    {
        IAimHandler CreateAimHandler();
        IMovingHandler CreateMovingHandler();
        IReloadHandler CreateReloadHandler();
        IHitHandler CreateHitHandler();
        IHintsHandler CreateHintsHandler();
        ILaunchHandler CreateLaunchHandler();
        IGameOverHandler CreateGameOverHandler();
    }
}