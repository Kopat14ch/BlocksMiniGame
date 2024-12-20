using Code.Gameplay.Features.Block.Data;

namespace Code.Gameplay.Features.Block.Services
{
    public interface IBlockViewService
    {
        void Draw(BlockData data, BlockViewBehaviourData behaviourData);
    }
}