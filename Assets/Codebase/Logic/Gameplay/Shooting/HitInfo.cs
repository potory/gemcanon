using System;
using Codebase.Logic.Gameplay.Field;

namespace Codebase.Logic.Gameplay.Shooting
{
    [Flags]
    public enum AllowedHit
    {
        Forbidden = 0,
        Attach = 1,
        Replace = 2
    }

    public sealed class HitInfo
    {
        public static readonly HitInfo None = new();
        public AllowedHit AllowedHit { get; }
        public TargetNode Node { get; }

        private HitInfo() {}

        public HitInfo(AllowedHit allowedHit, TargetNode node)
        {
            AllowedHit = allowedHit;
            Node = node;
        }
    }
}