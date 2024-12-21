using UnityEngine;

namespace Code.Gameplay.Features.Drag.Behaviour
{
    public class DragContainerBehaviour : MonoBehaviour, IDragContainerBehaviour
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public Transform GetTransform() =>
             _transform;
    }
}