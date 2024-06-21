using System;
using System.Xml;
using flameborn.Core.UI.Abstract;
using flameborn.Core.UI.Animations;
using flameborn.Core.UI.Controller;
using HF.Extensions;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace flameborn.Core.UI
{
    [Serializable]
    public class AlertPanel : PanelBase, IPanel
    {

        [FoldoutGroup("Panel Settings")]
        [field: SerializeField] protected AlertMenuController alertPanelControllerController;

        [FoldoutGroup("UI Elements")]
        public TextMeshProUGUI titleField;

        [FoldoutGroup("UI Elements")]
        public TextMeshProUGUI bodyField;


        [FoldoutGroup("UI Animation Settings")]
        [field: SerializeField] protected UIExpandAnimation uiAnimation;

        private UnityEvent onClose;

        public AlertPanel()
        {

        }

        public void Init()
        {
            uiAnimation.Init();
            alertPanelControllerController.SetPanel(this);
        }

        public void Show(string title, string body, params Action[] actions)
        {
            onClose = new UnityEvent();
            actions.ForEach(a =>
            {
                onClose.AddListener(new UnityAction(a));
            });

            titleField.text = title;
            bodyField.text = body;
            Show();
        }

        public void Show(string title, string body)
        {
            titleField.text = title;
            bodyField.text = body;
            Show();
        }

        public override void Show()
        {
            base.Show();
            uiAnimation.Play();
        }

        public override void Hide()
        {
            onClose?.Invoke();
            onClose = new UnityEvent();
            uiAnimation.Stop(base.Hide);
        }
    }
}