using Codebase.Infrastructure.Exceptions;
using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Game.Settings.Field;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Bubbles.Data.Abstract;
using Codebase.Logic.Gameplay.Bubbles.Factories;
using Codebase.Logic.Gameplay.Field.Tags;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Field.Implementations
{
    public class RandomFieldGenerator : IFieldGenerator
    {
        private readonly IBubbleFactory _bubbleFactory;
        private readonly FieldTargetParentTag _parent;
        private readonly IBubbleDataSource _bubbleDataSource;

        private readonly Vector2Int _fieldSize;

        public RandomFieldGenerator(GameSettings settings, IBubbleFactory bubbleFactory, 
            FieldTargetParentTag parent, IBubbleDataSource bubbleDataSource)
        {
            if (settings.FieldSettings is not RandomFieldSettings fieldSettings)
                throw new InvalidServiceInstallException();
            
            _fieldSize = fieldSettings.FieldSize;
            _bubbleFactory = bubbleFactory;
            _parent = parent;
            _bubbleDataSource = bubbleDataSource;
        }

        public Bubble[] Create()
        {
            var array = new Bubble[_fieldSize.x * _fieldSize.y];
            
            for (int row = 0; row < _fieldSize.y; row++)
            {
                for (int column = 0; column < _fieldSize.x; column++)
                {
                    var position = GetPosition(row, column);

                    var bubbleData = _bubbleDataSource.GetRandom();
                    var bubble = _bubbleFactory.CreateBubble(bubbleData);
                    
                    bubble.SetPosition(position);

                    array[row * _fieldSize.x + column] = bubble;
                }
            }

            return array;
        }
        
        private Vector3 GetPosition(int row, int column)
        {
            var basicOffset = -_fieldSize.x /(float) 2 + Contracts.BubbleRadius * (_fieldSize.x % 2);
            var rowOffset = row % 2 * Contracts.BubbleRadius;

            return new Vector3(basicOffset + column + rowOffset, -row + _parent.transform.position.y, 0);
        }
    }
}