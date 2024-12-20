using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Features.Drag
{
    public interface IDraggable : IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public RectTransform GetRectTransform();
    }
}