using Code.Gameplay.Features.Block.Factory;
using Code.Gameplay.Features.Block.Services;
using Code.Gameplay.Features.Level.Data;
using Code.Gameplay.Features.Level.Services;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private LevelData _levelData;
        
        public override void InstallBindings()
        {
            BindBlock();
            BindLevel();
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
    }
}