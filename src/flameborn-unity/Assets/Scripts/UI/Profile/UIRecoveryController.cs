namespace Flameborn.UI
{
    using Flameborn.Azure;
    using Flameborn.Device;
    using Flameborn.Managers;
    using HF.Extensions;
    using HF.Logger;
    using TMPro;
    using UnityEngine;

    public class UIRecoveryController : MonoBehaviour
    {

        private bool isEmailValid;
        private string dummyTextEmail;
        private string emailErrorText = $"<color=#ff0000>Please enter a valid Email Address.</color>";

        [field: SerializeField]
        private TMP_InputField emailInputField { get; set; }


        private bool isPasswordValid;
        private string dummyTextPassword;
        private string passwordErrorText = $"<color=#ff0000>Your password must be longer than 6 characters.</color>";

        [field: SerializeField]
        private TMP_InputField passwordInputField { get; set; }

        [field: SerializeField]
        private TextMeshProUGUI passwordInfo { get; set; }

        [field: SerializeField]
        private GameObject popupMenu { get; set; }

        public void OnEmailFieldSelect(string email)
        {
            if (emailInputField.text.Equals(dummyTextEmail) || emailInputField.text.Equals(emailErrorText))
            {
                emailInputField.text = "";
                isEmailValid = false;
            }
        }

        public void OnEmailFieldEndEdit(string email)
        {
            var data = new DeviceDataFactory().SetEmail(email).Create();

            if (data.errorLogs.Count > 0 || email.Equals(dummyTextEmail) || email.Equals(emailErrorText))
            {
                emailInputField.text = emailErrorText;
                isEmailValid = false;
                return;
            }

            isEmailValid = true;
        }

        public void OnEmailFieldDeselect(string email)
        {
            if (!isEmailValid)
            {
                emailInputField.text = emailErrorText;
            }
        }

        public void OnPasswordFieldSelect(string password)
        {
            if (passwordInputField.text.Equals(dummyTextPassword) || passwordInputField.text.Equals(passwordErrorText))
            {
                passwordInputField.text = "";
                passwordInfo.text = "";
                isPasswordValid = false;
            }
        }

        public void OnPasswordFieldEndEdit(string password)
        {
            var data = new DeviceDataFactory().SetPassword(password).Create();

            if (data.errorLogs.Count > 0 || password.Equals(dummyTextPassword) || password.Equals(passwordErrorText))
            {
                passwordInputField.text = "";
                passwordInfo.text = passwordErrorText;
                isPasswordValid = false;
                return;
            }

            isPasswordValid = true;
        }

        public void OnPasswordFieldDeselect(string password)
        {
            if (!isPasswordValid)
            {
                passwordInputField.text = "";
                passwordInfo.text = passwordErrorText;
            }
        }

        public void Btn_Recovery()
        {
            if (!isEmailValid) return;

            if (AzureManager.Instance.IsNull() || AzureManager.Instance.gameObject.IsNull())
            {
                UIManager.Instance.AlertController.Show("ERROR", "Something went wrong.");
                HFLogger.LogError(AzureManager.Instance, "Azure Manager instance is null.");
                gameObject.SetActive(false);
                return;
            }

            if (AzureManager.Instance.isAnyTaskRunning)
            {
                UIManager.Instance.AlertController.Show("Warning", "Please wait API is busy.");
                return;
            }

            UserManager.Instance.SetEmail(emailInputField.text);
            UserManager.Instance.SetPassword(passwordInputField.text);


            AzureManager.Instance.RecoveryUserPassword(out var _, emailInputField.text, this.OnRecoveryCompleted);
            UIManager.Instance.LoadingUIController.SetActiveLoadingScreen(true);
        }

        internal void OnRecoveryCompleted(bool isRecoveryValidated)
        {
            UIManager.Instance.LoadingUIController.SetActiveLoadingScreen(false);

            if (isRecoveryValidated)
            {
                UIManager.Instance.ProfileController.CloseAll();
            }

            gameObject.SetActive(!isRecoveryValidated);


        }


        private void OnEnable()
        {
            dummyTextEmail = emailInputField.text;
            emailInputField.onSelect.AddListener(OnEmailFieldSelect);
            emailInputField.onEndEdit.AddListener(OnEmailFieldEndEdit);
            emailInputField.onDeselect.AddListener(OnEmailFieldDeselect);

            dummyTextPassword = passwordInputField.text;
            passwordInputField.onSelect.AddListener(OnPasswordFieldSelect);
            passwordInputField.onEndEdit.AddListener(OnPasswordFieldEndEdit);
            passwordInputField.onDeselect.AddListener(OnPasswordFieldDeselect);

        }

        private void OnDisable()
        {
            emailInputField.text = dummyTextEmail;
            passwordInputField.text = dummyTextPassword;
        }
    }
}