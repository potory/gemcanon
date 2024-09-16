using Codebase.Infrastructure.Game.States;
using Codebase.Logic.States.Abstract;
using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.Game
{
    public sealed class GameStateMachine : StateMachine, IInitializable
    {
        private readonly GameStateFactory _gameStateFactory;

        public GameStateMachine(GameStateFactory gameStateFactory) => 
            _gameStateFactory = gameStateFactory;

        public void Initialize()
        {
            Debug.Log($"<b>{nameof(BootstrapState)}</b> initialized");
            Enter<BootstrapState>();
        }

        protected override TState ChangeState<TState>()
        {
            if (!States.ContainsKey(typeof(TState)))
            {
                States[typeof(TState)] = _gameStateFactory.CreateState<TState>();
            }
            
            return base.ChangeState<TState>();
        }
    }
}