using Codebase.Data;

namespace Codebase.Logic.Gameplay.Bubbles.Factories
{
    public interface IBubbleFactory
    {
        public Bubble CreateBubble(BubbleData data);
    }
}