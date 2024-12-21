using System;
using System.Collections.Generic;
using Code.Common.Extensions;
using Code.Gameplay.Common;
using Code.Gameplay.Features.Block.Behaviour;
using Code.Gameplay.Features.Saver.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Tower.Services
{
    public class TowerService : ITowerService, IDisposable
    {
        private readonly List<IBlockBehaviour> _blocks;
        private readonly ISaverService _saverService;
        private readonly IClearButton _clearButton;

        private RectTransform _dropZoneTransform;

        public bool HaveBlock => _blocks.Count > 0;
        
        public TowerService(ISaverService saverService, IClearButton clearButton)
        {
            _saverService = saverService;
            _clearButton = clearButton;
            _blocks = new List<IBlockBehaviour>();
            
            _clearButton.Clicked += OnClearButtonClick;
        }

        public void Init(RectTransform dropZoneTransform)
        {
            _dropZoneTransform = dropZoneTransform;
        }
        
        public void Dispose()
        {
            _clearButton.Clicked -= OnClearButtonClick;
        }

        public IBlockBehaviour GetLastBlock() => 
            _blocks[^1];

        public bool Contains(IBlockBehaviour blockBehaviour) => 
            _blocks.Contains(blockBehaviour);
        
        public bool IsIntersectingWithAny(RectTransform draggableTransform)
        {
            Rect draggableRect = draggableTransform.GetWorldRect();

            foreach (IBlockBehaviour blockBehaviour in _blocks)
            {
                Rect existingRect = blockBehaviour.GetRectTransform().GetWorldRect();
                
                if (draggableRect.Overlaps(existingRect))
                {
                    return true; 
                }
            }

            return false;
        }
        
        public void AddBlock(IBlockBehaviour blockBehaviour)
        {
            blockBehaviour.GetRectTransform().SetParent(_dropZoneTransform);
            _blocks.Add(blockBehaviour);
            
            _saverService.Save();
            
            blockBehaviour.Destroyed += OnBlockDestroyed;
        }
        
        private void OnBlockDestroyed(IDestructible destructible)
        {
            destructible.Destroyed -= OnBlockDestroyed;
            
            if (destructible is IBlockBehaviour blockBehaviour)
            {
                int removedIndex = _blocks.IndexOf(blockBehaviour);
            
                _blocks.Remove(blockBehaviour);
                
                _saverService.Data.BlocksData.RemoveAt(removedIndex);
            
                MoveBlocksDown(removedIndex);
                
                SaveData();
            }
        }
        
        private void MoveBlocksDown(int removedIndex)
        {
            for (int i = removedIndex; i < _blocks.Count; i++)
            {
                IBlockBehaviour blockBehaviour = _blocks[i];
                
                Vector3 newPosition = blockBehaviour.PrePosition;
                newPosition.y = blockBehaviour.PrePosition.y - blockBehaviour.GetRectTransform().rect.height;
                
                Vector3 prePosition = blockBehaviour.GetRectTransform().AnimateToY(newPosition.y);
                
                blockBehaviour.SetPrePosition(prePosition);
                
                SaveData();
            }
        }
        
        private void SaveData()
        {
            for (int i = 0; i < _blocks.Count; i++)
            {
                _saverService.Data.BlocksData[i].Data = _blocks[i].Data;
                _saverService.Data.BlocksData[i].Position = _blocks[i].PrePosition;
            }
            
            _saverService.Save();
        }
        
        private void OnClearButtonClick()
        {
            _saverService.Data.BlocksData.Clear();

            foreach (IBlockBehaviour blockBehaviour in _blocks)
                blockBehaviour.Destroy(false, false);
            
            _saverService.Save();
        }
    }
}