using UnityEngine;
namespace Flameborn.UI.Profile
{
    public class UIRankController : MonoBehaviour
    {
        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}