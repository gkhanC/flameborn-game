using System;
using System.Text.RegularExpressions;
using flameborn.Core.Managers;
using flameborn.Core.UI.Abstract;
using flameborn.Core.UI.Animations;
using flameborn.Core.UI.Controller;
using HF.Logger;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace flameborn.Core.UI
{
    [Serializable]
    public class RegisterPanel : PanelBase, IRegisterPanel
    {
        [FoldoutGroup("Panel Settings")]
        [field: SerializeField] protected RegisterMenuController registerPanelController;

        [FoldoutGroup("UI Animation Settings")]
        [field: SerializeField] protected UIExpandAnimation uiAnimation;

        public RegisterMenuController Controller => registerPanelController;

        [FoldoutGroup("UI Elements")]
        [field: SerializeField] protected TMP_InputField emailField;

        [FoldoutGroup("UI Elements")]
        [field: SerializeField] protected TMP_InputField userNameField;

        [FoldoutGroup("UI Elements")]
        [field: SerializeField] protected TMP_InputField passwordField;

        [FoldoutGroup("UI Elements")]
        [field: SerializeField] private TextMeshProUGUI passwordInfo;

        public IPanel LoginPanel { get; set; } = default;

        public bool IsEmailValid { get; set; } = false;
        public bool IsUserNameValid { get; set; } = false;
        public bool IsPasswordValid { get; set; } = false;

        private readonly Regex EmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase);

        public RegisterPanel()
        {

        }

        public void Init(IPanel loginPanel)
        {
            uiAnimation.Init();
            registerPanelController.SetPanel(this);
            LoginPanel = loginPanel;

            emailField.onSelect.AddListener(OnEmailSelect);
            emailField.onDeselect.AddListener(OnEmailValueChanged);

            userNameField.onSelect.AddListener(OnUserNameSelect);
            userNameField.onDeselect.AddListener(OnUserNameValueChanged);

            passwordField.onSelect.AddListener(OnPasswordSelect);
            passwordField.onDeselect.AddListener(OnPasswordValueChanged);
        }

        public override void Show()
        {
            base.Show();
            uiAnimation.Play();
        }

        public override void Hide()
        {
            uiAnimation.Stop(base.Hide);
        }

        public void OnEmailSelect(string email)
        {
            if (!IsEmailValid) emailField.text = "";
        }

        public void OnEmailValueChanged(string email)
        {
            if (email.Length < 5 || !EmailRegex.IsMatch(email))
            {
                emailField.text = $"<color=#ff0000>Please enter a valid Email Address.</color>";
                IsEmailValid = false;
                return;
            }


            var accountManager = GameManager.Instance.GetManager<AccountManager>();
            accountManager.Instance.Account.Email = email;
            IsEmailValid = true;

        }

        private void OnUserNameSelect(string userName)
        {
            if (!IsUserNameValid) { userNameField.text = ""; }
        }

        public void OnUserNameValueChanged(string userName)
        {
            if (userName.Length < 4)
            {
                userNameField.text = $"<color=#ff0000>Your nickname must be longer than 4 characters without special letters..</color>";
                userNameField.text = "";
                IsUserNameValid = false;
                return;
            }


            var accountManager = GameManager.Instance.GetManager<AccountManager>();
            accountManager.Instance.Account.UserData.UserName = userName;
            IsUserNameValid = true;

        }

        private void OnPasswordSelect(string password)
        {
            if (!IsPasswordValid) { passwordInfo.text = ""; passwordField.text = ""; }
        }

        public void OnPasswordValueChanged(string password)
        {
            if (password.Length < 6)
            {
                passwordInfo.text = $"<color=#ff0000>Your password must be longer than 6 characters.</color>";
                passwordField.text = "";
                IsPasswordValid = false;
                return;
            }


            var accountManager = GameManager.Instance.GetManager<AccountManager>();
            accountManager.Instance.Account.Password = password;
            IsPasswordValid = true;

        }
    }
}