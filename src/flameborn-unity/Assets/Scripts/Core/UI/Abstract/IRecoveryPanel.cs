namespace flameborn.Core.UI.Abstract
{
    public interface IRecoveryPanel : IPanel
    {
        bool IsEmailValid { get; }
        IPanel LoginPanel { get; }
        IPanel RegisterPanel { get; }
    }
}