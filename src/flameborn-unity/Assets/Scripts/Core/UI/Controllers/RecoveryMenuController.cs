using flameborn.Core.Managers;
using flameborn.Core.UI.Abstract;
using flameborn.Core.UI.Controller.Abstract;
using HF.Logger;

namespace flameborn.Core.UI.Controller
{
    public class RecoveryMenuController : UIControllerBase<IRecoveryPanel>, IPanel
    {
        public RecoveryMenuController()
        {

        }

        public void Btn_Login()
        {
            Hide();
            Panel.LoginPanel.Show();
            HFLogger.Log(this, $"{nameof(this.Btn_Login)} invoked.");
        }

        public void Btn_Register()
        {
            Hide();
            Panel.RegisterPanel.Show();
            HFLogger.Log(this, $"{nameof(this.Btn_Register)} invoked.");
        }

        public void Btn_Recovery()
        {
            if (Panel.IsEmailValid)
            {
                var accountManager = GameManager.Instance.GetManager<AccountManager>();
                if (accountManager.IsContain)
                {
                    accountManager.Instance.PasswordResetRequest();
                    Hide();
                }
            }
            HFLogger.Log(this, $"{nameof(this.Btn_Recovery)} invoked.");
        }
    }
}