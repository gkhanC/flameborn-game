using flameborn.Core.User;

namespace flameborn.Core.Accounts
{
    public interface IAccount
    {
        UserData UserData { get; }
        void SetUserData(UserData data);
    }
}