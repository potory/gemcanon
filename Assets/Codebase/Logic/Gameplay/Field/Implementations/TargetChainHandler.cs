using System.Collections.Generic;

namespace Codebase.Logic.Gameplay.Field.Implementations
{
    public class TargetChainHandler : ITargetChainHandler
    {
        private readonly List<TargetNode> _cache = new();

        public IReadOnlyList<TargetNode> Create(TargetNode node)
        {
            _cache.Clear();
            _cache.Add(node);
            
            BuildChainRecursively(node, _cache);

            return _cache;
        }
        
        private static void BuildChainRecursively(TargetNode node, List<TargetNode> nodes)
        {
            foreach (var (_, neighbourNode) in node.Neighbours)
            {
                if (neighbourNode == TargetNode.Empty)
                    continue;
                
                var isSameType = neighbourNode.Bubble.BubbleTypeId == node.Bubble.BubbleTypeId;
                
                if (!isSameType)
                    continue;
                
                if (nodes.Contains(neighbourNode))
                    continue;
                
                nodes.Add(neighbourNode);
                BuildChainRecursively(neighbourNode, nodes);
            }
        }
    }
}