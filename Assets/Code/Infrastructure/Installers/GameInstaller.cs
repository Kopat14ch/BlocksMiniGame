using Code.Gameplay.Features.Block.Factory;
using Code.Gameplay.Features.Block.Services;
using Code.Gameplay.Features.Drag.Behaviour;
using Code.Gameplay.Features.Drag.Services;
using Code.Gameplay.Features.Level.Data;
using Code.Gameplay.Features.Level.Services;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private LevelData _levelData;
        [SerializeField] private DragContainerBehaviour _dragContainerBehaviour;
        
        public override void InstallBindings()
        {
            BindBlock();
            BindLevel();
            BindInfrastructure();
        }

        private void BindLevel()
        {
            Container.Bind<ILevelData>().To<LevelData>().FromInstance(_levelData).AsSingle();
            Container.BindInterfacesTo<LevelService>().AsSingle();
        }
        
        private void BindBlock()
        {
            Container.Bind<IBlockViewService>().To<BlockViewService>().AsSingle();
            Container.Bind<IBlockFactory>().To<BlockFactory>().AsSingle();
        }

        private void BindInfrastructure()
        {
            Container.BindInterfacesTo<GameInstaller>().FromInstance(this).AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IDragInit>().Init(_dragContainerBehaviour);
        }
    }
}