namespace Flameborn.UI
{
    using System;
    using HF.Extensions;
    using UnityEngine;

    [Serializable]
    public class LoadingUIController : IUIController
    {
        [SerializeField]
        private GameObject loadingScreen;

        public void SetActiveLoadingScreen(bool isActive)
        {
            if (loadingScreen.IsNull()) return;
            loadingScreen.SetActive(isActive);
        }

        public void CloseAll()
        {
            SetActiveLoadingScreen(false);
        }
    }
}