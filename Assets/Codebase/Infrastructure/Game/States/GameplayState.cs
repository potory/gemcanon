using Codebase.Infrastructure.Abstract;
using Codebase.Logic.States.Abstract;

namespace Codebase.Infrastructure.Game.States
{
    public class GameplayState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoadingService _sceneLoadingService;

        public GameplayState(GameStateMachine stateMachine, ISceneLoadingService sceneLoadingService)
        {
            _stateMachine = stateMachine;
            _sceneLoadingService = sceneLoadingService;
        }

        public void Enter()
        {
            const string sceneName = "GameplayScene";
            _sceneLoadingService.Load(sceneName);
        }

        public void Exit() {}
    }
}