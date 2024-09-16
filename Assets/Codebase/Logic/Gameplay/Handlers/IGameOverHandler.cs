using System;

namespace Codebase.Logic.Gameplay.Handlers
{
    public enum GameResult
    {
        Win,
        Loose
    }

    public interface IGameOverHandler
    {
        public event Action<GameResult> GameOver;
        public void HandleWin();
        public void HandleLoose();
    }
}