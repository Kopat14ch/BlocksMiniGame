using Code.Gameplay.Features.Saver.Data;

namespace Code.Gameplay.Features.Saver.Services
{
    public interface ISaverService
    {
        SaveData Data { get; }
        void Save();
    }
}