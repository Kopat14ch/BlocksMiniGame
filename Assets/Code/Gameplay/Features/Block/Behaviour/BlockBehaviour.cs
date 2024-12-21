using System;
using Code.Common.Extensions;
using Code.Gameplay.Common;
using Code.Gameplay.Features.Block.Data;
using Code.Gameplay.Features.Debugger.Behaviour;
using Code.Gameplay.Features.Drag;
using Code.Gameplay.Features.Drag.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Object = UnityEngine.Object;

namespace Code.Gameplay.Features.Block.Behaviour
{
    [RequireComponent(typeof(BlockViewBehaviour), typeof(RectTransform))]
    public class BlockBehaviour : MonoBehaviour, IBlockBehaviour
    {
        private BlockViewBehaviour _viewBehaviour;
        private RectTransform _rectTransform;
        private IDragService _dragService;
        private bool _isDestroying;
        private Vector3 _previousPosition;
        private IDebugBehaviour _debugBehaviour;

        public event Action<IDestructible> Destroyed;  
        
        public bool IsCreated { get; private set; }

        public Vector3 PrePosition { get; private set; }

        public BlockData Data { get; private set; }

        [Inject]
        private void Construct(IDragService dragService, IDebugBehaviour debugBehaviour)
        {
            _debugBehaviour = debugBehaviour;
            _dragService = dragService;
        }
        
        private void Awake()
        {
            _viewBehaviour = GetComponent<BlockViewBehaviour>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnDestroy()
        {
            _debugBehaviour.SetLog("Cube destroyed");
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isDestroying == false)
                _dragService.StartDrag(this, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isDestroying == false) 
                _dragService.Dragging(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_isDestroying == false) 
                _dragService.EndDrag(eventData);
        }

        public void Init(BlockData data)
        {
            Data = data;
            _viewBehaviour.Init(data);
        }

        public void Destroy(bool isAnimation = true, bool invokeEvent = true, bool onlyInvoke = false)
        {
            _isDestroying = true;

            if (onlyInvoke)
            {
                Destroyed?.Invoke(this);
            }
            else
            {
                if (invokeEvent)
                    Destroyed?.Invoke(this);
            
                if (isAnimation)
                    gameObject.DestroyAnimate();
                else
                    Object.Destroy(gameObject);
            }
        }

        public void Create() => 
            IsCreated = true;

        public RectTransform GetRectTransform() => 
            _rectTransform;

        public void SetPrePosition(Vector3 position) =>
            PrePosition = position;
    }
}