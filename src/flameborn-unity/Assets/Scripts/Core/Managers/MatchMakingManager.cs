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
    [Serializable]
    public class MatchMakingManager : MonoBehaviourSingleton<MatchMakingManager>, IManager
    {
        [field: SerializeField] private string queueName = "flameborn_match";
        [field: SerializeField] private int coroutineIterationCount = 10;
        private int iteration = 0;
        ICreateMatchTicketResponse ticketResponse = null;
        IFindMatchResponse findResponse = null;
        UiManager uiManager;
        AccountManager accountManager;
        IApiRequest<IFindMatchResponse> findMatchRequest;

        private void Start()
        {
            GameManager.Instance.SetManager(this);
            uiManager = GameManager.Instance.GetManager<UiManager>().Instance;
            accountManager = GameManager.Instance.GetManager<AccountManager>().Instance;
        }

        public void NewMatch()
        {
            if (ticketResponse.IsNotNull()) return;
            CreateTicket();
        }

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

        private void FindMatch()
        {
            findMatchRequest = new FindMatchRequest(new FindMatchController_Playfab(ticketResponse.TicketId, queueName));
            StartCoroutine(nameof(this.MatchSearchingCoroutine));
        }

        private void GetMachInfo()
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
                yield return new WaitForSeconds(5f);
            }

            yield return null;
        }

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

    }
}