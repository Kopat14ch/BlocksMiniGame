using Code.Gameplay.Common;
using Code.Gameplay.Features.Block.Factory;
using Code.Gameplay.Features.Block.Services;
using Code.Gameplay.Features.Debugger.Behaviour;
using Code.Gameplay.Features.Drag.Behaviour;
using Code.Gameplay.Features.Drag.Services;
using Code.Gameplay.Features.DropZone.Behaviours;
using Code.Gameplay.Features.DropZone.Services;
using Code.Gameplay.Features.Level.Data;
using Code.Gameplay.Features.Level.Services;
using Code.Gameplay.Features.Tower.Services;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private LevelData _levelData;
        [SerializeField] private DebugBehaviour _debugBehaviour;
        [SerializeField] private DragContainerBehaviour _dragContainerBehaviour;
        [SerializeField] private DropZoneBehaviour[] _dropZoneBehaviour;
        [SerializeField] private ClearButton _clearButton;
        
        public override void InstallBindings()
        {
            BindBlock();
            BindLevel();
            BindInfrastructure();
            BindTower();
            BindDebug();
            BindClear();
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
        
        private void BindTower()
        {
            Container.BindInterfacesTo<TowerService>().AsSingle();
        }

        private void BindDebug()
        {
            Container.Bind<IDebugBehaviour>().To<DebugBehaviour>().FromInstance(_debugBehaviour).AsSingle();
        }

        private void BindClear()
        {
            Container.Bind<IClearButton>().To<ClearButton>().FromInstance(_clearButton).AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IDragInit>().Init(_dragContainerBehaviour, Container.Resolve<IBlockFactory>());
            Container.Resolve<IDropZoneInit>().Init(_dropZoneBehaviour);
        }
    }
}