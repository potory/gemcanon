namespace Codebase.Infrastructure.Game
{
    public class GameSettingsSource
    {
        public GameSettings CurrentSettings { get; private set; }

        public void Set(GameSettings gameSettings) => CurrentSettings = gameSettings;
    }
}