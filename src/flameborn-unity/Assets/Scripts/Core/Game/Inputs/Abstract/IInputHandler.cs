using System;
using UnityEngine;

namespace flameborn.Core.Game.Inputs.Abstract
{
    public interface IInputHandler<T> where T : new()
    {
        void HandleInput(params Action<T>[] inputListeners);
    }
}