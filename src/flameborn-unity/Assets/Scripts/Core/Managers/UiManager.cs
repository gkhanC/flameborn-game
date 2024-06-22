using flameborn.Core.Accounts;
using flameborn.Core.Managers.Abstract;
using flameborn.Core.UI;
using flameborn.Core.User;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace flameborn.Core.Managers
{
    public class UiManager : SingletonManager<UiManager>, IManager
    {
        [InfoBox("This structure is responsible for providing the necessary definitions for the ui controller. ", InfoMessageType = InfoMessageType.None)]
        [FoldoutGroup("Main Menu Objects", expanded: true)]
        [field: SerializeField] public MainMenuPanel mainMenu = new MainMenuPanel();

        [FoldoutGroup("Lobby Menu Objects", expanded: true)]
        [field: SerializeField] public LobbyPanel lobbyMenu = new LobbyPanel();

        [FoldoutGroup("Loader Objects", expanded: true)]
        [field: SerializeField] public LoaderPanel loader = new LoaderPanel();

        [FoldoutGroup("Alert Objects", expanded: true)]
        [field: SerializeField] public AlertPanel alert = new AlertPanel();

        [FoldoutGroup("Game Panel Objects", expanded: true)]
        [field: SerializeField] public GamePanel gamePanel = new GamePanel();

        #region Unity Function

        public UiManager()
        {

        }

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoad;
            GameManager.Instance.SetManager(this);
            var accountManager = GameManager.Instance.GetManager<AccountManager>();
            if (accountManager.IsContain)
            {
                accountManager.Instance.Event_OnAccountDataOnChanged.AddListener(new UnityAction<Account>(mainMenu.EventListener_OnDataHasChanged));
                accountManager.Instance.Event_OnUserDataOnChanged.AddListener(new UnityAction<UserData>(mainMenu.EventListener_OnDataHasChanged));
            }

            lobbyMenu.Init();
            alert.Init();
        }

        private void OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 0) return;

            CloseAll();

            if (scene.buildIndex == 1)
            {
                mainMenu.Show();
            }

            if (scene.buildIndex == 2)
            {
                gamePanel.Show();
            }

        }


        public void CloseAll()
        {
            loader.Hide();
            mainMenu.Hide();
            alert.Hide();
            gamePanel.Hide();
            lobbyMenu.Hide();

        }

        #endregion
    }
}