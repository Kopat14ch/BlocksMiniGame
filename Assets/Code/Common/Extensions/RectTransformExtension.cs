using System;
using Code.Gameplay.Features.Block.Behaviour;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Common.Extensions
{
    public static class RectTransformExtension
    {
        public static float GetBottomEdge(this RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return corners[0].y;
        }
        
        public static float GetTopEdge(this RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return corners[1].y;
        }
        
        public static float GetHeight(this RectTransform rectTransform)
        {
            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            return corners[1].y - corners[0].y;
        }
        
        public static Vector3 AnimateToY(this RectTransform rectTransform, float targetBottomY, float duration = 0.5f, Action onComplete = null)
        {
            Vector3 targetPosition = rectTransform.localPosition;
            targetPosition.y = targetBottomY;
            
            rectTransform.DOLocalMove(targetPosition, duration)
                .SetEase(Ease.OutQuad).OnKill(() =>
                {
                    rectTransform.localPosition = targetPosition;
                }).OnComplete(() => onComplete?.Invoke());

            return targetPosition;
        }

        public static Vector3 AnimateTopWithBounce(this RectTransform rectTransform, IBlockBehaviour highestObject,
            float duration = 2f, float jumpPower = 1f, int numJumps = 1)
        {
            float width = rectTransform.rect.width;

            float randomOffsetX = Random.Range(-width * 0.5f, width * 0.5f);
            
            Vector3 targetPosition = new Vector3(
                highestObject.PrePosition.x + randomOffsetX,
                highestObject.PrePosition.y + highestObject.GetRectTransform().rect.height,
                rectTransform.localPosition.z
            );

            rectTransform.DOLocalJump(targetPosition, jumpPower: jumpPower, numJumps: numJumps, duration: duration)
                .SetEase(Ease.OutQuad);

            return targetPosition;
        }
        
        public static Rect GetWorldRect(this RectTransform rectTransform)
        {
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);
            
            Vector3 min = worldCorners[0];
            Vector3 max = worldCorners[2];
            
            return new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
        }

    }
}