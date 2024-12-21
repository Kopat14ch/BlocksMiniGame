using UnityEngine.EventSystems;

namespace Code.Gameplay.Features.Drag.Services
{
    public interface IDragService
    {
        void StartDrag(IDraggable draggable, PointerEventData eventData);
        void Dragging(PointerEventData eventData);
        void EndDrag(PointerEventData eventData);
    }
}