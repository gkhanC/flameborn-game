using System.Collections.Generic;
using ExitGames.Client.Photon;
using flameborn.Core.Managers;
using flameborn.Core.Managers.Abstract;
using HF.Logger;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace flameborn.Sdk.Photon
{
    public class PhotonManager : MonoBehaviourPunCallbacks, IManager
    {
        private static PhotonManager instance;

        private bool isReady = false;
        private bool isConnecting = false;

        private string matchId = "";
        private string userName = "";

        public List<Player> playersInRoom = new List<Player>();
        private PhotonRoomController roomController;

        private UiManager uiManager;
        private bool isMatchStop;
        private bool isMatchStart;
        private bool isConnectSuccess;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            GameManager.Instance.SetManager(this);
            uiManager = GameManager.Instance.GetManager<UiManager>().Instance;
            roomController = new PhotonRoomController();
        }

        public void Init(string matchID, string userName)
        {
            this.matchId = matchID;
            this.userName = userName;

            Debug.Log(PhotonNetwork.NetworkClientState);
            Debug.Log("connect:" + PhotonNetwork.IsConnectedAndReady.ToString());
            if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
            {
                JoinOrCreateRoom();
                return;
            }

            ConnectToPhoton();

        }

        private void InitializePlayerList()
        {
            playersInRoom.Clear();
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                playersInRoom.Add(player);
            }

            if (playersInRoom.Count >= 2)
            {
                ShowLobby();
            }
        }

        public void ShowLobby()
        {
            if (playersInRoom.Count >= 2)
            {
                uiManager.mainMenu.waitingPanel.SetActive(false);
                uiManager.lobbyMenu.SetPlayerData(playersInRoom);
                uiManager.lobbyMenu.Show();
                isReady = true;

                Hashtable props = new Hashtable
                {
                    { "IsReady", isReady }
                };
                PhotonNetwork.LocalPlayer.SetCustomProperties(props);

                if (PhotonNetwork.IsMasterClient)
                {
                    Invoke(nameof(this.StartMatch), 3f);
                }
            }
        }

        public void ConnectCheck()
        {
            if (!isConnectSuccess) MatchCanceled("Network ERROR.");
        }

        private void PlayerLeft(Player otherPlayer)
        {
            if (playersInRoom.Contains(otherPlayer))
            {
                playersInRoom.Remove(otherPlayer);
                if (playersInRoom.Count < 2)
                {
                    MatchCanceled("Match canceled.");
                }
            }
        }

        private void JoinOrCreateRoom()
        {
            isMatchStop = false;
            isMatchStart = true;
            isConnectSuccess = true;
            roomController.CreateOrJoinRoom(matchId, 2, userName);
        }

        private void ConnectToPhoton()
        {
            if (!isConnecting)
            {
                isConnecting = true;
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = "1";
            }
        }

        public override void OnConnectedToMaster()
        {
            if (!isMatchStart)
                JoinOrCreateRoom();

            Invoke(nameof(this.ConnectCheck), 2f);
        }

        public void MatchCanceled(string message)
        {
            PhotonNetwork.LeaveRoom();
            uiManager.mainMenu.waitingPanel.SetActive(false);
            uiManager.lobbyMenu.Hide();

            if (isMatchStop) return;
            isMatchStop = true;
            uiManager.alert.Show("Alert", message, LoadMainMenu);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(1);
        }

        public override void OnLeftRoom()
        {
            PhotonNetwork.LeaveLobby();
        }

        public override void OnLeftLobby()
        {

        }
        public override void OnCreatedRoom()
        {

        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            MatchCanceled("Network ERROR.");
        }

        public override void OnJoinedRoom()
        {

            InitializePlayerList();

        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            MatchCanceled("Network ERROR.");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            if (!isMatchStop)
                uiManager.alert.Show("Alert", "You are disconnected.", Application.Quit);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            InitializePlayerList();
            return;
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            PlayerLeft(otherPlayer);
        }

        [PunRPC]
        private void RPC_StartMatch()
        {
            HFLogger.Log(this, "Match started.");
            PhotonNetwork.LoadLevel(2);
        }

        public void StartMatch()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (playersInRoom.Count < 2) return;

                Player[] players = PhotonNetwork.PlayerList;
                foreach (Player player in players)
                {
                    if (!player.CustomProperties.ContainsKey("IsReady") || !(bool)player.CustomProperties["IsReady"])
                    {
                        HFLogger.Log(this, "Not all players are ready.");
                        Invoke(nameof(this.StartMatch), 1f);
                        return;
                    }
                }

                photonView.RPC("RPC_StartMatch", RpcTarget.All);
                HFLogger.Log(this, "All players are ready. Starting match!");
            }
        }

    }
}
