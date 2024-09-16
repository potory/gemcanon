using System;
using Codebase.Infrastructure.Abstract;

namespace Codebase.Infrastructure.Implementations
{
    public class SceneLoadingService : ISceneLoadingService
    {
        private readonly ILoadingScreen _loadingScreen;
        private readonly ISceneLoader _sceneLoader;

        public SceneLoadingService(ISceneLoader sceneLoader, 
            ILoadingScreen loadingScreen)
        {
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
        }

        public void Load(string sceneName, bool forceReload = false, Action onLoaded = null)
        {
            _loadingScreen.Show();
            _loadingScreen.SetProgress(0);

            LoadScene(sceneName, forceReload, onLoaded);
        }
        
        private void LoadScene(string sceneName, bool forceReload = false, Action onLoaded = null) => 
            _sceneLoader.Load(sceneName, forceReload, onLoaded: () => OnLoaded(onLoaded), onProgress: OnProgress);
        
        private void OnProgress(float value) => 
            _loadingScreen.SetProgress(value);

        private void OnLoaded(Action onLoaded)
        {
            _loadingScreen.SetProgress(1);
            _loadingScreen.Hide();
            
            onLoaded?.Invoke();
        }
    }
}