using Code.Gameplay.Features.Level.Config;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private LevelConfig _levelConfig;
        
        public void LoadAll()
        {
            LoadLevelConfig();
        }
        
        public ILevelConfig GetLevelConfig() =>
            _levelConfig;

        private void LoadLevelConfig() => 
            _levelConfig = Resources.Load<LevelConfig>("Configs/Level/LevelConfig");
    }
}