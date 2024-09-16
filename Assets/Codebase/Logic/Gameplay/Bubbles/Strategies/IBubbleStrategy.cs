namespace Codebase.Logic.Gameplay.Bubbles.Strategies
{
    public interface IBubbleStrategy
    {
        public void Apply(Bubble bubble);
        public void Remove(Bubble bubble);
    }
}