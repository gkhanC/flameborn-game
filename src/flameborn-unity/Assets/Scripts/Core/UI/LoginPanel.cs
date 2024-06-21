using System;
using System.Text.RegularExpressions;
using flameborn.Core.Managers;
using flameborn.Core.UI.Abstract;
using flameborn.Core.UI.Animations;
using flameborn.Core.UI.Controller;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace flameborn.Core.UI
{
    [Serializable]
    public class LoginPanel : PanelBase, ILoginPanel
    {

        [FoldoutGroup("Panel Settings")]
        [field: SerializeField] protected LoginMenuController loginPanelController;

        [FoldoutGroup("UI Elements")]
        [field: SerializeField] protected TMP_InputField emailField;

        [FoldoutGroup("UI Elements")]
        [field: SerializeField] protected TMP_InputField passwordField;

        [FoldoutGroup("UI Elements")]
        [field: SerializeField] private TextMeshProUGUI passwordInfo;

        [FoldoutGroup("UI Animation Settings")]
        [field: SerializeField] protected UIExpandAnimation uiAnimation;

        public LoginMenuController Controller => loginPanelController;

        public IPanel RegisterPanel { get; set; } = default;

        public IPanel RecoveryPanel { get; set; } = default;

        public bool IsEmailValid { get; set; } = false;
        public bool IsPasswordValid { get; set; } = false;

        private readonly Regex EmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase);

        public LoginPanel()
        {

        }

        public void Init(IPanel registerPanel, IPanel recoveryPanel)
        {
            uiAnimation.Init();
            loginPanelController.SetPanel(this);
            RegisterPanel = registerPanel;
            RecoveryPanel = recoveryPanel;

            emailField.onSelect.AddListener(OnEmailSelect);
            emailField.onDeselect.AddListener(OnEmailValueChanged);

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