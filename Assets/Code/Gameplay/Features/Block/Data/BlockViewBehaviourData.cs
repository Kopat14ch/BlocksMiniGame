using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Gameplay.Features.Block.Data
{
    [Serializable]
    public struct BlockViewBehaviourData
    {
        [field: SerializeField] public Image Image { get; private set; }
    }
}