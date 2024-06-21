namespace flameborn.Core.UI.Abstract
{
    public interface IDataPanel<T> : IPanel where T : class, new()
    {
        void EventListener_OnDataHasChanged(T value);
        void Init(T values);
    }
}