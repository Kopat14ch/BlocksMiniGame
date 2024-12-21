using Code.Gameplay.Features.DropZone.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Features.DropZone.Services
{
    public interface IDropZoneService
    {
        IDropZoneBehaviour FindDropZone(RectTransform targetTransform);
    }
}