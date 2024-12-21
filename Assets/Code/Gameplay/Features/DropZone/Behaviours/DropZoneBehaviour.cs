using Code.Common.Utils;
using Code.Gameplay.Features.Block.Behaviour;
using UnityEngine;

namespace Code.Gameplay.Features.DropZone.Behaviours
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class DropZoneBehaviour : MonoBehaviour, IDropZoneBehaviour
    {
        protected RectTransform RectTransform;
        
        protected virtual void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }

        public abstract bool TryDrop(IBlockBehaviour blockBehaviour);

        public bool IsInsideZone(RectTransform rectTransform)
        {
            return RectTransformUtil.IsInside(RectTransform, rectTransform);
        }
    }
}
