using Codebase.Logic.Gameplay.Bubbles.Components;

namespace Codebase.Logic.Gameplay.Shooting.Services
{
    public interface IHighlightService
    {
        public void Highlight(BaseBubbleComponent component);
        public void Disable();
    }
}