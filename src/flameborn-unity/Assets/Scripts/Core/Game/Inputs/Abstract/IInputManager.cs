using System;
using flameborn.Core.Game.Inputs.Controllers.Abstract;

namespace flameborn.Core.Game.Inputs.Abstract
{
    public interface IInputManager<T, V> where T : IInputController where V : new()
    {
        T Controller { get; }
        void SubscribeInputController(Action<V> listener);
    }
}