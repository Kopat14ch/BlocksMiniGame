using System;
using UnityEngine;

namespace Code.Gameplay.Features.Block.Data
{
    [Serializable]
    public struct BlockData
    {
        [field: SerializeField] public Color Color { get; private set; }
    }
}