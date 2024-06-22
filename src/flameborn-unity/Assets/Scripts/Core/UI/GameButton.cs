using System;
using HF.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace flameborn.Core.UI
{
    [Serializable]
    public class GameButton
    {
        public string btnName;
        public Sprite btnImage;
        public Action ClickEvent;

        public GameButton(string btnName, Sprite buttonImage, params Action[] actions)
        {
            this.btnName = btnName;
            this.btnImage = buttonImage;
            actions.ForEach(a =>
            {
                ClickEvent += a;
            });
        }
    }
}