using System;
using System.Collections.Generic;
using flameborn.Core.UI.Abstract;
using flameborn.Core.UI.Animations;
using Photon.Realtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace flameborn.Core.UI
{
    [Serializable]
    public class LobbyPanel : PanelBase, ILobbyPanel
    {

        [FoldoutGroup("UI Animation Settings")]
        [field: SerializeField] protected UISlideAnimation uiPlayer1Animation;

        [FoldoutGroup("UI Animation Settings")]
        [field: SerializeField] protected UISlideAnimation uiPlayer2Animation;

        [FoldoutGroup("UI Elements")]
        [field: SerializeField] protected TextMeshProUGUI playerOne;

        [FoldoutGroup("UI Elements")]
        [field: SerializeField] protected TextMeshProUGUI playerTwo;

        public void SetPlayerData(List<Player> data)
        {
            playerOne.text = data[0].NickName;
            playerTwo.text = data[1].NickName;
        }

        public void Init()
        {
            uiPlayer1Animation.Init();
            uiPlayer2Animation.Init();
        }

        public override void Show()
        {
            base.Show();
            uiPlayer1Animation.Play();
            uiPlayer2Animation.Play();
        }

        public override void Hide()
        {
            uiPlayer1Animation.Stop();
            uiPlayer2Animation.Stop(base.Hide);
        }
    }
}