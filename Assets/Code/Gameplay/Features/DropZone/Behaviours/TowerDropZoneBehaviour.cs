using System;
using Code.Common.Extensions;
using Code.Gameplay.Common;
using Code.Gameplay.Features.Block.Behaviour;
using Code.Gameplay.Features.Block.Data;
using Code.Gameplay.Features.Block.Factory;
using Code.Gameplay.Features.Debugger.Behaviour;
using Code.Gameplay.Features.Saver.Services;
using Code.Gameplay.Features.Tower.Services;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.DropZone.Behaviours
{
    public class TowerDropZoneBehaviour : DropZoneBehaviour
    {
        [SerializeField] private RectTransform _bottomTransformPoint;
        [SerializeField] private float _durationToBottom;

        private ITowerService _towerService;
        private ISaverService _saverService;
        private IBlockFactory _blockFactory;
        private IDebugBehaviour _debugBehaviour;

        [Inject]
        private void Construct(ITowerService towerService, ISaverService saverService, IBlockFactory blockFactory, IDebugBehaviour debugBehaviour)
        {
            _blockFactory = blockFactory;
            _towerService = towerService;
            _saverService = saverService;
            _debugBehaviour = debugBehaviour;
        }

        protected override void Awake()
        {
            base.Awake();
            
            _towerService.Init(RectTransform);
            
            if (_saverService.Data != null)
            {
                foreach (BlockSaveData blockSaveData in _saverService.Data.BlocksData)
                {
                    _towerService.AddBlock(_blockFactory.Create(blockSaveData.Data, RectTransform, blockSaveData.Position, true));
                }
            }
        }

        public override bool TryDrop(IBlockBehaviour blockBehaviour)
        {
            if (_towerService.Contains(blockBehaviour) || (_towerService.HaveBlock && IsAboveScreenHeight(_towerService.GetLastBlock().GetRectTransform())))
                return false;
            
            RectTransform draggableTransform = blockBehaviour.GetRectTransform();
            
            if (_towerService.HaveBlock == false)
            {
                _towerService.AddBlock(blockBehaviour);
                
                float targetBottomY = _bottomTransformPoint.localPosition.y;
                Vector3 prePosition = draggableTransform.AnimateToY(targetBottomY, _durationToBottom);
                
                _saverService.Data.BlocksData.Add(new BlockSaveData()
                {
                    Data = blockBehaviour.Data,
                    Position = prePosition
                });
                
                _saverService.Save();
                
                blockBehaviour.SetPrePosition(prePosition);
                
                _debugBehaviour.SetLog("Cube placed");

                return true;
            }
            
            if (_towerService.IsIntersectingWithAny(draggableTransform))
            {
                IBlockBehaviour lastBlock = _towerService.GetLastBlock();
                
                _towerService.AddBlock(blockBehaviour);
                
               Vector3 prePosition = draggableTransform.AnimateTopWithBounce(lastBlock, _durationToBottom);
                
               _saverService.Data.BlocksData.Add(new BlockSaveData()
               {
                   Data = blockBehaviour.Data,
                   Position = prePosition
               });
               
               _saverService.Save();
               
                blockBehaviour.SetPrePosition(prePosition);
                
                _debugBehaviour.SetLog("Cube placed");
                
                return true;
            }

            return false;
        }


        
        private bool IsAboveScreenHeight(RectTransform draggableTransform)
        {
            float topEdge = draggableTransform.GetTopEdge();

            bool expr = topEdge > RectTransform.GetTopEdge();
            
            if (expr)
                _debugBehaviour.SetLog("Height limit");
            
            return expr;
        }
    }
}