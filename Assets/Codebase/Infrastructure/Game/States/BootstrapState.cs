using Codebase.Infrastructure.Abstract;
using Codebase.Logic.States.Abstract;

namespace Codebase.Infrastructure.Game.States
{
    public class BootstrapState : IState
    {
        private const string InitSceneName = "_InitScene";

        private readonly GameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter() => 
            _sceneLoader.Load(InitSceneName, onLoaded: OpenMenu);

        private void OpenMenu() => _gameStateMachine.Enter<MainMenuState>();

        public void Exit() {}
    }
}