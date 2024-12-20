using UnityEngine;

namespace Code.Gameplay.Features.UserInput.Services
{
    public class InputService : IInputService
    {
        public Vector3 GetMousePosition() => 
            Input.mousePosition;
        
    }
}