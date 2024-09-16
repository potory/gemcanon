using System;
using Codebase.Logic.Gameplay.Field;
using Codebase.Logic.Gameplay.Services;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting.Services.Implementations
{
    public class PhysicsService : IPhysicsService
    {
        private const int StandardArrayLength = 256;

        private readonly IScreenService _screenService;
        private readonly IHitDetectionService _hitDetectionService;

        private Vector3[] _data = new Vector3[StandardArrayLength];

        public PhysicsService(IScreenService screenService, IHitDetectionService hitDetectionService)
        {
            _screenService = screenService;
            _hitDetectionService = hitDetectionService;
        }

        public Impact GetImpact(float tension)
        {
            var impact = tension + Contracts.MaxTensionDelta > Contracts.MaxTension ? 
                Impact.Powerful : 
                Impact.Standard;
            
            return impact;
        }

        public Trajectory GetTrajectory(Vector2 startPosition, Vector2 direction, float tension)
        {
            var trajectory = CalculateTrajectoryInternal(startPosition, direction, tension, _data);
            _data = trajectory.Path.Points;

            return trajectory;
        }

        private Trajectory CalculateTrajectoryInternal(Vector2 startPosition, Vector2 direction, float tension, Vector3[] array)
        {
            var impact = GetImpact(tension);
            var velocity = tension * Contracts.TensionVelocityGain;

            var screenSize = _screenService.GetScreenWorldSize();
            var halfHeight = screenSize.y / 2;

            var currentY = 0f;
            var index = 0;

            var equation = new TrajectoryEquation(startPosition, direction, velocity, screenSize);

            float currentT = 0;
            TargetNode targetNode = null;
            
            while (Mathf.Abs(currentY) < halfHeight)
            {
                currentT = index * Contracts.SimulationStep;
                
                var position = equation.Evaluate(currentT);

                currentY = position.y;

                if (array.Length <= index)
                {
                    var currentArray = array;
                    array = new Vector3[currentArray.Length * 2];
                    
                    Array.Copy(currentArray, array, currentArray.Length);
                }
                
                array[index] = position;
                index++;
                
                var bubbleNodeHit = _hitDetectionService.GetHitInfo(position);

                if (bubbleNodeHit == HitInfo.None) 
                    continue;

                if (!bubbleNodeHit.AllowedHit.HasFlag(AllowedHit.Attach) &&
                    impact != Impact.Powerful)
                {
                    targetNode = TargetNode.ForbiddenAttach;
                    break;
                }

                targetNode = bubbleNodeHit.Node;
                break;
            }

            targetNode ??= TargetNode.Empty;

            var path = new TrajectoryPath(index, array);
            equation.SetLimit(currentT);

            return new Trajectory(targetNode, equation, path);
        }
    }
}