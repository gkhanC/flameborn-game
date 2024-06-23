namespace flameborn.Core.Game.Inputs.Abstract
{
    public interface IInputListener<T> where T : new()
    {
        void InputListener(T result);
    }
}