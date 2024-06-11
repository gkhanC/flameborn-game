using Flameborn.PlayFab;
using HF.Extensions;
using HF.Logger;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flameborn.Managers
{
    public class InitializerSceneLogic : SceneLogic
    {
        [SerializeField]
        protected int mainMenuSceneId = 1;

        protected override void Start()
        {
            if (UIManager.Instance.IsNull() || UIManager.Instance.gameObject.IsNull())
            {
                HFLogger.LogError(UIManager.Instance, "UI Manager instance is null.");
            }

            base.Start();

            UIManager.Instance.LoadingUIController.SetActiveLoadingScreen(true);
        }

        public void OnPlayFabLoginSuccess()
        {
            SceneManager.LoadScene(mainMenuSceneId);
        }

        public override void Initialize()
        {
            PlayFabManager.Instance.SubscribeLoginSuccessEvent(this.OnPlayFabLoginSuccess);
        }
    }
}