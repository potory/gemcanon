namespace Codebase.Infrastructure.Game.Settings.Turns
{
    public class FiniteTurnsSettings : TurnsSettings
    {
        public int AvailableTurns { get; }

        public FiniteTurnsSettings(int availableTurns)
        {
            AvailableTurns = availableTurns;
        }
    }
}