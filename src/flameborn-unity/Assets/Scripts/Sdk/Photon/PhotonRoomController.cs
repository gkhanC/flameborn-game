using System;
using Photon.Pun;
using Photon.Realtime;

namespace flameborn.Sdk.Photon
{
    /// <summary>
    /// Controller for managing Photon rooms.
    /// </summary>
    [Serializable]
    public class PhotonRoomController
    {
        #region Methods

        /// <summary>
        /// Creates or joins a Photon room with the specified parameters.
        /// </summary>
        /// <param name="roomName">The name of the room.</param>
        /// <param name="maxPlayers">The maximum number of players allowed in the room.</param>
        /// <param name="userName">The user name to set for the player.</param>
        public void CreateOrJoinRoom(string roomName, byte maxPlayers, string userName)
        {
            PhotonNetwork.NickName = userName;
            RoomOptions roomOptions = new RoomOptions { MaxPlayers = maxPlayers };
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        }

        /// <summary>
        /// Determines whether the local player is the master client.
        /// </summary>
        /// <returns>true if the local player is the master client; otherwise, false.</returns>
        public bool IsMasterClient()
        {
            return PhotonNetwork.IsMasterClient;
        }

        #endregion
    }
}
