using DG.Tweening;
using UnityEngine;

namespace Code.Common.Extensions
{
    public static class GameObjectExtension
    {
        public static void DestroyAnimate(this GameObject monoBehaviour, float duration = 0.5f)
        {
            monoBehaviour.transform.DOScale(Vector3.zero, duration).OnComplete(() => Object.Destroy(monoBehaviour.gameObject));
        }
    }
}