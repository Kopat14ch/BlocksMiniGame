using System;
using UnityEngine;

namespace Code.Gameplay.Features.Block.Data
{
    [Serializable]
    public class BlockSaveData
    {
        public BlockData Data;
        public Vector3 Position;

        public BlockSaveData()
        {
            Data = new BlockData();
            Position = Vector3.zero;
        }
    }
}