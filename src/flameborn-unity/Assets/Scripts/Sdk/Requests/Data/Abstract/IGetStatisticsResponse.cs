using System.Collections.Generic;
using flameborn.Sdk.Requests.Abstract;

namespace flameborn.Sdk.Requests.Data.Abstract
{
    /// <summary>
    /// Defines an interface for responses that contain statistics data.
    /// </summary>
    public interface IGetStatisticsResponse : IApiResponse
    {
        #region Properties

        /// <summary>
        /// Gets the dictionary containing statistics data.
        /// </summary>
        Dictionary<string, int> Statistics { get; }

        #endregion
    }
}
