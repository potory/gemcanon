using System.Linq;
using Codebase.Data;
using Codebase.Infrastructure.Abstract;
using Codebase.Logic.Gameplay.Bubbles.Data.Abstract;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Bubbles.Data.Implementations
{
    public class BubbleDataSource : IBubbleDataSource
    {
        private readonly BubbleData[] _scriptableObjects;

        public BubbleDataSource(IAssetProvider assetProvider)
        {
            const string scriptableObjectsPath = "Data/Bubbles";
            
            _scriptableObjects = assetProvider
                .GetAssets<BubbleData>(scriptableObjectsPath);
        }

        public BubbleData GetById(int bubbleTypeId) => 
            _scriptableObjects.First(so => so.BubbleTypeId == bubbleTypeId);

        public BubbleData GetRandom() => 
            _scriptableObjects[Random.Range(0, _scriptableObjects.Length)];
    }
}