using System;
using System.Collections.Generic;
using flameborn.Core.Game.Events;
using flameborn.Core.Game.Objects.Abstract;
using flameborn.Core.Managers;
using flameborn.Core.UI;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace flameborn.Core.Game.Player
{
    public class PlayerCampFire : MonoBehaviourPun, IPunObservable, ISelectable
    {
        private PhotonView photonView;

        [SerializeField] private int wood = 200;
        [SerializeField] private string userName = "";
        [SerializeField] private string userColor = "";

        [SerializeField] private TextMeshProUGUI woodInfo;
        [SerializeField] private TextMeshProUGUI userNameInfo;

        [SerializeField] private List<GameObject> workers;
        private List<PlayerWorker> currentWorkers = new List<PlayerWorker>(10);

        private int nextWorkerCost = 25;

        public SelectableTypes selectableType => SelectableTypes.Player;

        public GameObject GetGameObject => gameObject;

        [SerializeField] private List<GameButton> campFireButtons;

        private UiManager uiManager;
        private AccountManager accountManager;

        /// <summary>
        /// Initializes the player campfire and sets up necessary components.
        /// </summary>
        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            userName = photonView.Controller.NickName;
            
            if (!photonView.IsMine)
            {
                return;
            }

            InitializeLocalPlayer();
            InitializeUIManager();
        }

        /// <summary>
        /// Initializes the local player with necessary data and components.
        /// </summary>
        private void InitializeLocalPlayer()
        {
            SpawnWorker();
            accountManager = GameManager.Instance.GetManager<AccountManager>().Instance;
            wood = accountManager.Account.UserData.Rating > 0 ? accountManager.Account.UserData.Rating : wood;
            UpdateUI();
            EventManager.GlobalAccess.SetLocalPlayer(this);
        }

        /// <summary>
        /// Initializes the UI manager for the player campfire.
        /// </summary>
        private void InitializeUIManager()
        {
            uiManager = GameManager.Instance.GetManager<UiManager>().Instance;
            uiManager.gamePanel.SetCampFire(gameObject);
        }

        /// <summary>
        /// Updates the player campfire state every frame.
        /// </summary>
        private void Update()
        {
            if (photonView.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    wood++;
                    UpdateUI();
                }
            }
        }

        /// <summary>
        /// Selects the player campfire and sets the UI game menu.
        /// </summary>
        public void Select()
        {
            if (!photonView.IsMine) return;

            SetUIGameMenu();
        }

        public void SetTarget(ISelectable go)
        {
            // Implement functionality here
        }

        public void SetDestination(Vector3 position)
        {
            // Implement functionality here
        }

        /// <summary>
        /// Sets the UI game menu for the player campfire.
        /// </summary>
        private void SetUIGameMenu()
        {
            if (!photonView.IsMine) return;

            campFireButtons[0].btnName = $"${nextWorkerCost} Worker";
            campFireButtons[0].ClickEvent = () =>
            {
                SpawnWorker();
                SetUIGameMenu();
            };

            uiManager.gamePanel.ShowButtons(campFireButtons.ToArray());
        }

        /// <summary>
        /// Deselects the player campfire and closes the UI game menu.
        /// </summary>
        public void DeSelect()
        {
            uiManager.gamePanel.CloseButtons();
        }

        /// <summary>
        /// Spawns a new worker if the player has enough wood.
        /// </summary>
        public void SpawnWorker()
        {
            if (nextWorkerCost <= wood)
            {
                try
                {
                    var selected = UnityEngine.Random.Range(0, workers.Count);
                    var worker = PhotonNetwork.Instantiate(workers[selected].name, transform.position, transform.rotation).GetComponent<PlayerWorker>();
                    worker.SetDestination(transform.position + Vector3.forward * -4f);

                    if (workers.Count > 0)
                    {
                        wood -= nextWorkerCost;
                        nextWorkerCost += (int)(nextWorkerCost * 0.5f);
                    }

                    currentWorkers.Add(worker);
                    UpdateUI();
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error spawning worker: {e.Message}");
                }
            }
        }

        /// <summary>
        /// Sets the user's name and updates the UI.
        /// </summary>
        /// <param name="newUserName">The new user name to set.</param>
        public void SetUserName(string newUserName)
        {
            userName = newUserName;
            UpdateUI();
        }

        /// <summary>
        /// Adds wood to the player's campfire and updates the UI.
        /// </summary>
        /// <param name="amount">The amount of wood to add.</param>
        public void AddWoods(int amount)
        {
            wood += amount;
            UpdateUI();
            accountManager.Account.UserData.Rating = wood;
            accountManager.UpdateStatistics();
        }

        /// <summary>
        /// Updates the UI elements with the current player campfire state.
        /// </summary>
        private void UpdateUI()
        {
            if (woodInfo != null)
            {
                woodInfo.text = wood.ToString();
            }
            if (userNameInfo != null)
            {
                userNameInfo.text = userName;
            }
        }

        /// <summary>
        /// Handles Photon serialization for syncing data across the network.
        /// </summary>
        /// <param name="stream">The Photon stream to read from or write to.</param>
        /// <param name="info">Additional information about the Photon message.</param>
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (photonView == null) return;

            if (stream.IsWriting && photonView.IsMine)
            {
                stream.SendNext(wood);
                stream.SendNext(userName);
            }
            else if (!photonView.IsMine && info.Sender != PhotonNetwork.LocalPlayer)
            {
                wood = (int)stream.ReceiveNext();
                userName = (string)stream.ReceiveNext();
                UpdateUI();
            }
        }

        /// <summary>
        /// Called when the script is enabled.
        /// </summary>
        private void OnEnable()
        {
            UpdateUI();
        }
    }
}
