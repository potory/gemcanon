using Codebase.Logic.Gameplay.Field;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting
{
    public sealed class Trajectory
    {
        public TargetNode Target { get; }
        public TrajectoryEquation Equation { get; set; }
        public TrajectoryPath Path { get; }

        public Trajectory(TargetNode target, TrajectoryEquation equation, TrajectoryPath path)
        {
            Target = target;
            Equation = equation;
            Path = path;
        }
    }

    public sealed class TrajectoryPath
    {
        public int PointsCount { get; }
        public Vector3[] Points { get; }

        public TrajectoryPath(int pointsCount, Vector3[] points)
        {
            PointsCount = pointsCount;
            Points = points;
        }
    }

    public sealed class TrajectoryEquation
    {
        private readonly Vector2 _start;
        private readonly Vector2 _direction;
        private readonly float _velocity;
        private readonly Vector2 _screenSize;
        
        public float LimitT { get; private set; }

        public TrajectoryEquation(Vector2 start, Vector2 direction, float velocity, Vector2 screenSize)
        {
            _start = start;
            _direction = direction;
            _velocity = velocity;
            _screenSize = screenSize;
        }

        public Vector2 Evaluate(float t)
        {
            var width = _screenSize.x;
            var position = _start + _direction * (_velocity * t);
                
            if (position.x > width / 2 || 
                position.x < -width / 2)
            {
                position.x = GetBoundedX(position.x, width);
            }

            position.y += Physics2D.gravity.y * t * t;
            return position;
        }

        public void SetLimit(float t) => LimitT = t;

        private static float GetBoundedX(float x, float width)
        {
            var direction = x > 0 ? 1 : 0;

            var halfWidth = width / 2;
            var outOfBounds = Mathf.Abs(x) - halfWidth;

            var iteration = (int)(outOfBounds / width);

            if (iteration % 2 == direction)
                x = -halfWidth + outOfBounds % width;
            else
                x = halfWidth - outOfBounds % width;

            return x;
        }
    }
}