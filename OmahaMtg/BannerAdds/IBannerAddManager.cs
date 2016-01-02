using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.BannerAdds
{
    /// <summary>
    /// An interface for a banner adds data manager.
    /// </summary>
    public interface IBannerAddManager : IDisposable
    {
        /// <summary>
        /// Gets a list of banner adds.
        /// </summary>
        /// <returns></returns>
        BannerAdds GetBannerAdds();

        /// <summary>
        /// Gets a banner add from the data source.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Data.BannerAdd GetBannerAdd(int id);

        /// <summary>
        /// Updates a banner add.
        /// </summary>
        /// <param name="add">The banner add.</param>
        void UpdateBannerAdd(Data.BannerAdd add);

        /// <summary>
        /// Creates a banner add.
        /// </summary>
        /// <param name="add">The banner add.</param>
        /// <returns></returns>
        int CreateBannerAdd(Data.BannerAdd add);
        /// <summary>
        /// Deletes the banner add.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void DeleteBannerAdd(int id);

        /// <summary>
        /// Gets the active banner adds.
        /// </summary>
        /// <returns></returns>
        BannerAdds GetActiveBannerAdds();

        /// <summary>
        /// Gets the random active banner add.
        /// </summary>
        /// <returns></returns>
        Data.BannerAdd GetRandomActiveBannerAdd();
    }
}
