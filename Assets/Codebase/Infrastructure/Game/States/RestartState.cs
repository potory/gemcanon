using Codebase.Infrastructure.Abstract;
using Codebase.Logic.States.Abstract;

namespace Codebase.Infrastructure.Game.States
{
    public class RestartState : IState
    {
        private readonly ISceneLoadingService _loadingService;

        public RestartState(ISceneLoadingService loadingService)
        {
            _loadingService = loadingService;
        }

        public void Enter()
        {
            const string sceneName = "GameplayScene";
            _loadingService.Load(sceneName, forceReload:true);
        }

        public void Exit() {}
    }
}