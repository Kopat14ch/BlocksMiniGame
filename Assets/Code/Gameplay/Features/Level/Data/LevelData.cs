using System;
using UnityEngine;

namespace Code.Gameplay.Features.Level.Data
{
    [Serializable]
    public struct LevelData : ILevelData
    {
       [field: SerializeField] public RectTransform ScrollContainer { get; private set; }
    }
}