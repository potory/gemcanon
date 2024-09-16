using System;

namespace Codebase.Logic.Gameplay.Services.Implementations
{
    public class SessionScoreService : ISessionScoreService
    {
        public int Score { get; private set; }
        public int TurnsLeft { get; private set; } = 30;

        public event Action<int> ScoreChange;

        public void IncreaseScore()
        {
            Score++;
            ScoreChange?.Invoke(Score);
        }

        public void DecreaseTurns() => TurnsLeft -= 1;
    }
}