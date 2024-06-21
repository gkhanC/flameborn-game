namespace flameborn.Core.UI.Abstract
{
    public interface IRegisterPanel : IPanel
    {
        bool IsEmailValid { get; }
        bool IsUserNameValid { get; }
        bool IsPasswordValid { get; }
        IPanel LoginPanel { get; }
    }
}