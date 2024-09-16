using Codebase.Logic.Gameplay.Bubbles.Components;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting.Services.Implementations
{
    public class HighlightService : IHighlightService
    {
        private BaseBubbleComponent _current;

        public void Highlight(BaseBubbleComponent component)
        {
            if (_current != null && _current != component) 
                _current.DisableHighlight();

            _current = component;
            _current.EnableHighlight(Color.white);
        }

        public void Disable() => 
            _current?.DisableHighlight();
    }
}