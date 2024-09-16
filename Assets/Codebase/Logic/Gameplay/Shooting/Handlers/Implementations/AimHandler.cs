using System;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Bubbles.Components;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Gameplay.Shooting.Handlers.Implementations
{
    public class AimHandler : ITickable, IAimHandler
    {
        private enum State
        {
            Idle,
            Aiming,
            Released
        }
        
        private LoadedBubbleComponent _loadedBubbleComponent;

        private State _state;

        public event Action<AimInfo> Aim;
        public event Action<AimInfo, Bubble> AimFinished;
        
        public void SetBubble(LoadedBubbleComponent bubbleComponent)
        {
            if (_loadedBubbleComponent)
            {
                _loadedBubbleComponent.DragStarted -= OnDragStarted;
                _loadedBubbleComponent.DragEnded -= OnDragEnded;
            }

            _loadedBubbleComponent = bubbleComponent;

            _loadedBubbleComponent.DragStarted += OnDragStarted;
            _loadedBubbleComponent.DragEnded += OnDragEnded;
        }

        public void ReturnToOrigin()
        {
            _loadedBubbleComponent.ReturnToOrigin();
            _state = State.Idle;
        }

        public void Release() => _state = State.Released;

        private void OnDragStarted()
        {
            _state = State.Aiming;
        }

        private void OnDragEnded(Vector2 vector)
        {
            
            var info = new AimInfo(_loadedBubbleComponent.transform.position,
                _loadedBubbleComponent.Direction,
                _loadedBubbleComponent.Tension);

            var bubbleEntity = _loadedBubbleComponent.GetComponent<BaseBubbleComponent>().Entity;
            AimFinished?.Invoke(info, bubbleEntity);
        }

        public void Tick()
        {
            if (!_loadedBubbleComponent)
                return;

            if (_state != State.Aiming)
            {
                return;
            }

            var info = new AimInfo(_loadedBubbleComponent.transform.position,
                _loadedBubbleComponent.Direction,
                _loadedBubbleComponent.Tension);
                
            Aim?.Invoke(info);
        }
    }
}