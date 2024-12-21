using Code.Common.Extensions;
using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Common;
using Code.Gameplay.Features.Block.Behaviour;
using Code.Gameplay.Features.Block.Factory;
using Code.Gameplay.Features.Drag.Behaviour;
using Code.Gameplay.Features.DropZone.Behaviours;
using Code.Gameplay.Features.DropZone.Services;
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
        private readonly IDropZoneService _dropZoneService;
        
        private IBlockFactory _blockFactory;
        private IDragContainerBehaviour _container;
        private RectTransform _draggedTransform;
        private Transform _lastTransformParent;
        private Vector3 _lastPosition;
        private Vector3 _dragStartPosition;
        private int _lastSiblingIndex;
        private bool _isBlockDragging;
        private bool _isScrollDragging;

        private IBlockBehaviour _blockBehaviour;
        private ScrollRect _scrollRect;

        private const float DragThresholdX = 4f;
        private const float DragThresholdY = 1f;

        public DragService(IInputService inputService, ICameraProvider cameraProvider, IDropZoneService dropZoneService)
        {
            _cameraProvider = cameraProvider;
            _dropZoneService = dropZoneService;
            _inputService = inputService;
        }

        public void Init(IDragContainerBehaviour dragContainerBehaviour, IBlockFactory blockFactory)
        {
            _container = dragContainerBehaviour;
            _blockFactory = blockFactory;
        }
        
        public void StartDrag(IBlockBehaviour blockBehaviour, PointerEventData eventData)
        {
            _blockBehaviour = blockBehaviour;
            _scrollRect = blockBehaviour.GetRectTransform().GetComponentInParent<ScrollRect>();
            _dragStartPosition = _inputService.GetMousePosition();
            
            
            
            _isBlockDragging = false;
            _isScrollDragging = false;

            if (_scrollRect != null)
                _scrollRect.OnBeginDrag(eventData);
        }

        public void Dragging(PointerEventData eventData)
        {
            Vector3 currentMousePosition = _inputService.GetMousePosition();
            
            if (_scrollRect != null)
            {
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
                        if (_blockBehaviour.IsCreated == false)
                        {
                            switch (_blockBehaviour)
                            {
                                case BlockBehaviour blockBehaviour:
                                    _blockBehaviour = _blockFactory.Create(blockBehaviour.Data, _container.GetTransform(), created: true);
                                    break;
                            }
                        }

                        _isBlockDragging = true;
                    }
                    else if (absDeltaX > absDeltaY && absDeltaX > DragThresholdX)
                    {
                        if (_scrollRect != null)
                        {
                            _scrollRect.OnDrag(eventData);
                            _isScrollDragging = true;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            if (_scrollRect == null || _isBlockDragging)
            {
                SetDraggable();
                _blockBehaviour.GetRectTransform().SetParent(_container.GetTransform());
                
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _draggedTransform.parent.transform as RectTransform,
                    currentMousePosition,
                    _cameraProvider.MainCamera,
                    out Vector2 localPoint);

                _draggedTransform.localPosition = localPoint;
            }
        }

        public void EndDrag(PointerEventData eventData)
        {
            if (_isScrollDragging)
            {
                _scrollRect.OnEndDrag(eventData);
            }
            else if (_scrollRect == null || _isBlockDragging)
            {
                IDropZoneBehaviour dropZoneBehaviour = _dropZoneService.FindDropZone(_blockBehaviour.GetRectTransform());

                if (_blockBehaviour is IDestructible destructible)
                {
                    if (dropZoneBehaviour == null || dropZoneBehaviour.TryDrop(_blockBehaviour) == false)
                    {
                        destructible.Destroy();
                    }
                }
                else
                {
                    ResetDraggedItemPosition();
                }
            }
            
            _blockBehaviour = null;
            _scrollRect = null;
            _lastTransformParent = null;
            _draggedTransform = null;
        }
        
        private void ResetDraggedItemPosition()
        {
            _draggedTransform.SetParent(_lastTransformParent);
            _draggedTransform.SetSiblingIndex(_lastSiblingIndex);
            _draggedTransform.anchoredPosition = _lastPosition;
        }

        private void SetDraggable()
        {
            _draggedTransform = _blockBehaviour.GetRectTransform();
            _lastTransformParent = _draggedTransform.parent;
            _lastSiblingIndex = _draggedTransform.GetSiblingIndex();
            _lastPosition = _draggedTransform.anchoredPosition;
        }
    }
}