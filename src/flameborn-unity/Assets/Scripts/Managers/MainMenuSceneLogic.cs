using HF.Extensions;
using HF.Logger;
using UnityEngine;
namespace Flameborn.Managers
{
    public class MainMenuSceneLogic : SceneLogic
    {
        protected override void Start()
        {
            if (UIManager.Instance.IsNull() || UIManager.Instance.gameObject.IsNull())
            {
                HFLogger.LogError(UIManager.Instance, "UI Manager instance is null.");
            }
            
            base.Start();
        }
        public override void Initialize()
        {
            UIManager.Instance.MainMenuUIController.Init();
        }
    }
}