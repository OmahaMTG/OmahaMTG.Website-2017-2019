using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OmahaMtg.BannerAdds;
using OmahaMtg.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OmahaMtg.Web.Areas.Admin.Models.BannerAdd
{
    public class BannerAddsModel
    {
        #region Fields
        OmahaMtg.BannerAdds.IBannerAddManager addManager;
        #endregion
        #region Constructors
        public BannerAddsModel()
        {
            addManager = new OmahaMtg.BannerAdds.BannerAddManager();
        }
        #endregion
        #region Public Methods
        public OmahaMtg.BannerAdds.BannerAdds GetBannerAdds()
        {
            return addManager.GetBannerAdds();
        }

        public void Create(Data.BannerAdd model)
        {
            addManager.CreateBannerAdd(ModelToBannerAdd(model));
        }

        private Data.BannerAdd ModelToBannerAdd(Data.BannerAdd model)
        {
            //Ensure item is base so EF doesn't freak out...
            Data.BannerAdd bannerAdd = AddToAdd(model);
            return bannerAdd;
        }

        public Data.BannerAdd Get(int id)
        {
            return addManager.GetBannerAdd(id);
        }

        public void Edit(Data.BannerAdd model)
        {
            //Ensure item is base so EF doesn't freak out...
            Data.BannerAdd bannerAdd = AddToAdd(model);
            addManager.UpdateBannerAdd(ModelToBannerAdd(bannerAdd));
        }


        public void Delete(int id)
        {
            addManager.DeleteBannerAdd(id);
        }
        #endregion
        #region Private Methods
        private Data.BannerAdd AddToAdd(Data.BannerAdd model)
        {
            Data.BannerAdd bannerAdd = new Data.BannerAdd();
            bannerAdd.FileName = model.FileName;
            bannerAdd.Id = model.Id;
            bannerAdd.Name = model.Name;
            bannerAdd.RotationEnd = model.RotationEnd;
            bannerAdd.RotationStart = model.RotationStart;
            return bannerAdd;
        }
        #endregion
    }

    public class BannerAddDeleteModel
    {
        public int ItemId { get; set; }
    }

    public class BannerAddUploadModel : OmahaMtg.Data.BannerAdd
    {
        [DisplayName("File to Upload")]
        public HttpPostedFileBase UploadFile { get; set; }
    }
}