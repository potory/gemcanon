using Codebase.Infrastructure.Abstract;
using Codebase.Logic.Gameplay.Shooting.Tags;
using UnityEngine;

namespace Codebase.Logic.Gameplay.Shooting.Services.Implementations
{
    public class PlaceholderService : IPlaceholderService
    {
        private const string ResourcePath = "Prefabs/Shooter/Placeholder";
        
        private readonly GameObject _gameObject;
        
        public PlaceholderService(IAssetProvider assetProvider)
        {
            var prefab = assetProvider.GetAsset<PlaceholderTag>(ResourcePath);
            
            _gameObject = Object.Instantiate(prefab).gameObject;
            _gameObject.SetActive(false);
        }

        public void Set(Vector3 position)
        {
            _gameObject.SetActive(true);
            _gameObject.transform.position = position;
        }

        public void Disable()
        {
            _gameObject.SetActive(false);
        }
    }
}