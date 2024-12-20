using UnityEngine;

namespace Code.Common.Extensions
{
    public static class CanvasGroupExtension
    {
        public static void Enable(this CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public static void Disable(this CanvasGroup canvasGroup)
        {
            if (canvasGroup == null)
                return;
            
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}