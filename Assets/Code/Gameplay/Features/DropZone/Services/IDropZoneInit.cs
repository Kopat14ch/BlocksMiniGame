using Code.Gameplay.Features.DropZone.Behaviours;

namespace Code.Gameplay.Features.DropZone.Services
{
    public interface IDropZoneInit
    {
        void Init(IDropZoneBehaviour[] dropZones);
    }
}