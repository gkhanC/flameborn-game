using System;
using UnityEngine;
using flameborn.Core.UI.Abstract;
using flameborn.Core.User;
using Sirenix.OdinInspector;
using TMPro;
using flameborn.Core.Accounts;
using HF.Logger;

namespace flameborn.Core.UI
{
    [Serializable]
    public class MainMenuPanel : DataPanelBase<Account>, IMainMenuPanel, IDataPanel<UserData>
    {
        private Account _account = new Account();

        [FoldoutGroup("Panel Settings", expanded: true)]
        [field: SerializeField] protected MainMenuController mainMenuController;

        [FoldoutGroup("Menu Elements")]
        [field: SerializeField] protected MainMenuElements elements = new MainMenuElements();

        [BoxGroup("Associated panels")]
        [field: SerializeField] protected ProfilePanel profileMenu = new ProfilePanel();

        [BoxGroup("Associated panels")]
        [field: SerializeField] protected LoginPanel loginMenu = new LoginPanel();

        [BoxGroup("Associated panels")]
        [field: SerializeField] protected RegisterPanel registerMenu = new RegisterPanel();

        [BoxGroup("Associated panels")]
        [field: SerializeField] protected RecoveryPanel recoveryMenu = new RecoveryPanel();

        public GameObject waitingPanel;

        public UserData UserData => _account.UserData;
        public MainMenuElements MenuElements => elements;
        public ProfilePanel ProfileMenu => profileMenu;
        public LoginPanel LoginMenu => loginMenu;
        public RegisterPanel RegisterMenu => registerMenu;
        public RecoveryPanel RecoveryMenu => recoveryMenu;

        public override void Init(Account value)
        {
            _account = value;
            Init(_account.UserData);
        }

        public void Init(UserData value)
        {
            mainMenuController.SetPanel(this);
            _account.SetUserData(value);
            InitUIElements();
            InitPanels();
        }

        public override void EventListener_OnDataHasChanged(Account value)
        {
            _account = value;
            InitUIElements();
            Init(_account.UserData);
        }

        public void EventListener_OnDataHasChanged(UserData value)
        {
            _account.UserData = value;
            InitUIElements();
            Init(_account.UserData);
        }

        private void InitPanels()
        {
            loginMenu.Init(registerMenu, recoveryMenu);
            profileMenu.Init(_account.UserData);
            recoveryMenu.Init(registerMenu, loginMenu);
            registerMenu.Init(loginMenu);
        }

        private void InitUIElements()
        {
            elements.launchCountField.text = _account.UserData.LaunchCount.ToString();
            elements.ratingField.text = _account.UserData.Rating.ToString();
            elements.userNameField.text = _account.UserData.UserName;
        }

        public override void Hide()
        {
            profileMenu.Hide();
            loginMenu.Hide();
            registerMenu.Hide();
            recoveryMenu.Hide();
            waitingPanel.SetActive(false);
            base.Hide();
        }

        public override void Lock(object lockerObject)
        {
            base.Lock(lockerObject);
            profileMenu.Lock(lockerObject);
            loginMenu.Lock(lockerObject);
            registerMenu.Lock(lockerObject);
            recoveryMenu.Lock(lockerObject);
        }

        public override void UnLock(object lockerObject)
        {
            base.UnLock(lockerObject);
            profileMenu.UnLock(lockerObject);
            loginMenu.UnLock(lockerObject);
            registerMenu.UnLock(lockerObject);
            recoveryMenu.UnLock(lockerObject);
        }

        [Serializable]
        public class MainMenuElements
        {
            public TextMeshProUGUI userNameField;
            public TextMeshProUGUI ratingField;
            public TextMeshProUGUI launchCountField;
        }
    }
}