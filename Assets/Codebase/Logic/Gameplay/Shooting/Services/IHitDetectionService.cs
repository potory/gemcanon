using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting.Services
{
    public interface IHitDetectionService
    {
        public HitInfo GetHitInfo(Vector3 point);
    }
}