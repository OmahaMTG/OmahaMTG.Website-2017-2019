using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OmahaMtg.Web.Models
{
    public class RandomBannerAddModel
    {
        #region Fields
        private Data.BannerAdd bannerAdd;

        #endregion
        #region Constructor
        public RandomBannerAddModel()
        {
            OmahaMtg.BannerAdds.IBannerAddManager bannerManager =
                    new OmahaMtg.BannerAdds.BannerAddManager();
            bannerAdd = bannerManager.GetRandomActiveBannerAdd();
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the banner add URL for a random active banner add.
        /// </summary>
        /// <value>
        /// The banner add URL.
        /// </value>
        public string BannerAddUrl
        {
            get
            {
                string fileName = bannerAdd.FileName;
                string filePath = "Content/Banners/" + fileName;
                return filePath; 
            }
        }
        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url
        {
            get { return bannerAdd.AddUrl; }
        }
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get { return bannerAdd.Name; }
        }
        /// <summary>
        /// Gets a value indicating whether this instance has a banner add.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has banner add; otherwise, <c>false</c>.
        /// </value>
        public bool HasBannerAdd { get { return bannerAdd != null; } }
        #endregion
    }
}