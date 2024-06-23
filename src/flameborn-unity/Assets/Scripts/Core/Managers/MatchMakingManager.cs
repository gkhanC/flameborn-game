using System;
using System.Collections;
using flameborn.Core.Managers.Abstract;
using flameborn.Sdk.Controllers.MatchMaking;
using flameborn.Sdk.Photon;
using flameborn.Sdk.Requests.Abstract;
using flameborn.Sdk.Requests.MatchMaking;
using flameborn.Sdk.Requests.MatchMaking.Abstract;
using flameborn.Sdk.Requests.MatchMaking.Entity;
using HF.Extensions;
using HF.Library.Utilities.Singleton;
using HF.Logger;
using UnityEngine;

namespace flameborn.Core.Managers
{
    /// <summary>
    /// Manages matchmaking operations.
    /// </summary>
    [Serializable]
    public class MatchMakingManager : MonoBehaviourSingleton<MatchMakingManager>, IManager
    {
        #region Fields

        [field: SerializeField] private string queueName = "flameborn_match";
        [field: SerializeField] private int coroutineIterationCount = 12;
        private int iteration = 0;
        private ICreateMatchTicketResponse ticketResponse = null;
        private IFindMatchResponse findResponse = null;
        private UiManager uiManager;
        private AccountManager accountManager;
        private IApiRequest<IFindMatchResponse> findMatchRequest;

        #endregion

        #region UnityFunctions

        /// <summary>
        /// Initializes the MatchMakingManager and sets up dependencies.
        /// </summary>
        private void Start()
        {
            GameManager.Instance.SetManager(this);
            uiManager = GameManager.Instance.GetManager<UiManager>().Instance;
            accountManager = GameManager.Instance.GetManager<AccountManager>().Instance;
        }

        #endregion

        #region PublicMethods

        /// <summary>
        /// Initiates a new match if no ticket response is present.
        /// </summary>
        public void NewMatch()
        {
            if (ticketResponse.IsNotNull()) return;
            CreateTicket();
        }

        #endregion

        #region PrivateMethods

        /// <summary>
        /// Creates a new matchmaking ticket.
        /// </summary>
        private void CreateTicket()
        {
            uiManager.mainMenu.Lock(this);
            uiManager.mainMenu.waitingPanel.SetActive(true);

            var createTicketRequest = new CreateMatchTicketRequest(new CreateMatchTicketController_Playfab(queueName, accountManager.Account.UserData));
            createTicketRequest.SendRequest(out string errorLog, OnGetMatchTicketResponse_EventListener);

            if (errorLog.Length > 0)
            {
                ticketResponse = new CreateMatchTicketResponse();
                ticketResponse.SetResponse(false, "", errorLog);
                OnGetMatchTicketResponse_EventListener(ticketResponse);
            }
        }

        /// <summary>
        /// Starts the process of finding a match.
        /// </summary>
        private void FindMatch()
        {
            findMatchRequest = new FindMatchRequest(new FindMatchController_Playfab(ticketResponse.TicketId, queueName));
            StartCoroutine(nameof(this.MatchSearchingCoroutine));
        }

        /// <summary>
        /// Retrieves match information.
        /// </summary>
        private void GetMatchInfo()
        {
            var getMatchInfo = new GetMatchInfoRequest(new GetMatchInfoController_Playfab(findResponse.MatchId, queueName));
            getMatchInfo.SendRequest(out string errorLog, OnGetMatchInfoResponse_EventListener);

            if (errorLog.Length > 0)
            {
                IGetMatchInfoResponse response = new GetMatchInfoResponse();
                response.SetResponse(false, "", errorLog);
                OnGetMatchInfoResponse_EventListener(response);
            }
        }

        /// <summary>
        /// Coroutine that repeatedly attempts to find a match.
        /// </summary>
        private IEnumerator MatchSearchingCoroutine()
        {
            while (iteration < coroutineIterationCount)
            {
                findMatchRequest.SendRequest(out string errorLog, OnGetFindMatchResponse_EventListener);
                if (errorLog.Length > 0)
                {
                    var response = new FindMatchResponse();
                    response.IsRequestSuccess = false;
                    OnGetFindMatchResponse_EventListener(response);
                    iteration = 0;
                    ticketResponse = null;
                    findResponse = null;
                    HFLogger.LogError(this, "Find match has some errors.", errorLog);
                    uiManager.alert.Show("Alert", $"Find match has some errors. {errorLog}");
                    StopCoroutine(nameof(this.MatchSearchingCoroutine));
                    yield return null;
                }

                iteration++;
                yield return new WaitForSeconds(4f);
            }

            yield return null;
        }

        /// <summary>
        /// Event listener for match info response.
        /// </summary>
        /// <param name="response">The response received for match info.</param>
        private void OnGetMatchInfoResponse_EventListener(IGetMatchInfoResponse response)
        {
            if (response.IsRequestSuccess)
            {
                uiManager.mainMenu.UnLock(this);
                uiManager.mainMenu.waitingPanel.SetActive(false);

                uiManager.lobbyMenu.Show();

                return;
            }

            uiManager.mainMenu.UnLock(this);
            uiManager.mainMenu.waitingPanel.SetActive(false);
            uiManager.alert.Show("Alert", response.Message);
        }

        /// <summary>
        /// Event listener for find match response.
        /// </summary>
        /// <param name="response">The response received for find match.</param>
        private void OnGetFindMatchResponse_EventListener(IFindMatchResponse response)
        {
            if (response.IsRequestSuccess)
            {
                HFLogger.Log(this, "Match found.");

                var photon = GameManager.Instance.GetManager<PhotonManager>();
                photon.Instance.Init(response.MatchId, accountManager.Account.UserData.UserName);
                uiManager.mainMenu.UnLock(this);
                ticketResponse = null;
                findResponse = null;
                StopCoroutine(nameof(this.MatchSearchingCoroutine));
                iteration = 0;
            }
            else
            {
                if (iteration >= coroutineIterationCount)
                {
                    StopCoroutine(nameof(this.MatchSearchingCoroutine));
                    uiManager.mainMenu.UnLock(this);
                    uiManager.mainMenu.waitingPanel.SetActive(false);
                    ticketResponse = null;
                    findResponse = null;
                    iteration = 0;
                    uiManager.alert.Show("Alert", response.Message);
                }
            }

            HFLogger.Log(response, response.Message);
        }

        /// <summary>
        /// Event listener for match ticket response.
        /// </summary>
        /// <param name="response">The response received for match ticket.</param>
        private void OnGetMatchTicketResponse_EventListener(ICreateMatchTicketResponse response)
        {
            if (!response.IsRequestSuccess)
            {
                uiManager.mainMenu.UnLock(this);
                uiManager.mainMenu.waitingPanel.SetActive(false);
                ticketResponse = null;
                uiManager.alert.Show("Alert", response.Message);
                return;
            }

            ticketResponse = response;
            HFLogger.LogSuccess(this, "Create match making ticket on playfab succeed.");
            FindMatch();
        }

        #endregion
    }
}
