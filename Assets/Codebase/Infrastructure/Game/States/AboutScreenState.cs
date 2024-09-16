using Codebase.Infrastructure.Abstract;
using Codebase.Logic.States.Abstract;

namespace Codebase.Infrastructure.Game.States
{
    public class AboutScreenState : IState
    {
        private readonly ISceneLoadingService _sceneLoadingService;

        public AboutScreenState(ISceneLoadingService sceneLoadingService)
        {
            _sceneLoadingService = sceneLoadingService;
        }

        public void Enter()
        {
            const string sceneName = "AboutScene";
            _sceneLoadingService.Load(sceneName);
        }

        public void Exit() {}
    }
}