namespace Codebase.Infrastructure.Game.Settings.Field
{
    public class PredefinedFieldSettings : FieldSettings
    {
        public string Name { get; }

        public PredefinedFieldSettings(string name)
        {
            Name = name;
        }
    }
}