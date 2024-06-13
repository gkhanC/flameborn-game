namespace Flameborn.UI.Profile
{
    using TMPro;
    using UnityEngine;

    public class UIProfileController : MonoBehaviour
    {
        public TextMeshProUGUI userName;
        public TextMeshProUGUI ranking;
        public TextMeshProUGUI rating;

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}