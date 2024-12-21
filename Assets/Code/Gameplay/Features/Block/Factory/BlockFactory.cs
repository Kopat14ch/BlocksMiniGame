using Code.Common.Extensions;
using Code.Gameplay.Features.Block.Behaviour;
using Code.Gameplay.Features.Block.Data;
using Code.Infrastructure.AssetManagement;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Block.Factory
{
    public class BlockFactory : IBlockFactory
    {
        private readonly BlockBehaviour _prefab;
        private readonly DiContainer _container;

        public BlockFactory(IAssetProvider assetProvider, DiContainer container)
        {
            _container = container;
            _prefab = assetProvider.LoadAsset<BlockBehaviour>("Block/UIBlock");

            if (_prefab == null)
                throw new System.Exception("Block prefab not found");
        }

        public BlockBehaviour Create(BlockData data, Transform parent, Vector3 position = default, bool created = false)
        {
            BlockBehaviour createdBlockBehaviour = _container.InstantiatePrefabForComponent<BlockBehaviour>(_prefab, Vector3.zero, Quaternion.identity, parent);
            createdBlockBehaviour.Init(data);
            createdBlockBehaviour.SetDefaultTransform();
            createdBlockBehaviour.GetRectTransform().localPosition = position;
            
            createdBlockBehaviour.SetPrePosition(position);

            if (created)
                createdBlockBehaviour.Create();
            
            return createdBlockBehaviour;
        }
    }
}