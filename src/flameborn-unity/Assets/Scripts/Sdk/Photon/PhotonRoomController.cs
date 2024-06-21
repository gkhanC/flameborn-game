using System;
using Photon.Pun;
using Photon.Realtime;

namespace flameborn.Sdk.Photon
{
    [Serializable]
    public class PhotonRoomController
    {
        public void CreateOrJoinRoom(string roomName, byte maxPlayers, string userName)
        {
            PhotonNetwork.NickName = userName;
            RoomOptions roomOptions = new RoomOptions { MaxPlayers = maxPlayers };
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
        }

        public bool IsMasterClient()
        {
            return PhotonNetwork.IsMasterClient;
        }
    }
}