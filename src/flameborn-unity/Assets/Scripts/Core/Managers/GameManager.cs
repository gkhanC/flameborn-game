using System;
using System.Collections.Generic;
using flameborn.Core.Managers.Abstract;
using flameborn.Sdk.Controllers.MatchMaking;
using flameborn.Sdk.Requests.MatchMaking;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using HF.Logger;
using PlayFab.MultiplayerModels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace flameborn.Core.Managers
{
    public class GameManager : SingletonManager<GameManager>, IGameManager
    {
        private Dictionary<Type, object> managerRepository = new Dictionary<Type, object>();

        #region UnityFunctions

        public GameManager()
        {
        }

        #endregion

        #region PublicMethods

        

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

        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(1);
        }

#nullable enable
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
                HFLogger.LogError(this, $"{nameof(managerRepository)} don't have any {typeof(T).Name} concrete");
            }

            return (IsContain, Instance);
        }

        public void SetManager<T>(T concrete) where T : IManager
        {
            if (managerRepository.ContainsKey(typeof(T))) return;

            managerRepository.Add(typeof(T), concrete);
            HFLogger.Log(this, $"Added new manager \"{concrete.GetType().Name}\" to {nameof(managerRepository)}");
        }

#nullable disable


        #endregion
    }
}