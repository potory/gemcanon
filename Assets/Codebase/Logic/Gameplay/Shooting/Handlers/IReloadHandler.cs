using System;
using Codebase.Data;

namespace Codebase.Logic.Gameplay.Shooting.Handlers
{
    public interface IReloadHandler
    {
        void Handle();
        event Action Reloaded;
        event Action<BubbleData> NextPicked;
        BubbleData Next { get; }
    }
}