using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Features.Drag.Services;
using Code.Gameplay.Features.DropZone.Services;
using Code.Gameplay.Features.Saver.Services;
using Code.Gameplay.Features.UserInput.Services;
using Code.Gameplay.StaticData;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Loading;
using Lean.Localization;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {
        [SerializeField] private LeanLocalization _leanLocalization;
        
        public override void InstallBindings()
        {
            BindCamera();
            BindInfrastructure();
            BindCommon();
            BindInput();
            BindDrag();
            BindDropZone();
            BindSaver();
            BindLeanLocalization();
        }

        private void BindCamera()
        {
            Container.Bind<ICameraProvider>().To<CameraProvider>().AsSingle();
        }
        
        private void BindInfrastructure()
        {
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
        }

        private void BindCommon()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
        }

        private void BindInput()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
        }

        private void BindDrag()
        {
            Container.BindInterfacesTo<DragService>().AsSingle();
        }

        private void BindDropZone()
        {
            Container.BindInterfacesTo<DropZoneService>().AsSingle();
        }

        private void BindSaver()
        {
            Container.BindInterfacesTo<SaverService>().AsSingle();
        }

        private void BindLeanLocalization()
        {
            Container.Bind<LeanLocalization>().AsSingle();
        }
        
        public void Initialize()
        {
            Container.Resolve<IStaticDataService>().LoadAll();
            Container.Resolve<ISaverInit>().Load();
            Container.Resolve<ISceneLoader>().LoadScene(Scenes.Game);
        }
    }
}