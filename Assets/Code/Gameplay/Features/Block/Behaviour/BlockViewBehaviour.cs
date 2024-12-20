using Code.Gameplay.Features.Block.Data;
using Code.Gameplay.Features.Block.Services;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Features.Block.Behaviour
{
    public class BlockViewBehaviour : MonoBehaviour
    {
        [SerializeField] private BlockViewBehaviourData _data;
        
        private BlockViewService _viewService;

        [Inject]
        private void Construct(BlockViewService viewService)
        {
            _viewService = viewService;
        }

        public void Init(BlockData data)
        {
            _viewService.Draw(data, _data);
        }
    }
}