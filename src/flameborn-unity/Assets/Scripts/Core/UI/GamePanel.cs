using System;
using System.Collections.Generic;
using flameborn.Core.Game.Cameras;
using flameborn.Core.Game.Events;
using flameborn.Core.Game.Objects.Abstract;
using flameborn.Core.UI.Abstract;
using flameborn.Core.UI.Animations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace flameborn.Core.UI
{
    [Serializable]
    public class GamePanel : PanelBase, IPanel
    {
        private GameObject campFire;

        public Button campFireButton;
        public List<Button> buttons = new List<Button>();
        public List<Image> buttonsImage = new List<Image>();
        public List<UIExpandAnimation> buttonsAnimations = new List<UIExpandAnimation>();

        public void SetCampFire(GameObject camp)
        {
            campFire = camp;
            campFireButton.onClick.AddListener(new UnityAction(() =>
            {
                CameraController.GlobalAccess.SetDestination(campFire.transform.position);
                var selectable = campFire.GetComponent<ISelectable>();
                EventManager.GlobalAccess.current = selectable;
                selectable.Select();
                Debug.Log("Camp fire Click");
            }));
            campFireButton.interactable = true;
            Debug.Log("Campfire setted");
        }

        public void ShowButtons(params GameButton[] buttonsData)
        {
            for (var i = 0; i < buttonsData.Length; i++)
            {
                var button = buttons[i];

                buttonsImage[i].sprite = buttonsData[i].btnImage;

                var text = button.GetComponentInChildren<TextMeshProUGUI>();
                text.text = buttonsData[i].btnName;
                button.onClick = new Button.ButtonClickedEvent();
                button.onClick.AddListener(new UnityAction(buttonsData[i].ClickEvent));
                button.gameObject.SetActive(true);
                buttonsAnimations[i].Play();
            }
        }

        public void CloseButtons()
        {
            for (var i = 0; i < buttons.Count; i++)
            {
                buttonsAnimations[i].Stop(() => buttons[i].gameObject.SetActive(false));
            }
        }

        public override void Show()
        {
            buttonsAnimations.ForEach(a =>
            {
                a.Init();
            });
            base.Show();
        }

        public override void Hide()
        {
            CloseButtons();
            base.Hide();
        }

    }
}