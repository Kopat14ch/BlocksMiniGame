using Code.Common.Extensions;
using Code.Common.Utils;
using Code.Gameplay.Features.Block.Behaviour;
using Code.Gameplay.Features.Debugger.Behaviour;
using Code.Gameplay.Features.Hole.Behaviour;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.DropZone.Behaviours
{
    public class HoleDropZoneBehaviour : DropZoneBehaviour
    {
        [SerializeField] private RectTransform _bottomTargetPoint;
        [SerializeField] private HoleBehaviour _holeBehaviour;
        [SerializeField] private float _durationToBottom;
        
        private IDebugBehaviour _debugBehaviour;

        [Inject]
        private void Construct(IDebugBehaviour debugBehaviour)
        {
            _debugBehaviour = debugBehaviour;
        }
        
        
        public override bool TryDrop(IBlockBehaviour blockBehaviour)
        {
            if (RectTransformUtil.IsInside(_holeBehaviour.GetRectTransform(), blockBehaviour.GetRectTransform()))
            {
                blockBehaviour.GetRectTransform().SetParent(_holeBehaviour.Content);
                blockBehaviour.GetRectTransform().AnimateToY(_bottomTargetPoint.localPosition.y, _durationToBottom,
                    () =>
                    {
                        blockBehaviour.Destroy(false, false);
                    });
                
                blockBehaviour.Destroy(onlyInvoke: true);
                
                _debugBehaviour.SetLog("Cube in hole");
            }
            else
            {
                blockBehaviour.GetRectTransform().SetParent(RectTransform);
                blockBehaviour.Destroy();
            }

            return true;
        }
    }
}