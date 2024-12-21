using Code.Gameplay.Features.Block.Behaviour;
using UnityEngine;

namespace Code.Gameplay.Features.DropZone.Behaviours
{
    public interface IDropZoneBehaviour
    {
        public bool TryDrop(IBlockBehaviour blockBehaviour);
        
        bool IsInsideZone(RectTransform rectTransform);
    }
}