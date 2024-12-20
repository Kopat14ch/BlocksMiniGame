using System.Collections.Generic;
using Code.Gameplay.Features.Block.Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Features.Level.Config
{
    [CreateAssetMenu(menuName = "Configs/LevelConfig", fileName = "LevelConfig", order = 51)]
    public class LevelConfig : ScriptableObject, ILevelConfig
    {
        [SerializeField] private List<BlockData> _blockDatas;

        public IEnumerable<BlockData> GetBlockDatas => 
            _blockDatas;
    }
}