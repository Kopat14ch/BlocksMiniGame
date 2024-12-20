using Code.Gameplay.Features.Block.Data;
using Code.Gameplay.Features.Block.Factory;
using Code.Gameplay.Features.Level.Config;
using Code.Gameplay.Features.Level.Data;
using Code.Gameplay.StaticData;
using Zenject;

namespace Code.Gameplay.Features.Level.Services
{
    public class LevelService : IInitializable
    {
        private readonly ILevelData _data;
        private readonly ILevelConfig _config;
        private readonly IBlockFactory _blockFactory;

        public LevelService(ILevelData data, IStaticDataService staticDataService, IBlockFactory blockFactory)
        {
            _data = data;
            _config = staticDataService.GetLevelConfig();
            _blockFactory = blockFactory;
        }

        public void Initialize()
        {
            Load();
        }
        
        private void Load()
        {
            foreach (BlockData blockData in _config.GetBlockDatas)
                _blockFactory.Create(blockData, _data.ScrollContainer);
        }
    }
}