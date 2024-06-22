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


        /// <summary>
        /// Initializes the SpawnController and spawns players at the designated spawn points.
        /// </summary>
        private void Start()
        {
            var photonManager = GameManager.Instance.GetManager<PhotonManager>().Instance;
            var players = photonManager.playersInRoom;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] == PhotonNetwork.LocalPlayer)
                {
                    SpawnPlayer(i);
                }
            }
        }

        /// <summary>
        /// Spawns the player at the specified index and sets the camera position.
        /// </summary>
        /// <param name="index">The index of the player in the list.</param>
        private void SpawnPlayer(int index)
        {
            string playerName = index == 0 ? "PlayerBlue" : "PlayerGreen";
            Vector3 spawnPosition = spawnPoints[index].transform.position;
            CameraController.GlobalAccess.transform.position = spawnPosition;
            PhotonNetwork.Instantiate(playerName, spawnPosition, Quaternion.identity).GetComponent<PlayerCampFire>();
        }
    }
}