using Codebase.Infrastructure.Abstract;
using Codebase.Logic.States.Abstract;

namespace Codebase.Infrastructure.Game.States
{
    public class MainMenuState : IState
    {
        private readonly ISceneLoadingService _sceneLoadingService;

        public MainMenuState(ISceneLoadingService sceneLoadingService)
        {
            _sceneLoadingService = sceneLoadingService;
        }

        public void Enter()
        {
            const string sceneName = "MainMenuScene";
            _sceneLoadingService.Load(sceneName);
        }

        public void Exit() {}
    }
}