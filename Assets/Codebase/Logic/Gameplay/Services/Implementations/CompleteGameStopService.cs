using System.Linq;
using Codebase.Logic.Gameplay.Field;

namespace Codebase.Logic.Gameplay.Services.Implementations
{
    public class CompleteGameStopService : IGameStopService
    {
        private readonly ITargetField _targetField;
        private readonly ITurnsService _turnsService;

        public CompleteGameStopService(ITargetField targetField, 
            ITurnsService turnsService)
        {
            _targetField = targetField;
            _turnsService = turnsService;
        }

        public GameLoopCheck Check()
        {
            if (!_targetField.Nodes.Any())
                return GameLoopCheck.Win;

            return _turnsService.TurnsLeft == 0 ? 
                GameLoopCheck.Loose : 
                GameLoopCheck.Continue;
        }
    }
}