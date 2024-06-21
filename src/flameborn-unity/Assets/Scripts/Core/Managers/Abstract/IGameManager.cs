using System;

namespace flameborn.Core.Managers.Abstract
{
    public interface IGameManager : IManager
    {
        (bool IsContain, T Instance) GetManager<T>() where T : IManager;
        void SetManager<T>( T concrete) where T : IManager;
    }
}