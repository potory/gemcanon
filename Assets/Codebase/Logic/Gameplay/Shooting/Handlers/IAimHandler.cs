using System;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Bubbles.Components;

namespace Codebase.Logic.Gameplay.Shooting.Handlers
{
    public interface IAimHandler
    {
        void SetBubble(LoadedBubbleComponent bubbleComponent);
        event Action<AimInfo> Aim;
        event Action<AimInfo, Bubble> AimFinished;
        void ReturnToOrigin();
        void Release();
    }
}