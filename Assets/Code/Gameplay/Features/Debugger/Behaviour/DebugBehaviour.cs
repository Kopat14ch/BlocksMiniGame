using Lean.Localization;
using TMPro;
using UnityEngine;

namespace Code.Gameplay.Features.Debugger.Behaviour
{
    public class DebugBehaviour : MonoBehaviour, IDebugBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        public void SetLog(string key) 
        {
            string localizedText = LeanLocalization.GetTranslationText(key);
            
            _text.text = $"{localizedText} (Debug)";
        }
    }
}
