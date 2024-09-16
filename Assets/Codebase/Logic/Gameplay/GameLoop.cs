using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Factories;
using Codebase.Logic.Gameplay.Field;
using Codebase.Logic.Gameplay.Handlers;
using Codebase.Logic.Gameplay.Services;
using Codebase.Logic.Gameplay.Shooting;
using Codebase.Logic.Gameplay.Shooting.Handlers;
using Zenject;

namespace Codebase.Logic.Gameplay
{
    public class GameLoop : IInitializable
    {
        private readonly IGameplayHandlerFactory _handlerFactory;
        
        private IAimHandler _aimHandler;
        private IMovingHandler _movingHandler;
        private IReloadHandler _reloadHandler;
        private IHitHandler _hitHandler;
        private IHintsHandler _hintsHandler;
        private ILaunchHandler _launchHandler;
        private IGameOverHandler _gameOverHandler;

        private readonly ISessionScoreService _sessionScoreService;
        private readonly ITurnsService _turnsService;
        private readonly IGameStopService _stopService;

        public GameLoop(ISessionScoreService sessionScoreService, ITurnsService turnsService, 
            IGameStopService stopService, IGameplayHandlerFactory handlerFactory)
        {
            _sessionScoreService = sessionScoreService;
            _turnsService = turnsService;
            _stopService = stopService;
            _handlerFactory = handlerFactory;
        }

        public void Initialize()
        {
            SetupAim();
            SetupLaunch();
            SetupMoving();
            SetupHit();
            SetupReload();
            SetupHints();
            SetupGameOver();

            StartGame();
        }

        private void StartGame()
        {
            _reloadHandler.Handle();
        }

        private void SetupAim()
        {
            _aimHandler = _handlerFactory.CreateAimHandler();
            _aimHandler.Aim += OnAim;
            _aimHandler.AimFinished += OnReleased;
        }

        private void SetupLaunch()
        {
            _launchHandler = _handlerFactory.CreateLaunchHandler();
            _launchHandler.Launched += OnLaunched;
            _launchHandler.Cancelled += OnLaunchCancelled;
        }

        private void SetupMoving()
        {
            _movingHandler = _handlerFactory.CreateMovingHandler();
            _movingHandler.ReachedDesignation += OnReachedDesignation;
        }

        private void SetupHit()
        {
            _hitHandler = _handlerFactory.CreateHitHandler();
            _hitHandler.Complete += OnHitComplete;
            _hitHandler.Destroyed += OnDestroyed;
        }

        private void SetupReload()
        {
            _reloadHandler = _handlerFactory.CreateReloadHandler();
            _reloadHandler.Reloaded += OnReloaded;
        }

        private void SetupHints() => _hintsHandler = _handlerFactory.CreateHintsHandler();
        private void SetupGameOver() => _gameOverHandler = _handlerFactory.CreateGameOverHandler();

        private void OnAim(AimInfo aimInfo) => 
            _hintsHandler.HandleAim(aimInfo);

        private void OnReleased(AimInfo aimInfo, Bubble bubble) => 
            _launchHandler.HandleLaunch(aimInfo, bubble);

        private void OnLaunched(Trajectory trajectory, Impact impact, Bubble bubble)
        {
            _aimHandler.Release();
            _hintsHandler.DisableAim();
            
            _movingHandler.Handle(trajectory, impact, bubble);
        }

        private void OnLaunchCancelled()
        {
            _aimHandler.ReturnToOrigin();
            _hintsHandler.DisableAim();
        }

        private void OnReachedDesignation(TargetNode target, Impact impact, Bubble bubble) => 
            _hitHandler.Handle(target, impact, bubble);

        private void OnHitComplete()
        {
            var check = _stopService.Check();

            switch (check)
            {
                case GameLoopCheck.Win:
                    _gameOverHandler.HandleWin();
                    break;
                case GameLoopCheck.Loose:
                    _gameOverHandler.HandleLoose();
                    break;
                case GameLoopCheck.Continue:
                default:
                    _reloadHandler.Handle();
                    break;
            }
        }

        private void OnDestroyed(Bubble obj) => _sessionScoreService.IncreaseScore();
        private void OnReloaded() => _turnsService.NextTurn();
    }
}