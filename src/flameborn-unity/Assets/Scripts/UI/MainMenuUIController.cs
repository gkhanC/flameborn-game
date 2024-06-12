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
        private GameObject MainMenuScreen { get; set; }

        public void Init()
        {
            SetUIData();
            MainMenuScreen.SetActive(true);
        }

        private void SetUIData()
        {
            UserNameText.text = UserManager.Instance.currentUserData.UserName;
        }

        public void CloseAll()
        {
            MainMenuScreen.SetActive(false);
        }
    }
}