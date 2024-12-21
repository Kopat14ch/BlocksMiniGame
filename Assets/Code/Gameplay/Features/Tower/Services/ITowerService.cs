using Code.Gameplay.Features.Block.Behaviour;
using UnityEngine;

namespace Code.Gameplay.Features.Tower.Services
{
    public interface ITowerService
    {
        bool HaveBlock { get; }
        void Init(RectTransform currentTransform);
        IBlockBehaviour GetLastBlock();
        bool Contains(IBlockBehaviour blockBehaviour);
        bool IsIntersectingWithAny(RectTransform draggableTransform);
        void AddBlock(IBlockBehaviour blockBehaviour);
    }
}