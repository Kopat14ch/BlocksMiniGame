using Code.Gameplay.Features.Block.Factory;
using Code.Gameplay.Features.Drag.Behaviour;

namespace Code.Gameplay.Features.Drag.Services
{
    public interface IDragInit
    {
        void Init(IDragContainerBehaviour dragContainerBehaviour, IBlockFactory blockFactory);
    }
}