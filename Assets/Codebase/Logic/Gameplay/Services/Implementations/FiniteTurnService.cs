using Codebase.Infrastructure.Exceptions;
using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Game.Settings.Turns;

namespace Codebase.Logic.Gameplay.Services.Implementations
{
    public class FiniteTurnService : ITurnsService
    {
        public int TurnsLeft { get; private set; }

        public FiniteTurnService(GameSettings settings)
        {
            if (settings.TurnsSettings is not FiniteTurnsSettings turnsSettings)
                throw new InvalidServiceInstallException();

            TurnsLeft = turnsSettings.AvailableTurns;
        }

        public void NextTurn() => TurnsLeft -= 1;
    }
}