using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting
{
    public readonly struct AimInfo
    {
        public AimInfo(Vector3 position, Vector3 direction, float tension)
        {
            Position = position;
            Direction = direction;
            Tension = tension;
        }

        public Vector3 Position { get; }
        public Vector3 Direction { get; }
        public float Tension { get; }
    }
}