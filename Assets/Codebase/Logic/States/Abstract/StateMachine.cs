using System;
using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Logic.States.Abstract
{
    public abstract class StateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states = new();
        private IExitableState _activeState;

        protected Dictionary<Type, IExitableState> States => _states;

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        protected virtual TState ChangeState<TState>() where TState : class, IExitableState
        {
            Debug.Log($"Entering <b>{typeof(TState).Name}</b>");

            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}