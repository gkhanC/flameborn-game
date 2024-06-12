using System;
using Flameborn.Managers;
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
        }

        public void CloseAll()
        {
            MainMenuScreen.SetActive(false);
        }
    }
}