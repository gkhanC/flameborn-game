using System;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;

namespace flameborn.Core.UI.Animations
{
    [Serializable]
    public class UIExpandAnimation : IUIAnimation
    {
        private bool isInitialized = false;
        private bool isStopping;
        private bool isPlaying;
        private Vector2 scale = new Vector2();

        public bool isDisappearedOnStart = true;
        public float expandTime = 1f;
        public float targetSize = 1f;
        public float disappearTime = 1;
        public float disappearSize = 0f;
        public AnimationCurve easePlay;
        public AnimationCurve easeStop;

        public RectTransform uiObject;

        public void Init()
        {
            scale = Vector2.one;
            if (isDisappearedOnStart)
            {
                uiObject.localScale = scale * disappearSize;
            }
            isInitialized = true;

        }

        public void Play(params Action[] playListeners)
        {
            if (!isInitialized || isPlaying) return;

            DOTween.Kill(uiObject, true);

            uiObject.DOScale(scale * targetSize, expandTime).SetEase(easePlay).OnStart(() =>
            {
                isPlaying = true;
            }).OnComplete(() =>
            {
                playListeners.ForEach(p => p.Invoke());
                isPlaying = false;
                isStopping = false;
            });
        }

        public void Stop(params Action[] stopListeners)
        {
            if (!isInitialized || isStopping) return;

            DOTween.Kill(uiObject, true);

            uiObject.DOScale(scale * disappearSize, disappearTime).SetEase(easeStop).OnStart(() =>
            {
                isStopping = true;
            }).OnComplete(() =>
            {
                stopListeners.ForEach(p => p.Invoke());
                isStopping = false;
                isPlaying = false;
            });
        }
    }
}