namespace flameborn.Core.UI.Abstract
{
    public interface ILoginPanel : IPanel
    {
        IPanel RegisterPanel { get; }
        IPanel RecoveryPanel { get; }

        bool IsEmailValid { get; }
        bool IsPasswordValid { get; }
    }
}