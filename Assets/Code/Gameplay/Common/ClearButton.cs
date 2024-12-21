using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Common
{
    [RequireComponent(typeof(Button))]
    public class ClearButton : MonoBehaviour, IClearButton
    {
        private Button _button;

        public event Action Clicked;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Clicked?.Invoke();
            
            PlayerPrefs.DeleteAll();
        }
    }
}
