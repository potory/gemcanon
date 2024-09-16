using Codebase.Infrastructure.Exceptions;
using Codebase.Logic.Gameplay.Field;
using Codebase.Logic.Gameplay.Shooting.Components;
using Codebase.Logic.Gameplay.Shooting.Services;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting.Handlers.Implementations
{
    public class HintsHandler : IHintsHandler
    {
        private readonly IPhysicsService _physicsService;
        private readonly IHighlightService _highlightService;
        private readonly IPlaceholderService _placeholderService;

        private readonly TrajectoryRendererComponent _trajectoryRenderer;

        public HintsHandler(IPhysicsService physicsService, 
            IPlaceholderService placeholderService, 
            IHighlightService highlightService, TrajectoryRendererComponent trajectoryRenderer)
        {
            _physicsService = physicsService;
            _placeholderService = placeholderService;
            _highlightService = highlightService;

            _trajectoryRenderer = trajectoryRenderer;
        }
        
        public void HandleAim(AimInfo info)
        {
            if (info.Tension < Contracts.MinTension)
            {
                DisableAim();
                return;
            }
            
            var impact = _physicsService.GetImpact(info.Tension);
            
            if (impact == Impact.Powerful)
                HandlePowerfulAim(info);
            else
                HandleStandardAim(info);
        }

        private void HandlePowerfulAim(AimInfo info)
        {
            var trajectoryA = GetHintTrajectory(info, -Contracts.SpreadAngle);
            _trajectoryRenderer.RenderLineA(trajectoryA);
                
            var trajectoryB = GetHintTrajectory(info, Contracts.SpreadAngle);
            _trajectoryRenderer.RenderLineB(trajectoryB);
                
            _highlightService.Disable();
            _placeholderService.Disable();
        }

        private void HandleStandardAim(AimInfo info)
        {
            var trajectory = GetHintTrajectory(info);
                
            if (trajectory.Target == TargetNode.ForbiddenAttach)
            {
                _trajectoryRenderer.RenderForbidden(trajectory);
                _placeholderService.Disable();
                _highlightService.Disable();
    
                return;
            }

            _trajectoryRenderer.RenderSingle(trajectory);

            if (trajectory.Target == TargetNode.Empty)
            {
                _placeholderService.Disable();
                _highlightService.Disable();
                return;
            }

            _highlightService.Highlight(trajectory.Target.Bubble.Component);
                
            var lastPoint = trajectory.Equation.Evaluate(trajectory.Equation.LimitT);
            
            if (!trajectory.Target.TryFindEmptyPositionAround(lastPoint, out var position))
            {
                throw new InvalidTrajectoryException();
            }
                
            _placeholderService.Set(position.Value);
        }

        public void DisableAim()
        {
            _placeholderService.Disable();
            _highlightService.Disable();
            _trajectoryRenderer.Disable();
        }
        
        private Trajectory GetHintTrajectory(AimInfo info, float? additionalAngle = null)
        {
            var position = info.Position;
            var direction = -info.Direction;

            if (!additionalAngle.HasValue) 
                return _physicsService.GetTrajectory(position, direction, info.Tension);
            
            var rotation = Quaternion.Euler(0, 0, additionalAngle.Value);
            direction = rotation * direction;

            return _physicsService.GetTrajectory(position, direction, info.Tension);
        }
    }
}