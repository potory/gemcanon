using System;

namespace Codebase.Logic.Gameplay.Services
{
    public interface ISessionScoreService
    {
        int Score { get; }
        event Action<int> ScoreChange;
        void IncreaseScore();
    }
}