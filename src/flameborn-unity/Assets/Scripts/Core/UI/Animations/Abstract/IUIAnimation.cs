using System;

namespace flameborn.Core.UI.Animations
{
    public interface IUIAnimation
    {
        void Play(params Action[] playListeners);
        void Stop(params Action[] playListeners);
    }
}