using UnityEngine;

namespace Code.Common.Utils
{
    public static class RectTransformUtil
    {
        public static bool IsInside(RectTransform current, RectTransform target)
        {
            if (current == null || target == null)
                return false;

            Vector3 worldPoint = target.position;
            Vector3 localPoint = current.InverseTransformPoint(worldPoint);

            return current.rect.Contains(new Vector2(localPoint.x, localPoint.y));
        }
    }
}