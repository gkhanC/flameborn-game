using ExitGames.Client.Photon.StructWrapping;
using flameborn.Core.Managers;
using flameborn.Core.UI.Abstract;
using flameborn.Core.UI.Controller.Abstract;

namespace flameborn.Core.UI
{
    public class MainMenuController : UIControllerBase<IMainMenuPanel>, IPanel
    {
        public MainMenuController()
        {

        }

        public void Btn_ProfileMenu()
        {
            if (Panel.UserData.IsLogin)
            {
                Panel.ProfileMenu.Controller.Show();
            }
            else
            {
                Panel.LoginMenu.Controller.Show();
            }
        }

        public void Btn_Battle()
        {
            var matchMakingManager = GameManager.Instance.GetManager<MatchMakingManager>();
            if (matchMakingManager.IsContain)
            {
                matchMakingManager.Instance.NewMatch();
            }
        }
    }
}