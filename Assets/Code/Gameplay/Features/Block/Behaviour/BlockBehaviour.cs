using Code.Gameplay.Features.Block.Data;
using Code.Gameplay.Features.Block.Services;
using UnityEngine;

namespace Code.Gameplay.Features.Block.Behaviour
{
    [RequireComponent(typeof(BlockViewBehaviour))]
    public class BlockBehaviour : MonoBehaviour
    {
        private BlockViewService _viewService;
        private BlockViewBehaviour _viewBehaviour;
        
        private void Awake()
        {
            _viewBehaviour = GetComponent<BlockViewBehaviour>();
        }

        public void Init(BlockData data)
        {
            _viewBehaviour.Init(data);
        }
    }
}