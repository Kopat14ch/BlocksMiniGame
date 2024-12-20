using UnityEngine;

namespace Code.Gameplay.Cameras.Provider
{
    public interface ICameraProvider
    {
        Camera MainCamera { get; }
        float WorldScreenHeight { get; }
        float WorldScreenWidth { get; }
        void SetMainCamera(Camera camera);
        
        Vector2 GetTopRandomPoint();
        bool IsObjectVisibleY(Vector3 objectPosition);
    }
}