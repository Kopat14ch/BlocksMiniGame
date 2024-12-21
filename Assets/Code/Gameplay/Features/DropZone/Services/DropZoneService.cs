using Code.Gameplay.Features.DropZone.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Features.DropZone.Services
{
    public class DropZoneService : IDropZoneService, IDropZoneInit
    {
        private IDropZoneBehaviour[] _dropZones;
        
        public void Init(IDropZoneBehaviour[] dropZones)
        {
            _dropZones = dropZones;
        }

        public IDropZoneBehaviour FindDropZone(RectTransform targetTransform)
        {
            foreach (var dropZone in _dropZones)
                if (dropZone.IsInsideZone(targetTransform))
                    return dropZone;
            
            return null;
        }
    }
}