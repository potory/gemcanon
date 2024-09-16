using System;

namespace Codebase.Infrastructure.Abstract
{
    public interface ISceneLoadingService
    {
        public void Load(string sceneName, bool forceReload = false, Action onLoaded = null);
    }
}