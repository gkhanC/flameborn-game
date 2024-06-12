using System.Collections.Generic;
using System.Text.RegularExpressions;
using Flameborn.Device.Abstract;

namespace Flameborn.Device
{
    public class DeviceDataFactory : IDeviceDataFactory
    {
        private readonly DeviceData _deviceData;
        private readonly List<string> _errorLogs;
        private readonly Regex EmailRegex;
        private readonly Regex PasswordRegex;
        private readonly Regex UserNameRegex;

        public DeviceDataFactory()
        {
            _deviceData = new DeviceData();
            _errorLogs = new List<string>();

            EmailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

            PasswordRegex = new Regex(@"^(?=.*[A-Z]).{6,}$",
            RegexOptions.Compiled);

            UserNameRegex = new Regex(@"^[a-zA-Z0-9]{4,}$",
            RegexOptions.Compiled);
        }

        public IDeviceDataFactory SetEmail(string email)
        {
            if (IsValidEmail(email))
            {
                _deviceData.SetEmail(email);
            }
            else
            {
                _errorLogs.Add($"Invalid email: {email}");
            }
            return this;
        }

        public IDeviceDataFactory SetPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                _deviceData.SetPassword("");
            }
            else if (IsValidPassword(password))
            {
                string hashedPassword = PasswordHasher.HashPassword(password);
                _deviceData.SetPassword(hashedPassword);
            }
            else
            {
                _errorLogs.Add("Password does not meet criteria");
            }
            return this;
        }

        public IDeviceDataFactory SetUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                _deviceData.SetUserName(userName);
            }
            else if (IsValidUserName(userName))
            {
                _deviceData.SetUserName(userName);
            }
            else
            {
                _errorLogs.Add("Username cannot be empty or have a special character");
            }
            return this;
        }

        public IDeviceDataFactory SetLaunchCount(int launchCount)
        {
            if (launchCount >= 0)
            {
                _deviceData.SetLaunchCount(launchCount);
            }
            else
            {
                _errorLogs.Add("Launch count cannot be negative");
            }
            return this;
        }

        public IDeviceDataFactory SetRating(int rating)
        {
            if (rating >= 0)
            {
                _deviceData.SetRating(rating);
            }
            else
            {
                _errorLogs.Add("Rating must be positive integer");
            }
            return this;
        }

        public (DeviceData deviceData, List<string> errorLogs) Create()
        {
            return (_deviceData, _errorLogs);
        }

        private bool IsValidEmail(string email)
        {
            // Simplified email validation logic
            if (email.Length < 5) return false;
            return EmailRegex.IsMatch(email);
        }

        private bool IsValidPassword(string password)
        {
            // Simplified password validation logic
            if (password.Length < 6) return false;
            return true;
        }

        private bool IsValidUserName(string userName)
        {
            if (userName.Length < 4) return false;
            return UserNameRegex.IsMatch(userName);
        }
    }
}
