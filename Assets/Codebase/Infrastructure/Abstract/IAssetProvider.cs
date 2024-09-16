using UnityEngine;

namespace Codebase.Infrastructure.Abstract
{
    public interface IAssetProvider
    {
        public TAsset GetAsset<TAsset>(string path) where TAsset : Object;
        public TAsset[] GetAssets<TAsset>(string path) where TAsset : Object;
    }
}