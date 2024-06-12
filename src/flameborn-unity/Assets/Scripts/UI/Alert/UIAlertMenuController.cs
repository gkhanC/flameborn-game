using Flameborn.Managers;
using HF.Extensions;
using HF.Logger;
using TMPro;
using UnityEngine;

namespace Flameborn.UI.Alert
{
    public class UIAlertMenuController : MonoBehaviour
    {
        private bool _isCritical;
        public bool IsBusy { get; set; } = false;
        public TextMeshProUGUI title;
        public TextMeshProUGUI body;

        public GameObject alertMenu;

        public void Show(AlertData alertData)
        {
            IsBusy = true;
            title.text = alertData.Title;
            body.text = alertData.Message;
            _isCritical = alertData.IsCritical;

            alertMenu.SetActive(true);
            if (_isCritical)
            {
                HFLogger.LogError(this, "Application Quit with some errors.");
            }
        }

        public void AlertClose()
        {
            if (_isCritical)
            {
                Application.Quit();
            }

            alertMenu.SetActive(false);

            IsBusy = false;

            if (UIManager.Instance.IsNotNull() && UIManager.Instance.gameObject.IsNotNull())
            {
                UIManager.Instance.AlertController.GetAlert();
            }
        }
    }

    public class AlertData
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsCritical { get; set; }
    }
}