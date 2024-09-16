using System;

namespace Codebase.Infrastructure.Abstract
{
    public interface ISceneLoader
    {
        void Load(string name, bool forceReload = false, Action onLoaded = null, Action<float> onProgress = null);
    }
}