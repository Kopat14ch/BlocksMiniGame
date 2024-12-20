using Code.Gameplay.Features.Level.Config;

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        ILevelConfig GetLevelConfig();
    }
}