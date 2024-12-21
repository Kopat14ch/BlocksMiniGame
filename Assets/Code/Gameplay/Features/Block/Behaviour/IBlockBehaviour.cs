using Code.Gameplay.Common;
using Code.Gameplay.Features.Block.Data;
using Code.Gameplay.Features.Drag;
using UnityEngine;

namespace Code.Gameplay.Features.Block.Behaviour
{
    public interface IBlockBehaviour : IDraggable, IDestructible
    {
        public Vector3 PrePosition { get; }
        BlockData Data { get; }
        
        public RectTransform GetRectTransform();
        public void SetPrePosition(Vector3 position);
    }
}