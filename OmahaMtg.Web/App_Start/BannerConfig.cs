using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OmahaMtg.Web.App_Start
{
    public static class BannerConfig
    {
        /// <summary>
        /// Ensures the banner images.
        /// </summary>
        public static void EnsureBannerImages()
        {
            try {
                string appDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string contentBannersFoler = appDirectory + "Content\\Banners\\";
                string appDataBannersFolder = appDirectory + "App_Data\\Banners\\";

                if (!Directory.Exists(contentBannersFoler))
                    Directory.CreateDirectory(contentBannersFoler);

                if (!Directory.Exists(appDataBannersFolder))
                    Directory.CreateDirectory(appDataBannersFolder);


                foreach (var file in Directory.GetFiles(appDataBannersFolder))
                {
                    string newFileName = Path.Combine(contentBannersFoler, Path.GetFileName(file));
                    if (!File.Exists(newFileName))
                    {
                        File.Copy(file, newFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                OmahaMtg.Log.Logging.Information("Error configuring banner adds: ", ex.Message + " " + ex.StackTrace);
            }
        }
    }
}