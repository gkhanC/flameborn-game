using System;
using DG.Tweening;
using HF.Extensions;
using UnityEngine;

namespace flameborn.Core.UI.Animations
{
    [Serializable]
    public class UISlideAnimation : IUIAnimation
    {
        private bool isInitialized = false;
        private bool isStopping;
        private bool isPlaying;
        private Vector2 mainPos = new Vector2();

        public bool isDisappearedOnStart = true;
        public float slideTime = 1f;
        public float targetDirection = 1f;
        public float disappearTime = 1;
        public float disappearDirection = 0f;
        public float moveDistance = 0f;
        public AnimationCurve easePlay;
        public AnimationCurve easeStop;

        public RectTransform uiObject;

        public void Init()
        {
            mainPos = new Vector2(uiObject.localPosition.x, uiObject.localPosition.x);
            if (isDisappearedOnStart)
            {
               uiObject.DOAnchorPosX(mainPos.x + (moveDistance * disappearDirection), .1f, false).SetEase(easePlay);
            }
            isInitialized = true;

        }

        public void Play(params Action[] playListeners)
        {
            if (!isInitialized || isPlaying) return;

            DOTween.Kill(uiObject, true);

            uiObject.DOAnchorPosX(mainPos.x, slideTime, false).SetEase(easePlay).OnStart(() =>
            {
                isPlaying = true;
            }).OnComplete(() =>
            {
                playListeners.ForEach(p => p.Invoke());
                isPlaying = false;
            });
        }

        public void Stop(params Action[] playListeners)
        {
            if (!isInitialized || isStopping) return;

            DOTween.Kill(uiObject, true);

            uiObject.DOAnchorPosX(mainPos.x + (moveDistance * disappearDirection), slideTime, false).SetEase(easePlay).OnStart(() =>
            {
                isStopping = true;
            }).OnComplete(() =>
            {
                playListeners.ForEach(p => p.Invoke());
                isStopping = false;
            });
        }
    }
}