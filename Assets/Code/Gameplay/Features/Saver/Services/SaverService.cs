using Code.Gameplay.Features.Saver.Data;
using UnityEngine;

namespace Code.Gameplay.Features.Saver.Services
{
    public class SaverService : ISaverService, ISaverInit
    {
        private const string SaveDataKey = "SaveData";
        
        public SaveData Data { get; private set; }

        public SaverService()
        {
            Data = new SaveData();
        }
        
        public void Save()
        {
            PlayerPrefs.SetString(SaveDataKey, JsonUtility.ToJson(Data));
            PlayerPrefs.Save();
        }
        
        public void Load()
        {
            if (PlayerPrefs.HasKey(SaveDataKey) == false) 
                return;
            
            Data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SaveDataKey));
        }
    }
}