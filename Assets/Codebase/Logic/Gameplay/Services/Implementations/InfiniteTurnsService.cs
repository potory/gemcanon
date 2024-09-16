using Codebase.Infrastructure.Exceptions;
using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Game.Settings.Turns;

namespace Codebase.Logic.Gameplay.Services.Implementations
{
    public class InfiniteTurnsService : ITurnsService
    {
        public InfiniteTurnsService(GameSettings settings)
        {
            if (settings.TurnsSettings is not InfiniteTurnsSettings turnsSettings)
                throw new InvalidServiceInstallException();
        }
        public int TurnsLeft => 99;
        public void NextTurn() {}
    }
}