using System;
using System.Collections.Generic;
using flameborn.Core.Managers.Abstract;
using HF.Logger;
using PlayFab.MultiplayerModels;
using UnityEngine.SceneManagement;

namespace flameborn.Core.Managers
{
    /// <summary>
    /// Manages the game lifecycle and other managers in the system.
    /// </summary>
    public class GameManager : SingletonManager<GameManager>, IGameManager
    {
        #region Fields

        private Dictionary<Type, object> managerRepository = new Dictionary<Type, object>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
        /// </summary>
        public GameManager()
        {
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Gets a manager of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of manager to retrieve.</typeparam>
        /// <returns>A tuple containing a boolean indicating if the manager is present and the manager instance.</returns>
        public (bool IsContain, T? Instance) GetManager<T>() where T : IManager
        {
            var IsContain = false;
            T? Instance = default;

            if (managerRepository.ContainsKey(typeof(T)))
            {
                IsContain = true;
                Instance = (T)managerRepository[typeof(T)];
            }
            else
            {
                HFLogger.LogError(this, $"{nameof(managerRepository)} doesn't have any {typeof(T).Name} concrete");
            }

            return (IsContain, Instance);
        }

        /// <summary>
        /// Sets a manager of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of manager to set.</typeparam>
        /// <param name="concrete">The manager instance to set.</param>
        public void SetManager<T>(T concrete) where T : IManager
        {
            if (managerRepository.ContainsKey(typeof(T))) return;

            managerRepository.Add(typeof(T), concrete);
            HFLogger.Log(this, $"Added new manager \"{concrete.GetType().Name}\" to {nameof(managerRepository)}");
        }

        /// <summary>
        /// Loads the main menu scene.
        /// </summary>
        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(1);
        }

        #endregion

        #region PrivateMethods

        /// <summary>
        /// Handles the event when a matchmaking ticket result is received and a match is found.
        /// </summary>
        /// <param name="result">The matchmaking ticket result.</param>
        private void EventListener_MatchMaking_OnMatchFound(GetMatchmakingTicketResult result)
        {
            if (string.IsNullOrEmpty(result.MatchId))
            {
                var uiManager = GetManager<UiManager>();
                if (uiManager.IsContain)
                {
                    uiManager.Instance.mainMenu.waitingPanel.SetActive(false);
                }
            }
        }

        #endregion
    }
}
