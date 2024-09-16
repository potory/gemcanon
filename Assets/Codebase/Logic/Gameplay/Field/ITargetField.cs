using System;
using System.Collections.Generic;
using Codebase.Logic.Gameplay.Bubbles;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Field
{
    public interface ITargetField
    {
        event Action Created;
        Vector3 Origin { get; }
        IReadOnlyList<TargetNode> Nodes { get; }
        TargetNode Add(Bubble bubble);
        void Remove(TargetNode node);
        bool FindNodeAt(Vector3 point, out TargetNode node);
    }
}