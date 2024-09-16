using Codebase.Infrastructure.Abstract;
using UnityEngine;

namespace Codebase.Infrastructure.Implementations
{
    public class ResourceAssetProvider : IAssetProvider
    {
        public TAsset GetAsset<TAsset>(string path) where TAsset : Object => 
            Resources.Load<TAsset>(path);

        public TAsset[] GetAssets<TAsset>(string path) where TAsset : Object
        {
            var assets = Resources.LoadAll<TAsset>(path);
            return assets;
        }
    }
}