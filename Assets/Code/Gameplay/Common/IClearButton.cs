using System;

namespace Code.Gameplay.Common
{
    public interface IClearButton
    {
        event Action Clicked;
    }
}