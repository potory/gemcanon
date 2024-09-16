using System;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Field;
using Codebase.Logic.Gameplay.Shooting.Services;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Codebase.Logic.Gameplay.Shooting.Handlers.Implementations
{
    public class LaunchHandler : ILaunchHandler
    {
        private readonly IPhysicsService _physicsService;

        public event Action<Trajectory, Impact, Bubble> Launched;
        public event Action Cancelled;

        public LaunchHandler(IPhysicsService physicsService)
        {
            _physicsService = physicsService;
        }
        
        public void HandleLaunch(AimInfo aimInfo, Bubble bubble)
        {
            if (aimInfo.Tension < Contracts.MinTension)
            {
                Cancelled?.Invoke();
                return;
            }
            
            float? angle = null;

            var impact = _physicsService.GetImpact(aimInfo.Tension);
            
            if (impact == Impact.Powerful)
            {
                angle = Random.Range(-Contracts.SpreadAngle, Contracts.SpreadAngle);
                Debug.Log($"Random angle: {angle}");
            }
            
            var trajectory = GetLaunchTrajectory(aimInfo, angle);

            if (trajectory.Target == TargetNode.ForbiddenAttach)
            {
                Cancelled?.Invoke();
                return;
            }
            
            Launched?.Invoke(trajectory, impact, bubble);
        }
        
        private Trajectory GetLaunchTrajectory(AimInfo info, float? additionalAngle = null)
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