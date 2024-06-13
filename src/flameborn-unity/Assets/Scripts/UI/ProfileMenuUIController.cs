using System;
using Flameborn.Managers;
using Flameborn.PlayFab;
using HF.Extensions;
using HF.Logger;
using UnityEngine;

namespace Flameborn.UI.Profile
{
    [Serializable]
    public class ProfileMenuUIController : IUIController
    {
        [field: SerializeField] private GameObject profileMenu { get; set; }
        [field: SerializeField] private GameObject loginMenu { get; set; }
        [field: SerializeField] private GameObject registerMenu { get; set; }
        [field: SerializeField] private GameObject accountRecoveryMenu { get; set; }

        [field : SerializeField]
        public UILoginController UILoginController{ get; private set; }

        public void ActiveMenu()
        {
            if (UserManager.Instance.IsNull() && UserManager.Instance.gameObject.IsNull())
            {
                UIManager.Instance.AlertController.Show("ERROR", "Something went wrong!", true);
                HFLogger.LogError(UserManager.Instance, "User Manager instance is null.");
                return;
            }

            if (!UserManager.Instance.currentUserData.IsDeviceRegistered)
            {
                ActiveRegisterMenu();
                return;
            }

            if (UserManager.Instance.currentUserData.IsContainEmail)
            {
                if (!UserManager.Instance.currentUserData.IsRegistered)
                {
                    ActiveRegisterMenu();
                    return;
                }

                if (!UserManager.Instance.currentUserData.IsPasswordCorrect)
                {
                    ActiveLoginMenu();
                    return;
                }
            }
            else
            {
                ActiveLoginMenu();
                return;
            }

            if (PlayFabManager.Instance.IsNull() && PlayFabManager.Instance.gameObject.IsNull())
            {
                UIManager.Instance.AlertController.Show("ERROR", "Not found PlayFab Manager", true);
                HFLogger.LogError(UserManager.Instance, "PlayFab Manager instance is null.");
                return;
            }

            if (PlayFabManager.Instance.IsLogin)
            {
                ActiveProfileMenu();
                return;
            }

            ActiveLoginMenu();
            return;

        }


        internal void ActiveProfileMenu()
        {
            registerMenu.SetActive(false);
            loginMenu.SetActive(false);
            accountRecoveryMenu.SetActive(false);
            profileMenu.SetActive(true);
        }

        internal void ActiveLoginMenu()
        {
            registerMenu.SetActive(false);
            profileMenu.SetActive(false);
            accountRecoveryMenu.SetActive(false);
            loginMenu.SetActive(true);
        }


        internal void ActiveRegisterMenu()
        {
            loginMenu.SetActive(false);
            accountRecoveryMenu.SetActive(false);
            profileMenu.SetActive(false);
            registerMenu.SetActive(true);
        }

        internal void ActiveAccountRecoveryMenu()
        {
            loginMenu.SetActive(false);
            profileMenu.SetActive(false);
            registerMenu.SetActive(false);
            accountRecoveryMenu.SetActive(true);
        }


        public void CloseAll()
        {
            profileMenu.SetActive(false);
            loginMenu.SetActive(false);
            registerMenu.SetActive(false);
            accountRecoveryMenu.SetActive(false);
        }
    }
}