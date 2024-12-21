using System;

namespace Code.Gameplay.Common
{
    public interface IDestructible
    {
        public event Action<IDestructible> Destroyed; 
        
        public void Destroy(bool isAnimation = true, bool invokeEvent = true, bool onlyInvoke = false);
    }
}