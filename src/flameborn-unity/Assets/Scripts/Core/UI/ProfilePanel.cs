using System;
using UnityEngine;
using flameborn.Core.UI.Abstract;
using flameborn.Core.UI.Animations;
using flameborn.Core.User;
using Sirenix.OdinInspector;
using flameborn.Core.UI.Controller;
using HF.Logger;
using TMPro;

namespace flameborn.Core.UI
{
    [Serializable]
    public class ProfilePanel : DataPanelBase<UserData>, IPanel
    {
        [FoldoutGroup("Panel Settings")]
        [field: SerializeField] protected ProfileMenuController profileController;

        [FoldoutGroup("UI Elements")][field: SerializeField] private TextMeshProUGUI userNameField;
        [FoldoutGroup("UI Elements")][field: SerializeField] private TextMeshProUGUI ratingField;
        [FoldoutGroup("UI Elements")][field: SerializeField] private TextMeshProUGUI rankField;

        [FoldoutGroup("UI Animation Settings")]
        [field: SerializeField] protected UIExpandAnimation uiAnimation;

        public ProfileMenuController Controller => profileController;

        public ProfilePanel()
        {

        }

        public override void EventListener_OnDataHasChanged(UserData value)
        {
            throw new NotImplementedException();
        }

        public override void Init(UserData value)
        {
            uiAnimation.Init();
            profileController.SetPanel(this);

            userNameField.text = value.UserName;
            ratingField.text = value.Rating.ToString();
            rankField.text = value.Rank.ToString();
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
    }
}