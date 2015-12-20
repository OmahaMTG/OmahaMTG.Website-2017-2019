using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmahaMtg.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BannerAddsController : Controller
    {
        #region Fields
        Areas.Admin.Models.BannerAdd.BannerAddsModel bannerModel;
        string contentBanners;
        string appDataBanners;
        #endregion
        #region Constructors
        public BannerAddsController()
        {
            bannerModel = new Models.BannerAdd.BannerAddsModel();
        }
        #endregion
        #region Actions
        // GET: Admin/BannerAdds
        public ActionResult Index()
        {
            return View(bannerModel.GetBannerAdds());
        }

        // GET: Admin/BannerAdds/Create
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Models.BannerAdd.BannerAddUploadModel model =
                new Models.BannerAdd.BannerAddUploadModel();
            return View(model);
        }

        /// <summary>
        /// Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult Create(Models.BannerAdd.BannerAddUploadModel model)
        {
            if (model.UploadFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.UploadFile.FileName);
                model.FileName = fileName;
                SaveFileToServer(model.UploadFile, fileName);
            }
            else
            {
                model.FileName = "none";
            }
            bannerModel.Create(model as Data.BannerAdd);
            return RedirectToAction("Index");
            //return View(model);
        }

        // GET: Admin/BannerAdds/Details
        /// <summary>
        /// Gives detail for the given record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Data.BannerAdd model = bannerModel.Get(id);
            return View(model);
        }

        // GET: Admin/BannerAdds/Edit
        public ActionResult Edit(int id)
        {
            Data.BannerAdd add =
                bannerModel.Get(id);
            Models.BannerAdd.BannerAddUploadModel model =
                new Models.BannerAdd.BannerAddUploadModel()
                {
                    Name = add.Name,
                    Id = add.Id,
                    FileName = add.FileName,
                    RotationEnd = add.RotationEnd,
                    RotationStart = add.RotationStart
                };
            return View(model);
        }
        /// <summary>
        /// Edits the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult Edit(Models.BannerAdd.BannerAddUploadModel model)
        {
            if (model.UploadFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.UploadFile.FileName);
                model.FileName = fileName;
                SaveFileToServer(model.UploadFile, fileName);
            }
            bannerModel.Edit(model);
            return RedirectToAction("Index");
            //return View(model);
        }

        // GET: Admin/BannerAdds/Delete
        /// <summary>
        /// Deletes the specified record.
        /// </summary>
        /// <param name="id">The record.</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            Data.BannerAdd model =
                bannerModel.Get(id);
            return View(model);
        }

        /// <summary>
        /// Deletes the specified delete model.
        /// </summary>
        /// <param name="deleteModel">The delete model.</param>
        /// <returns></returns>
        [ValidateAntiForgeryToken()]
        [HttpPost]
        public ActionResult Delete(Models.BannerAdd.BannerAddDeleteModel deleteModel)
        {
            bannerModel.Delete(deleteModel.ItemId);
            return RedirectToAction("Index");
        }
        #endregion
        #region Private Methods
        /// <summary>
        /// Saves the file to server.
        /// </summary>
        /// <param name="uploadFile">The upload file.</param>
        /// <param name="fileName">Name of the file.</param>
        private void SaveFileToServer(HttpPostedFileBase uploadFile, string fileName)
        {
            contentBanners = Server.MapPath("~/Content/Banners/");
            appDataBanners = Server.MapPath("~/App_Data/Banners/");

            //Make sure content banners folder exists
            System.IO.Directory.CreateDirectory(contentBanners);
            uploadFile.SaveAs(Path.Combine(contentBanners, fileName));

            //Make sure application data banners folder exists
            System.IO.Directory.CreateDirectory(appDataBanners);
            uploadFile.SaveAs(Path.Combine(appDataBanners, fileName));
        }

        #endregion
    }
}