using Code.Gameplay.Cameras.Provider;
using Code.Gameplay.Features.Drag.Services;
using Code.Gameplay.Features.UserInput.Services;
using Code.Gameplay.StaticData;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Loading;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {
        public override void InstallBindings()
        {
            BindCamera();
            BindInfrastructure();
            BindCommon();
            BindInput();
            BindDrag();
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

        public void Initialize()
        {
            Container.Resolve<IStaticDataService>().LoadAll();
            Container.Resolve<ISceneLoader>().LoadScene(Scenes.Game);
        }
    }
}