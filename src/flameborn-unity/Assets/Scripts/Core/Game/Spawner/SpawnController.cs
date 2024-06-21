using System.Collections.Generic;
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
            players.ForEach(p =>
            {
                if (p == PhotonNetwork.LocalPlayer)
                {
                    var randX = Random.Range(-5, 5);
                   PhotonNetwork.Instantiate("Player", new Vector3(randX, 0f, 0f), Quaternion.identity);
                }
            });
        }
    }
}