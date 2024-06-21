using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

namespace flameborn.Core.Game.Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerController : MonoBehaviourPun
    {
        public NavMeshAgent agent;
        public Camera mainCamera; // Kamerayı atayın (genellikle Main Camera)
        public GameObject markerPrefab; // Tıklama noktasını işaretlemek için bir prefab (isteğe bağlı)
        private PhotonView photonView;
        private Plane plane;

        void Start()
        {
            photonView = GetComponent<PhotonView>();
            // Plane'i tanımla. Burada plane dünya koordinatlarında y=0 düzleminde.
            plane = new Plane(Vector3.up, Vector3.zero);

            // mainCamera'nın atandığından emin olun
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        void Update()
        {
            if (!photonView.IsMine) return;
            // Sol fare tıklamasıyla pozisyonu al
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 clickPosition = GetClickPosition();
                Debug.Log("Clicked Position: " + clickPosition);
                agent.SetDestination(clickPosition);

                // İşaretleyici yerleştirmek için (isteğe bağlı)
                if (markerPrefab != null)
                {
                    Instantiate(markerPrefab, clickPosition, Quaternion.identity);
                }
            }
        }

        Vector3 GetClickPosition()
        {
            // Fare pozisyonundan bir ray oluştur
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            float enter = 0.0f;

            // Ray'in plane ile kesişimini kontrol et
            if (plane.Raycast(ray, out enter))
            {
                // Kesişim noktasını hesapla
                Vector3 hitPoint = ray.GetPoint(enter);
                return hitPoint;
            }

            return Vector3.zero; // Eğer kesişim yoksa
        }
    }
}