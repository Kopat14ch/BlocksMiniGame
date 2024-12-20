using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Features.UserInput.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Drag.Services
{
    public class DragService : IDragService
    {
        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;

        private RectTransform _draggedTransform;
        private Transform _lastTransformParent;
        private Vector3 _lastPosition;
        private int _lastSiblingIndex;

        private IDraggable _draggable;

        public DragService(IInputService inputService, ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _inputService = inputService;
        }
        
        public void StartDrag(IDraggable draggable)
        {
            _draggable = draggable;
            
            _draggedTransform = _draggable.GetRectTransform();
            _lastTransformParent = _draggedTransform.parent;
            _lastSiblingIndex = _draggedTransform.GetSiblingIndex();
            _lastPosition = _draggedTransform.anchoredPosition;
        }

        public void Dragging()
        {
            if (_draggedTransform != null)
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _draggedTransform.parent.transform as RectTransform, 
                    _inputService.GetMousePosition(), 
                    _cameraProvider.MainCamera, 
                    out Vector2 localPoint);
                
                _draggedTransform.localPosition = localPoint;
            }
        }

        public void EndDrag()
        {
            ResetDraggedItemPosition();
        }
        
        private void ResetDraggedItemPosition()
        {
            _draggedTransform.SetParent(_lastTransformParent);
            _draggedTransform.SetSiblingIndex(_lastSiblingIndex);
            _draggedTransform.anchoredPosition = _lastPosition;
        }
    }
}