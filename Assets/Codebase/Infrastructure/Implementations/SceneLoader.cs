using System;
using System.Collections;
using Codebase.Infrastructure.Abstract;
using Codebase.Infrastructure.Exceptions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Infrastructure.Implementations
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner) => 
            _coroutineRunner = coroutineRunner;
        
        public void Load(string name, bool forceReload, Action onLoaded = null, Action<float> onProgress = null) => 
            _coroutineRunner.StartCoroutine(LoadScene(name, forceReload, onLoaded, onProgress));
        
        private static IEnumerator LoadScene(string nextScene, bool forceReload = false, 
            Action onLoaded = null, Action<float> onProgress = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene && !forceReload)
            {
                onLoaded?.Invoke();
                yield break;
            }

            yield return new WaitForSeconds(2);

            var waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            if (waitNextScene is null)
                throw new SceneLoadingException();
            
            while (!waitNextScene.isDone)
            {
                onProgress?.Invoke(waitNextScene.progress);
                yield return null;
            }
            
            onLoaded?.Invoke();
        }
    }
}