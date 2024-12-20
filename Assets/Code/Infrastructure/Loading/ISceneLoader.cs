using System;

namespace Code.Infrastructure.Loading
{
    public interface ISceneLoader
    {
        void LoadScene(Scenes sceneName, Action onLoaded = null);
    }
}