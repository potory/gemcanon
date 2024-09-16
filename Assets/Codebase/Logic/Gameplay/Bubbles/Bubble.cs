using Codebase.Logic.Gameplay.Bubbles.Components;
using Codebase.Logic.Gameplay.Bubbles.Strategies;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Bubbles
{
    public class Bubble
    {
        public int BubbleTypeId { get; private set; }
        public BaseBubbleComponent Component { get; }
        public GameObject GameObject { get; }

        public Vector3 Position => GameObject.transform.position;
        
        public IBubbleStrategy CurrentStrategy { get; private set; }

        public Bubble(int bubbleTypeId, BaseBubbleComponent component)
        {
            BubbleTypeId = bubbleTypeId;

            Component = component;
            Component.SetEntity(this);
            GameObject = component.gameObject;
        }

        public void SetPosition(Vector3 position) => 
            Component.transform.position = position;

        public void SetBubbleType(int id) =>
            BubbleTypeId = id;

        public void ApplyStrategy(IBubbleStrategy strategy)
        {
            CurrentStrategy?.Remove(this);
            CurrentStrategy = strategy;
            CurrentStrategy.Apply(this);
        }
    }
}