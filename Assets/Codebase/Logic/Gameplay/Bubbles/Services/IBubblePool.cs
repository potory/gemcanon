namespace Codebase.Logic.Gameplay.Bubbles.Services
{
    public interface IBubblePool
    {
        public void Add(Bubble bubble);
        public bool TryGetFree(out Bubble bubble);
    }
}