using Code.Gameplay.Features.Block.Behaviour;
using Code.Gameplay.Features.Block.Data;
using Code.Infrastructure.AssetManagement;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Gameplay.Features.Block.Factory
{
    public class BlockFactory : IBlockFactory
    {
        private readonly BlockBehaviour _prefab;
        
        public BlockFactory(IAssetProvider assetProvider)
        {
            _prefab = assetProvider.LoadAsset<BlockBehaviour>("");

            if (_prefab == null)
                throw new System.Exception("Block prefab not found");
        }

        public BlockBehaviour Create(BlockData data, Transform parent)
        {
            BlockBehaviour blockBehaviour = Object.Instantiate(_prefab, Vector3.zero, quaternion.identity, parent);
            blockBehaviour.Init(data);
            
            return blockBehaviour;
        }
    }
}