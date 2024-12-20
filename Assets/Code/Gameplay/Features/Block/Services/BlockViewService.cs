using Code.Gameplay.Features.Block.Data;

namespace Code.Gameplay.Features.Block.Services
{
    public class BlockViewService : IBlockViewService
    {
        public void Draw(BlockData data, BlockViewBehaviourData behaviourData)
        {
            behaviourData.Image.color = data.Color;
        }
    }
}