using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting.Services
{
    public interface IPlaceholderService
    {
        void Set(Vector3 position);
        void Disable();
    }
}