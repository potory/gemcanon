using System;
using Codebase.Logic.Gameplay.Bubbles;

namespace Codebase.Logic.Gameplay.Shooting.Handlers
{
    public interface ILaunchHandler
    {
        event Action<Trajectory, Impact, Bubble> Launched;
        event Action Cancelled;
        void HandleLaunch(AimInfo aimInfo, Bubble bubble);
    }
}