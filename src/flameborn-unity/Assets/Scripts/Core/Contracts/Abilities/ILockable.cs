namespace flameborn.Core.Contracts.Abilities
{
    public interface ILockAble
    {
        void Lock(object lockerObject);
        void UnLock(object lockerObject);
    }
}