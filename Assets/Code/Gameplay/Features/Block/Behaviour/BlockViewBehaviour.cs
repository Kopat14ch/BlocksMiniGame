using Code.Gameplay.Features.Block.Data;
using Code.Gameplay.Features.Block.Services;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Block.Behaviour
{
    public class BlockViewBehaviour : MonoBehaviour
    {
        [SerializeField] private BlockViewBehaviourData _data;
        
        private IBlockViewService _viewService;

        [Inject]
        private void Construct(IBlockViewService viewService)
        {
            _viewService = viewService;
        }

        public void Init(BlockData data)
        {
            _viewService.Draw(data, _data);
        }
    }
}