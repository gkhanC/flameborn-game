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
    public class RecoveryPanel : PanelBase, IRecoveryPanel
    {
        [FoldoutGroup("Panel Settings")]
        [field: SerializeField] protected RecoveryMenuController recoveryPanelController;

        [FoldoutGroup("UI Elements")]
        [field: SerializeField] protected TMP_InputField emailField;

        [FoldoutGroup("UI Animation Settings")]
        [field: SerializeField] protected UIExpandAnimation uiAnimation;

        public RecoveryMenuController Controller => recoveryPanelController;

        public IPanel RegisterPanel { get; set; } = default;

        public IPanel LoginPanel { get; set; } = default;

        public bool IsEmailValid { get; set; } = false;
        private readonly Regex EmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.IgnoreCase);


        public RecoveryPanel()
        {

        }

        public void Init(IPanel registerPanel, IPanel loginPanel)
        {
            uiAnimation.Init();
            recoveryPanelController.SetPanel(this);

            emailField.onSelect.AddListener(OnEmailSelect);
            emailField.onDeselect.AddListener(OnEmailValueChanged);

            RegisterPanel = registerPanel;
            LoginPanel = loginPanel;
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
    }
}