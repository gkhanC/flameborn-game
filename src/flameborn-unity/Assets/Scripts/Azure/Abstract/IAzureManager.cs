using System;
using UnityEngine.Events;

namespace Flameborn.Azure
{
    /// <summary>
    /// Interface for Azure Manager.
    /// </summary>
    public interface IAzureManager
    {
        /// <summary>
        /// Adds a device data request with specified parameters.
        /// </summary>
        /// <param name="errorLog">Output parameter for error logging.</param>
        /// <param name="email">Email address of the user.</param>
        /// <param name="userName">Username of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="launchCount">Launch count of the device.</param>
        /// <param name="rating">Rating of the device.</param>
        void AddDeviceDataRequest(out string errorLog, string email, string userName, string password, int launchCount, int rating, params Action<AddDeviceDataResponse>[] responseListener);

        /// <summary>
        /// Recovery user password with the specified email and password.
        /// </summary>
        /// <param name="errorLog">Output parameter for error logging.</param>
        /// <param name="email">Email address of the user.</param>
        /// <param name="password">New password for the user.</param>
        void RecoveryUserPassword(out string errorLog, string email, params Action<RecoveryUserPasswordResponse>[] responseListener);

        /// <summary>
        /// Updates launch count with the specified email, password, and new launch count.
        /// </summary>
        /// <param name="errorLog">Output parameter for error logging.</param>
        /// <param name="email">Email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="newLaunchCount">New launch count to be updated.</param>
        void UpdateLaunchCountRequest(out string errorLog, string email, string password, int newLaunchCount, params Action<UpdateLaunchCountResponse>[] responseListener);

        /// <summary>
        /// Updates rating with the specified email, password, and new rating.
        /// </summary>
        /// <param name="errorLog">Output parameter for error logging.</param>
        /// <param name="email">Email address of the user.</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="newRating">New rating to be updated.</param>
        void UpdateRatingRequest(out string errorLog, string email, string password, int newRating, params Action<UpdateRatingResponse>[] responseListener);

        /// <summary>
        /// Subscribes to the event that is invoked when user data load is completed.
        /// </summary>
        /// <param name="onUserDataLoadCompleted">The action to invoke when user data load is completed.</param>
        void SubscribeOnUserDataLoadCompleted(UnityAction onUserDataLoadCompleted);

        /// <summary>
        /// Subscribes to the event that is invoked when user data add is completed.
        /// </summary>
        /// <param name="onUserDataAddCompleted">The action to invoke when user data add is completed.</param>
        void SubscribeOnUserDataAddCompleted(UnityAction onUserDataAddCompleted);
    }
}
