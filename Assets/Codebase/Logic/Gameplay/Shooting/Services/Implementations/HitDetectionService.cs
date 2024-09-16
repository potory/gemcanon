using Codebase.Infrastructure.Exceptions;
using Codebase.Logic.Gameplay.Field;
using Codebase.Logic.Gameplay.Services;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting.Services.Implementations
{
    public class HitDetectionService : IHitDetectionService
    {
        private readonly Vector2 _screenWorldSize;
        private readonly ITargetField _targetField;

        public HitDetectionService(IScreenService screenService, ITargetField targetField)
        {
            _screenWorldSize = screenService.GetScreenWorldSize();
            _targetField = targetField;
        }

        public HitInfo GetHitInfo(Vector3 point)
        {
            if(!_targetField.FindNodeAt(point, out var node))
                return HitInfo.None;

            if (!node.TryFindEmptyPositionAround(point, out var supposedPosition))
                throw new InvalidTrajectoryException();

            var absoluteX = Mathf.Abs(supposedPosition.Value.x);
            var screenWorldX = _screenWorldSize.x / 2;
            
            if (absoluteX + Contracts.BubbleRadius > screenWorldX
                || supposedPosition.Value.y - _targetField.Origin.y > 0.5f)
            {
                return new HitInfo(AllowedHit.Replace, node);
            }

            return new HitInfo(AllowedHit.Attach | AllowedHit.Replace, node);
        }
    }
}