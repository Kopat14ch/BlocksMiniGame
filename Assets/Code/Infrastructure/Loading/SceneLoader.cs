using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;
        
        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(Scenes sceneName, Action onLoaded = null) 
            => _coroutineRunner.StartCoroutine(Load(sceneName, onLoaded));

        private IEnumerator Load(Scenes nextScene, Action onLoaded)
        {
            if (SceneManager.GetActiveScene().name == nextScene.ToString())
            {
                onLoaded?.Invoke();
                
                yield return null;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene.ToString());
            
            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}