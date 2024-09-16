namespace Codebase.Logic.Gameplay.Services
{
    public interface ITurnsService
    {
        int TurnsLeft { get; }
        void NextTurn();
    }
}