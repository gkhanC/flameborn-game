using flameborn.Core.Contracts.Abilities;

namespace flameborn.Core.UI.Abstract
{
    /// <summary>
    /// Defines a base interface for all UI panels.
    /// </summary>
    public interface IPanel : IShowAble, IHideAble, ILockAble
    {
        // This interface combines the capabilities of showing, hiding, and locking UI panels.
    }
}
