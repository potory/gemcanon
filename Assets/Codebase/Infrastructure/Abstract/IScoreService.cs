using System.Collections.Generic;

namespace Codebase.Infrastructure.Abstract
{
    public sealed class ScoreRecord
    {
        public string Name { get; }
        public int Score { get; }

        public ScoreRecord(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }

    public interface IScoreService
    {
        public void Add(string name, int score);
        public IReadOnlyList<ScoreRecord> GetAll();
        void Clear();
    }
}