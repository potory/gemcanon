using System;
using Codebase.Logic.Gameplay.Bubbles;
using Codebase.Logic.Gameplay.Field;

namespace Codebase.Logic.Gameplay.Shooting.Handlers
{
    public interface IMovingHandler
    {
        public event Action<TargetNode, Impact, Bubble> ReachedDesignation;

        public void Handle(Trajectory trajectory, Impact impact, Bubble bubble);
    }
}