using flameborn.Core.UI.Abstract;

namespace flameborn.Core.UI.Controller.Abstract
{
    public interface IUiController<T> : IPanel where T :  IPanel
    {
        T Panel { get; }
        void SetPanel(T panel);
    }
}