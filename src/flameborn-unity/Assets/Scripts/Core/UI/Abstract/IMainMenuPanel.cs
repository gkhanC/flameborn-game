using flameborn.Core.User;

namespace flameborn.Core.UI.Abstract
{
    public interface IMainMenuPanel : IPanel
    {
        UserData UserData { get; }
        ProfilePanel ProfileMenu { get; }
        LoginPanel LoginMenu { get; }
        RegisterPanel RegisterMenu { get; }
        RecoveryPanel RecoveryMenu { get; }
    }
}