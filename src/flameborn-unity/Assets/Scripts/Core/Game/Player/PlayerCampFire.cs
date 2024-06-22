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

        public int wood = 200;
        public string userName = "";
        public string userColor = "";

        public TextMeshProUGUI woodInfo;
        public TextMeshProUGUI UserNameInfo;

        public List<GameObject> workers;
        public List<PlayerWorker> currentWorkers;

        public int nextWorkerCost = 25;

        public SelectableTypes selectableType => SelectableTypes.Player;

        public GameObject GetGameObject => this.gameObject;

        public GameObject selectedObject;

        public List<GameButton> campFireButtons;

        private UiManager uiManager;

        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            userName = photonView.Controller.NickName;
            UpdateUI();

            if (!photonView.IsMine)
            {
                return;
            }

            SpawnWorker();
            EventManager.GlobalAccess.SetLocalPlayer(this);
            uiManager = GameManager.Instance.GetManager<UiManager>().Instance;
            uiManager.gamePanel.SetCampFire(this.gameObject);
        }

        private void Update()
        {
            if (photonView.IsMine)
            {
                UpdateUI();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                wood++;
            }
            UpdateUI();
        }

        public void Select()
        {
            if (!photonView.IsMine) return;

            SetUIGameMenu();
        }

        public void SetTarget(ISelectable go)
        {

        }

        public void SetDestination(Vector3 position)
        {

        }

        private void SetUIGameMenu()
        {
            if (!photonView.IsMine) return;

            campFireButtons[0].btnName = $"${nextWorkerCost} Worker";

            campFireButtons[0].ClickEvent = new System.Action(this.SpawnWorker);

            campFireButtons[0].ClickEvent += this.SetUIGameMenu;
            uiManager.gamePanel.ShowButtons(campFireButtons.ToArray());
        }

        public void DeSelect()
        {
            uiManager.gamePanel.CloseButtons();
            Debug.LogError("DeSelect");
        }

        public void SpawnWorker()
        {
            if (nextWorkerCost <= wood)
            {
                var selected = Random.Range(0, 1);
                var worker = PhotonNetwork.Instantiate(workers[selected].name, transform.position, transform.rotation).GetComponent<PlayerWorker>();
                worker.SetDestination(transform.position + (Vector3.forward * -4f));

                if (workers.Count > 0)
                {
                    wood -= nextWorkerCost;
                    nextWorkerCost += (int)(nextWorkerCost * .5f);
                }

                currentWorkers.Add(worker);
                UpdateUI();
            }
        }

        public void SetUserName(string userName)
        {
            this.userName = userName;
            UpdateUI();
        }

        public void AddWoods(int wood)
        {
            this.wood += wood;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (woodInfo != null)
            {
                woodInfo.text = wood.ToString();
            }
            if (UserNameInfo != null)
            {
                UserNameInfo.text = userName;
            }
        }

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

        private void OnEnable()
        {
            UpdateUI();
        }

    }
}
