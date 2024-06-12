using System.Collections.Generic;
using System;
using UnityEngine;
using Flameborn.UI.Alert;

namespace Flameborn.UI
{
    [Serializable]
    public class UIAlertController
    {

        [field: SerializeField]
        private UIAlertMenuController uiAlertMenuController { get; set; }

        [field: SerializeField]
        private Queue<AlertData> alertList { get; set; } = new Queue<AlertData>();

        public void Show(string title, string message, bool isCritical = false)
        {
            var alertData = new AlertData
            {
                Title = title,
                Message = message
            };

            if (alertList.Count > 0 || uiAlertMenuController.IsBusy)
            {
                alertList.Enqueue(alertData);

                return;
            }

            uiAlertMenuController.Show(alertData);
        }

        public void GetAlert()
        {
            if (alertList.Count == 0) return;

            var alert = alertList.Dequeue();
            uiAlertMenuController.Show(alert);
        }
    }
}