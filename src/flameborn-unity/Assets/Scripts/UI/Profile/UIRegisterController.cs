using Flameborn.Device;
using TMPro;
using UnityEngine;

namespace Flameborn.UI.Profile
{
    public class UIRegisterController : MonoBehaviour
    {
        private bool isEmailValid;
        private string dummyTextEmail;
        private string emailErrorText = $"<color=#ff0000>Please enter a valid Email Address.</color>";

        [field: SerializeField]
        public TMP_InputField emailInputField { get; private set; }


        private bool isPasswordValid;
        private string dummyTextPassword;
        private string passwordErrorText = $"<color=#ff0000>Your password must be longer than 6 characters.</color>";

        [field: SerializeField]
        public TMP_InputField passwordInputField { get; private set; }

        [field: SerializeField]
        public TextMeshProUGUI passwordInfo { get; private set; }

        private bool isNickNameValid;
        private string dummyTextNickName;
        private string nickNameErrorText = $"<color=#ff0000>Your nickname must be longer than 4 characters without special letters..</color>";

        [field: SerializeField]
        public TMP_InputField nickNameInputField { get; private set; }

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

            if (data.errorLogs.Count > 0 || !email.Equals(dummyTextEmail))
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

            if (data.errorLogs.Count > 0 || !password.Equals(dummyTextPassword) || string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
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

        public void OnNickNameFieldSelect(string nickName)
        {
            if (nickNameInputField.text.Equals(dummyTextNickName) || nickNameInputField.text.Equals(nickNameErrorText))
            {
                nickNameInputField.text = "";
                isNickNameValid = false;
            }
        }

        public void OnNickNameFieldEndEdit(string nickName)
        {
            var data = new DeviceDataFactory().SetUserName(nickName).Create();

            if (data.errorLogs.Count > 0 || !nickName.Equals(dummyTextNickName) || string.IsNullOrEmpty(nickName) || string.IsNullOrWhiteSpace(nickName))
            {

                nickNameInputField.text = nickNameErrorText;
                isNickNameValid = false;
                return;
            }

            isNickNameValid = true;

        }

        public void OnNickNameFieldDeselect(string nickName)
        {
            if (!isNickNameValid)
            {
                nickNameInputField.text = nickNameErrorText;
            }
        }

        public void Btn_SingUp()
        {

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

            dummyTextNickName = nickNameInputField.text;
            nickNameInputField.onSelect.AddListener(OnNickNameFieldSelect);
            nickNameInputField.onEndEdit.AddListener(OnNickNameFieldEndEdit);
            nickNameInputField.onDeselect.AddListener(OnNickNameFieldDeselect);
        }

        private void OnDisable()
        {

        }
    }
}