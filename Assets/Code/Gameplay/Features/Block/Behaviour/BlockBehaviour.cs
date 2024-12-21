using Code.Gameplay.Features.Block.Data;
using Code.Gameplay.Features.Drag;
using Code.Gameplay.Features.Drag.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.Gameplay.Features.Block.Behaviour
{
    [RequireComponent(typeof(BlockViewBehaviour), typeof(RectTransform))]
    public class BlockBehaviour : MonoBehaviour, IDraggable
    {
        private BlockViewBehaviour _viewBehaviour;
        private RectTransform _rectTransform;
        private IDragService _dragService;

        [Inject]
        private void Construct(IDragService dragService)
        {
            _dragService = dragService;
        }
        
        private void Awake()
        {
            _viewBehaviour = GetComponent<BlockViewBehaviour>();
            _rectTransform = GetComponent<RectTransform>();
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragService.StartDrag(this, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragService.Dragging(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragService.EndDrag(eventData);
        }

        public void Init(BlockData data)
        {
            _viewBehaviour.Init(data);
        }

        public RectTransform GetRectTransform() => 
            _rectTransform;


    }
}