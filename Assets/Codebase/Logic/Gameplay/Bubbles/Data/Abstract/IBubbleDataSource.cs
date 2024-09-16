using Codebase.Data;

namespace Codebase.Logic.Gameplay.Bubbles.Data.Abstract
{
    public interface IBubbleDataSource
    {
        public BubbleData GetById(int bubbleTypeId);
        public BubbleData GetRandom();
    }
}