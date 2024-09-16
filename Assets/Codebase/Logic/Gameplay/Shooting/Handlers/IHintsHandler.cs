namespace Codebase.Logic.Gameplay.Shooting.Handlers
{
    public interface IHintsHandler
    {
        void HandleAim(AimInfo info);
        void DisableAim();
    }
}