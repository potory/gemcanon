using System;
using Codebase.Logic.Gameplay.Services;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Codebase.Logic.Gameplay.Bubbles.Components
{
    public class LoadedBubbleComponent : MonoBehaviour
    {
        private enum State
        {
            Idle,
            Dragged,
            Returning
        }

        private State _state = State.Idle;

        private IInputService _inputService;

        private Vector3[] _path;
        private Vector3 _offset;
        
        public event Action DragStarted;
        public event Action<Vector2> DragEnded;

        public Vector3 Direction => transform.localPosition.normalized;
        public float Tension => transform.localPosition.magnitude;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
        
        private void OnMouseDown()
        {
            if (_state != State.Idle)
                return;
            
            _offset = gameObject.transform.position - _inputService.GetInputWorldPosition();
            _state = State.Dragged;
            
            DragStarted?.Invoke();
        }

        private void OnMouseDrag()
        {
            if (_state != State.Dragged)
                return;
            
            var globalPosition = _inputService.GetInputWorldPosition() + _offset;
            var localPosition = transform.parent.worldToLocalMatrix.MultiplyPoint(globalPosition);

            if (localPosition.magnitude > Contracts.MaxTension)
            {
                localPosition = localPosition.normalized * Contracts.MaxTension;
            }

            transform.localPosition = localPosition;
        }

        private void OnMouseUp() => 
            DragEnded?.Invoke(transform.localPosition);

        public void ReturnToOrigin()
        {
            _state = State.Returning;
            var sequence = DOTween.Sequence();

            sequence.Append(transform.DOLocalMove(Vector3.zero, 0.2f))
                .AppendCallback(() => _state = State.Idle);
        }
    }
}