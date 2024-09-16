using Codebase.Data;
using Codebase.Infrastructure.Abstract;
using Codebase.Infrastructure.Exceptions;
using Codebase.Infrastructure.Game;
using Codebase.Infrastructure.Game.Settings.Field;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Bubbles.Data.Abstract;
using Codebase.Logic.Gameplay.Bubbles.Factories;
using Vector3 = UnityEngine.Vector3;

namespace Codebase.Logic.Gameplay.Field.Implementations
{
    public class PredefinedFieldGenerator : IFieldGenerator
    {
        private readonly IBubbleFactory _bubbleFactory;
        private readonly IBubbleDataSource _bubbleDataSource;

        private readonly FieldData[] _fieldDataArray;

        public PredefinedFieldGenerator(GameSettings settings, IBubbleFactory bubbleFactory, 
            IBubbleDataSource bubbleDataSource, IFieldDataSource fieldDataSource)
        {
            if (settings.FieldSettings is not PredefinedFieldSettings fieldSettings)
                throw new InvalidServiceInstallException();
            
            _bubbleFactory = bubbleFactory;
            _bubbleDataSource = bubbleDataSource;

            _fieldDataArray = fieldDataSource.GetFieldData(fieldSettings.Name);
        }
        
        public Bubble[] Create()
        {
            Bubble[] bubbles = new Bubble[_fieldDataArray.Length];

            for (var index = 0; index < _fieldDataArray.Length; index++)
            {
                var fieldData = _fieldDataArray[index];
                var bubbleData = _bubbleDataSource.GetById(fieldData.BubbleType);
                
                bubbles[index] = _bubbleFactory.CreateBubble(bubbleData);
                bubbles[index].SetPosition(new Vector3(fieldData.PositionX, fieldData.PositionY));
            }
            
            return bubbles;
        }
    }
}