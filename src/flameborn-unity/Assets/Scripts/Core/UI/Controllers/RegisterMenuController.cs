using flameborn.Core.Managers;
using flameborn.Core.UI.Abstract;
using flameborn.Core.UI.Controller.Abstract;
using HF.Logger;
using UnityEngine;

namespace flameborn.Core.UI.Controller
{
    public class RegisterMenuController : UIControllerBase<IRegisterPanel>, IPanel
    {
        public RegisterMenuController()
        {

        }

        public void Btn_Login()
        {
            Hide();
            Panel.LoginPanel.Show();
            HFLogger.Log(this, $"{nameof(this.Btn_Login)} invoked.");
        }

        public void Btn_SingUp()
        {
            HFLogger.Log(this, $"{nameof(this.Btn_SingUp)} invoked.");

            if (!Panel.IsEmailValid || !Panel.IsPasswordValid || !Panel.IsUserNameValid) return;
            var accountManager = GameManager.Instance.GetManager<AccountManager>();
            if (accountManager.IsContain)
            {
                accountManager.Instance.Register();
                Hide();
            }
        }
    }
}