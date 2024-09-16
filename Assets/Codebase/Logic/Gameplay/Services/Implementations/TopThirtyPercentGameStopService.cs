using System.Linq;
using Codebase.Logic.Gameplay.Field;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Services.Implementations
{
    public class TopThirtyPercentGameStopService : IGameStopService
    {
        private readonly ITargetField _targetField;
        private readonly ITurnsService _turnsService;

        private int _initialTopCount;

        public TopThirtyPercentGameStopService(ITargetField targetField, ITurnsService turnsService)
        {
            _targetField = targetField;
            _turnsService = turnsService;

            _targetField.Created += OnFieldCreated;
        }

        private void OnFieldCreated()
        {
            _initialTopCount = _targetField.Nodes.Count(IsOnTop);
            Debug.Log($"<b>Initial top count</b>: {_initialTopCount}");
        }

        public GameLoopCheck Check()
        {
            var topCount = _targetField.Nodes.Count(IsOnTop);

            if (topCount / (float)_initialTopCount < 0.3f)
                return GameLoopCheck.Win;

            return _turnsService.TurnsLeft == 0 ? 
                GameLoopCheck.Loose : 
                GameLoopCheck.Continue;
        }

        private bool IsOnTop(TargetNode node) => 
            Mathf.Approximately(node.Position.y, _targetField.Origin.y);
    }
}