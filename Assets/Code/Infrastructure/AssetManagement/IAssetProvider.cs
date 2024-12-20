using UnityEngine;

namespace Code.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        public GameObject LoadAsset(string path);
        public T LoadAsset<T>(string path) where T : Component;
    }
}