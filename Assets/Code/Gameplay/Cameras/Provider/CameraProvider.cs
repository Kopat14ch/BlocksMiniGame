using UnityEngine;

namespace Code.Gameplay.Cameras.Provider
{
    public class CameraProvider : ICameraProvider
    {
        private const float WidthMultiplier = 2.3f;
        private const float HeightMultiplier = 1.65f;
        
        private Camera _mainCamera;
        
        public Camera MainCamera
        {
            get
            {
                if (_mainCamera == null)
                    SetMainCamera(Camera.main);
                
                return Camera.main;
            }
            private set => _mainCamera = value;
        }

        public float WorldScreenHeight { get; private set; }
        public float WorldScreenWidth { get; private set; }

        public void SetMainCamera(Camera camera)
        {
            MainCamera = camera;

            RefreshBoundaries();
        }
        
        public Vector2 GetTopRandomPoint()
        {
            float randomX = Random.Range(MainCamera.transform.position.x - WorldScreenWidth / WidthMultiplier,
                MainCamera.transform.position.x + WorldScreenWidth / WidthMultiplier);
            
            float y = MainCamera.transform.position.y + WorldScreenHeight / HeightMultiplier;
            
            return new Vector2(randomX, y);
        }
        
        public bool IsObjectVisibleY(Vector3 objectPosition)
        {
            Vector3 bottomLeft = MainCamera.ViewportToWorldPoint(new Vector3(0, 0, MainCamera.nearClipPlane));
            Vector3 topRight = MainCamera.ViewportToWorldPoint(new Vector3(1, 1, MainCamera.nearClipPlane));
            
            return objectPosition.y >= bottomLeft.y && objectPosition.y <= topRight.y;
        }

        private void RefreshBoundaries()
        {
            Vector2 bottomLeft = MainCamera.ViewportToWorldPoint(new Vector3(0, 0, MainCamera.nearClipPlane));
            Vector2 topRight = MainCamera.ViewportToWorldPoint(new Vector3(1, 1, MainCamera.nearClipPlane));
            WorldScreenWidth = topRight.x - bottomLeft.x;
            WorldScreenHeight = topRight.y - bottomLeft.y;
        }
    }
}