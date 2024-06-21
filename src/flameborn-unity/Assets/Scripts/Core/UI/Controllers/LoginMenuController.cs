using flameborn.Core.Managers;
using flameborn.Core.UI.Abstract;
using flameborn.Core.UI.Controller.Abstract;
using HF.Logger;
using UnityEngine;

namespace flameborn.Core.UI.Controller
{
    public class LoginMenuController : UIControllerBase<ILoginPanel>, IPanel
    {
        public LoginMenuController()
        {

        }

        public void Btn_SingUp()
        {
            Hide();
            Panel.RegisterPanel.Show();          
        }

        public void Btn_Login()
        {            
            if (!Panel.IsEmailValid || !Panel.IsPasswordValid) return;

            var accountManager = GameManager.Instance.GetManager<AccountManager>();
            if (accountManager.IsContain)
            {
                accountManager.Instance.Login();
                Hide();
            }
        }

        public void Btn_Recovery()
        {
            Hide();
            Panel.RecoveryPanel.Show();
            HFLogger.Log(this, $"{nameof(this.Btn_Recovery)} invoked.");
        }
    }
}