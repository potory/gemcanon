using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting.Services
{
    public interface IPhysicsService
    {
        Impact GetImpact(float tension);
        Trajectory GetTrajectory(Vector2 startPosition, Vector2 direction, float tension);
    }
}