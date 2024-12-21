using System;
using System.Collections.Generic;
using Code.Gameplay.Features.Block.Data;

namespace Code.Gameplay.Features.Saver.Data
{
    [Serializable]
    public class SaveData
    {
        public List<BlockSaveData> BlocksData;

        public SaveData()
        {
            BlocksData = new List<BlockSaveData>();
        }
    }
}