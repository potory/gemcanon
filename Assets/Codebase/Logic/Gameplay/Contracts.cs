namespace Codebase.Logic.Gameplay
{
    public static class Contracts
    {
        public const float BubbleRadius = 0.5f;
        public const float HitDetectionRadius = 0.6f;

        public const float MinTension = 0.5f;
        public const float MaxTension = 2f;
        public const float MaxTensionDelta = 0.2f;
        
        public const float SpreadAngle = 10f;
        
        public const float TensionVelocityGain = 15f;
        public const float SimulationStep = 0.005f;
    }
}