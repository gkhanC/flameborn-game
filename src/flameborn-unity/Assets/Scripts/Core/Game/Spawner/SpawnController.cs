using System.Collections.Generic;
using flameborn.Core.Game.Cameras;
using flameborn.Core.Game.Player;
using flameborn.Core.Managers;
using flameborn.Sdk.Photon;
using Photon.Pun;
using UnityEngine;

namespace flameborn.Core.Game.Spawner
{

    public class SpawnController : MonoBehaviour
    {
        public GameObject playerPrefab;
        public List<GameObject> spawnPoints;


        private void Start()
        {
            var photonManager = GameManager.Instance.GetManager<PhotonManager>().Instance;
            var players = photonManager.playersInRoom;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == PhotonNetwork.LocalPlayer)
                {
                    var name = i == 0 ? "PlayerBlue" : "PlayerGreen";
                    CameraController.GlobalAccess.transform.position = spawnPoints[i].transform.position;
                    var player = PhotonNetwork.Instantiate(name, spawnPoints[i].transform.position, Quaternion.identity).GetComponent<PlayerCampFire>();
                    player.userColor = i == 0 ? "Blue" : "Green";
                }
            }
        }
    }
}