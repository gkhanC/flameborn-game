using System;
using Flameborn.Managers;
using Flameborn.UI.Profile;
using TMPro;
using UnityEngine;

namespace Flameborn.UI
{
    [Serializable]
    public class MainMenuUIController : IUIController
    {
        [field: SerializeField]
        private TextMeshProUGUI UserNameText { get; set; }

        [field: SerializeField]
        private TextMeshProUGUI UserRatingText { get; set; }

        [field: SerializeField]
        private TextMeshProUGUI UserLaunchCountText { get; set; }

        [field: SerializeField]
        private GameObject MainMenuScreen { get; set; }

        [field: SerializeField]
        private UIProfileController uiProfileController { get; set; }

        public void Init()
        {
            SetUIData();
            MainMenuScreen.SetActive(true);
        }

        public void SetUIData()
        {
            UserNameText.text = UserManager.Instance.currentUserData.UserName;
            UserRatingText.text = UserManager.Instance.currentUserData.Rating.ToString();
            UserLaunchCountText.text = UserManager.Instance.currentUserData.LaunchCount.ToString();

            uiProfileController.userName.text = UserManager.Instance.currentUserData.UserName;
            uiProfileController.rating.text = UserManager.Instance.currentUserData.Rating.ToString();
            uiProfileController.ranking.text = UserManager.Instance.currentUserData.Rank.ToString();

        }

        public void CloseAll()
        {
            MainMenuScreen.SetActive(false);
        }
    }
}