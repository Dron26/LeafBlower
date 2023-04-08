using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
    public class SceneLoader
    {
        private readonly ICorountineRunner _corountineRunner;

        public SceneLoader(ICorountineRunner corountineRunner) =>
            _corountineRunner = corountineRunner;

        public  void Load(string name, Action onLoaded = null)=>
            _corountineRunner.StartCoroutine(LoadScene(name, onLoaded));
        
        private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

            while (!waitNextScene.isDone)
                yield return null;
            
            onLoaded?.Invoke();
        }
    }
}