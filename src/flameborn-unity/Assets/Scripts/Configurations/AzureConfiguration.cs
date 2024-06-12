using System;
using UnityEngine;
namespace Flameborn.Configurations
{
    public class AzureConfiguration : Configuration, IConfiguration
    {
        [field: SerializeField]
        public string AddDeviceDataFunctionConnection { get; set; } = string.Empty;

        [field: SerializeField]
        public string ValidateUserPassword { get; set; } = string.Empty;

        [field: SerializeField]
        public string ValidateUserEmailFunctionConnection { get; set; } = string.Empty;

        [field: SerializeField]
        public string ValidateDeviceIdFunctionConnection { get; set; } = string.Empty;

        [field: SerializeField]
        public string GetLaunchCountFunctionConnection { get; set; } = string.Empty;

        [field: SerializeField]
        public string GetRatingFunctionConnection { get; set; } = string.Empty;

        [field: SerializeField]
        public string UpdateDeviceDataFunctionConnection { get; set; } = string.Empty;

        [field: SerializeField]
        public string UpdateLaunchCountFunctionConnection { get; set; } = string.Empty;

        [field: SerializeField]
        public string UpdateRatingFunctionConnection { get; set; } = string.Empty;
        public string UpdateUserPasswordFunctionConnection { get; set; } = string.Empty;



        public AzureConfiguration(string path) : base(path)
        {

        }
    }
}