namespace Code.Gameplay.Features.Drag.Services
{
    public interface IDragService
    {
        void StartDrag(IDraggable draggable);
        void Dragging();
        void EndDrag();
    }
}