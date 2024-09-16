using System.Collections.Generic;

namespace Codebase.Logic.Gameplay.Field
{
    public interface ITargetChainHandler
    {
        IReadOnlyList<TargetNode> Create(TargetNode node);
    }
}