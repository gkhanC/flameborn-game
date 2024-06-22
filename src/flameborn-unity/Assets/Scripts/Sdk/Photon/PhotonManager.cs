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
    /// <summary>
    /// Manager for handling Photon networking operations.
    /// </summary>
    public class PhotonManager : MonoBehaviourPunCallbacks, IManager
    {
        #region Fields

        private static PhotonManager instance;

        private bool isReady = false;
        private bool isConnecting = false;
        private bool isMatchStop;
        private bool isMatchStart;
        private bool isConnectSuccess;

        private string matchId = "";
        private string userName = "";

        private PhotonRoomController roomController;
        private UiManager uiManager;

        public List<Player> playersInRoom = new List<Player>();

        #endregion

        #region Methods

        /// <summary>
        /// Called when the script instance is being loaded.
        /// </summary>
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
            PhotonNetwork.KeepAliveInBackground = 600.0f;
            PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 60000;
            PhotonNetwork.NetworkingClient.LoadBalancingPeer.SentCountAllowance = 15;
            PhotonNetwork.NetworkingClient.LoadBalancingPeer.TimePingInterval = 3000;
        }

        /// <summary>
        /// Called on the first frame the script is active.
        /// </summary>
        private void Start()
        {
            GameManager.Instance.SetManager(this);
            uiManager = GameManager.Instance.GetManager<UiManager>().Instance;
            roomController = new PhotonRoomController();
        }

        /// <summary>
        /// Initializes the PhotonManager with match ID and user name.
        /// </summary>
        /// <param name="matchID">The match ID.</param>
        /// <param name="userName">The user name.</param>
        public void Init(string matchID, string userName)
        {
            this.matchId = matchID;
            this.userName = userName;

            if (PhotonNetwork.IsConnectedAndReady && PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
            {
                JoinOrCreateRoom();
                return;
            }

            ConnectToPhoton();
        }

        /// <summary>
        /// Initializes the player list.
        /// </summary>
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

        /// <summary>
        /// Shows the lobby if enough players are present.
        /// </summary>
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
                    Invoke(nameof(StartMatch), 3f);
                }
            }
            else
            {
                Invoke(nameof(this.ShowLobby), 2f);
            }
        }

        /// <summary>
        /// Checks if the connection is successful.
        /// </summary>
        public void ConnectCheck()
        {
            if (!isConnectSuccess) MatchCanceled("Network ERROR.");
        }

        /// <summary>
        /// Handles the event when a player leaves the room.
        /// </summary>
        /// <param name="otherPlayer">The player who left the room.</param>
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

        /// <summary>
        /// Joins or creates a room.
        /// </summary>
        private void JoinOrCreateRoom()
        {
            isMatchStop = false;
            isMatchStart = true;
            isConnectSuccess = true;
            roomController.CreateOrJoinRoom(matchId, 2, userName);
        }

        /// <summary>
        /// Connects to Photon.
        /// </summary>
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

            Invoke(nameof(ConnectCheck), 2f);
        }

        /// <summary>
        /// Cancels the match.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void MatchCanceled(string message)
        {
            PhotonNetwork.LeaveRoom();
            uiManager.mainMenu.waitingPanel.SetActive(false);
            uiManager.lobbyMenu.Hide();

            if (isMatchStop) return;
            isMatchStop = true;
            uiManager.alert.Show("Alert", message, LoadMainMenu);
        }

        /// <summary>
        /// Loads the main menu scene.
        /// </summary>
        public void LoadMainMenu()
        {
            SceneManager.LoadScene(1);
        }

        public override void OnLeftRoom()
        {
            PhotonNetwork.LeaveLobby();
        }

        public override void OnLeftLobby() { }

        public override void OnCreatedRoom() { }

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
                uiManager.alert.Show("Alert", $"You are disconnected. {cause}", Application.Quit);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            InitializePlayerList();
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

        /// <summary>
        /// Starts the match if all players are ready.
        /// </summary>
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
                        Invoke(nameof(StartMatch), 1f);
                        return;
                    }
                }

                photonView.RPC("RPC_StartMatch", RpcTarget.All);
                HFLogger.Log(this, "All players are ready. Starting match!");
            }
        }

        #endregion
    }
}
