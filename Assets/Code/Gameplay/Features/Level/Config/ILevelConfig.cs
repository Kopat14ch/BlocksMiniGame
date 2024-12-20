using System.Collections.Generic;
using Code.Gameplay.Features.Block.Data;

namespace Code.Gameplay.Features.Level.Config
{
    public interface ILevelConfig
    {
        IEnumerable<BlockData> GetBlockDatas { get; }
    }
}