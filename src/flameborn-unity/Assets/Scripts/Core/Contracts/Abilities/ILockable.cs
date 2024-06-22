namespace flameborn.Core.Contracts.Abilities
{
    /// <summary>
    /// Defines an interface for objects that can be locked and unlocked.
    /// </summary>
    public interface ILockAble
    {
        /// <summary>
        /// Locks the object with a specified locker.
        /// </summary>
        /// <param name="lockerObject">The object used to lock.</param>
        void Lock(object lockerObject);

        /// <summary>
        /// Unlocks the object with a specified locker.
        /// </summary>
        /// <param name="lockerObject">The object used to unlock.</param>
        void UnLock(object lockerObject);
    }
}
