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
        private GameObject mainMenu { get; set; }

        public TextMeshProUGUI userName;

        public void Init()
        {
            SetUIData();
            mainMenu.SetActive(true);
        }

        private void SetUIData()
        {
            userName.text = UserManager.Instance.currentUserData.UserName;
        }

        public void CloseAll()
        {
            mainMenu.SetActive(false);
        }
    }
}