namespace flameborn.Core.UI.Abstract
{
    public abstract class DataPanelBase<T> : PanelBase, IDataPanel<T> where T : class, new()
    {
        public abstract void EventListener_OnDataHasChanged(T values);

        public abstract void Init(T value);
    }
}