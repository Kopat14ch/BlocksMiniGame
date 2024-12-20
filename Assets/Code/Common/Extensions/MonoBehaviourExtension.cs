using UnityEngine;

namespace Code.Common.Extensions
{
    public static class MonoBehaviourExtension
    {
        public static void SetDefaultTransform(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.transform.localScale = Vector3.one;
            monoBehaviour.transform.localRotation = Quaternion.identity;
            monoBehaviour.transform.localPosition = Vector3.zero;
        }
    }
}