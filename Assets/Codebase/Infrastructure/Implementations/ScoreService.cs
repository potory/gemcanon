using System.Collections.Generic;
using System.Text;
using Codebase.Infrastructure.Abstract;
using UnityEngine;

namespace Codebase.Infrastructure.Implementations
{
    public class ScoreService : IScoreService
    {
        private const char Separator = ';';
        private const string PrefKey = "RecordsList";

        private readonly StringBuilder _sb = new();
        private readonly List<ScoreRecord> _records = new();
        
        public ScoreService()
        {
            string recordsString = PlayerPrefs.GetString(PrefKey, string.Empty);

            if (string.IsNullOrEmpty(recordsString))
                return;

            var recordsData = recordsString.Split(Separator);

            for (int i = 0; i < recordsData.Length; i += 2)
            {
                var playerName = recordsData[i];
                var score = int.Parse(recordsData[i+1]);
                
                _records.Add(new ScoreRecord(playerName, score));
            }
        }

        public void Clear()
        {
            _records.Clear();
            Save();
        }
        
        public void Add(string name, int score)
        {
            var newRecord = new ScoreRecord(name, score);
            _records.Add(newRecord);
            Save();
        }

        private void Save()
        {
            _sb.Clear();
            
            foreach (var record in _records)
            {
                _sb.Append($"{record.Name}{Separator}{record.Score}{Separator}");
            }

            if (_sb.Length > 0) 
                _sb.Length -= 1;

            PlayerPrefs.SetString(PrefKey, _sb.ToString());
        }

        public IReadOnlyList<ScoreRecord> GetAll() => _records;
    }
}