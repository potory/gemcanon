using System;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Field;

namespace Codebase.Logic.Gameplay.Shooting.Handlers
{
    public interface IHitHandler
    {
        event Action Complete;
        void Handle(TargetNode target, Impact impact, Bubble bubble);
        event Action<Bubble> Destroyed;
    }
}