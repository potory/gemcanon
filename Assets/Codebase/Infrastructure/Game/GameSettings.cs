using Codebase.Infrastructure.Game.Settings.Field;
using Codebase.Infrastructure.Game.Settings.Turns;
using Codebase.Infrastructure.Game.Settings.WinCondition;

namespace Codebase.Infrastructure.Game
{
    public class GameSettings
    {
        public TurnsSettings TurnsSettings { get; }
        public FieldSettings FieldSettings { get; }
        public WinConditionSettings WinConditionSettings { get; }

        public GameSettings(TurnsSettings turnsSettings, FieldSettings fieldSettings, 
            WinConditionSettings winConditionSettings)
        {
            TurnsSettings = turnsSettings;
            FieldSettings = fieldSettings;
            WinConditionSettings = winConditionSettings;
        }
    }
}