using Code.Gameplay.Features.Block.Behaviour;
using Code.Gameplay.Features.Block.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Block.Factory
{
    public interface IBlockFactory
    {
        BlockBehaviour Create(BlockData data, Transform parent);
    }
}