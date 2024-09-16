using UnityEngine;

namespace Codebase.Infrastructure.Game.Settings.Field
{
    public class RandomFieldSettings : FieldSettings
    {
        public Vector2Int FieldSize { get; }

        public RandomFieldSettings(Vector2Int fieldSize)
        {
            FieldSize = fieldSize;
        }
    }
}