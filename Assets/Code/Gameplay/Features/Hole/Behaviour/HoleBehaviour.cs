using System;
using UnityEngine;

namespace Code.Gameplay.Features.Hole.Behaviour
{
    [RequireComponent(typeof(RectTransform))]
    public class HoleBehaviour : MonoBehaviour
    {
        [field: SerializeField] public RectTransform Content { get; private set; }

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public RectTransform GetRectTransform() =>
            _rectTransform;
    }
}