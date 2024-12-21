using Code.Gameplay.Features.Block.Behaviour;
using UnityEngine.EventSystems;

namespace Code.Gameplay.Features.Drag.Services
{
    public interface IDragService
    {
        void StartDrag(IBlockBehaviour blockBehaviour, PointerEventData eventData);
        void Dragging(PointerEventData eventData);
        void EndDrag(PointerEventData eventData);
    }
}