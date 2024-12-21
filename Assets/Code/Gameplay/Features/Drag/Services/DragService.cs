using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Features.Drag.Behaviour;
using Code.Gameplay.Features.UserInput.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Code.Gameplay.Features.Drag.Services
{
    public class DragService : IDragService, IDragInit
    {
        private readonly IInputService _inputService;
        private readonly ICameraProvider _cameraProvider;

        private IDragContainerBehaviour _container;
        private RectTransform _draggedTransform;
        private Transform _lastTransformParent;
        private Vector3 _lastPosition;
        private Vector3 _dragStartPosition;
        private int _lastSiblingIndex;
        private bool _isBlockDragging;
        private bool _isScrollDragging;

        private IDraggable _draggable;
        private ScrollRect _scrollRect;

        private const float DragThresholdX = 4f;
        private const float DragThresholdY = 1f;

        public DragService(IInputService inputService, ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _inputService = inputService;
        }

        public void Init(IDragContainerBehaviour dragContainerBehaviour)
        {
            _container = dragContainerBehaviour;
        }
        
        public void StartDrag(IDraggable draggable, PointerEventData eventData)
        {
            _draggable = draggable;
            _scrollRect = draggable.GetRectTransform().GetComponentInParent<ScrollRect>();

            if (_scrollRect == null)
                throw new System.Exception("ScrollRect not found");

            _draggedTransform = _draggable.GetRectTransform();
            _lastTransformParent = _draggedTransform.parent;
            _lastSiblingIndex = _draggedTransform.GetSiblingIndex();
            _lastPosition = _draggedTransform.anchoredPosition;
            _dragStartPosition = _inputService.GetMousePosition();
            _isBlockDragging = false;
            _isScrollDragging = false;
            
            _scrollRect.OnBeginDrag(eventData);
        }

        public void Dragging(PointerEventData eventData)
        {
            if (_draggedTransform != null)
            {
                Vector3 currentMousePosition = _inputService.GetMousePosition();
                Vector3 dragDelta = currentMousePosition - _dragStartPosition;

                float absDeltaX = Mathf.Abs(dragDelta.x);
                float absDeltaY = Mathf.Abs(dragDelta.y);


                if (_isScrollDragging)
                {
                    _scrollRect.OnDrag(eventData);
                    return;
                }
                
                if (_isBlockDragging == false && _isScrollDragging == false)
                {
                    if (absDeltaY > absDeltaX && absDeltaY > DragThresholdY)
                    {
                        _isBlockDragging = true;
                        _draggable.GetRectTransform().SetParent(_container.GetTransform());
                    }
                    else if (absDeltaX > absDeltaY && absDeltaX > DragThresholdX)
                    {
                        _scrollRect.OnDrag(eventData);
                        _isScrollDragging = true;
                    }
                    else
                    {
                        return;
                    }
                }

                if (_isBlockDragging)
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        _draggedTransform.parent.transform as RectTransform, 
                        _inputService.GetMousePosition(), 
                        _cameraProvider.MainCamera, 
                        out Vector2 localPoint);
                
                    _draggedTransform.localPosition = localPoint;
                    
                    return;
                }
            }
        }

        public void EndDrag(PointerEventData eventData)
        {
            _scrollRect.OnEndDrag(eventData);
            
            ResetDraggedItemPosition();

            _draggable = null;
            _scrollRect = null;
            _lastTransformParent = null;
        }
        
        private void ResetDraggedItemPosition()
        {
            _draggedTransform.SetParent(_lastTransformParent);
            _draggedTransform.SetSiblingIndex(_lastSiblingIndex);
            _draggedTransform.anchoredPosition = _lastPosition;
            
        }
    }
}