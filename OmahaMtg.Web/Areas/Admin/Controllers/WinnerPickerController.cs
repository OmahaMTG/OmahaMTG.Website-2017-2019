using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OmahaMtg.Web.Areas.Admin.Controllers
{
    public class WinnerPickerController : Controller
    {
        // GET: Admin/WinnerPicker
        public ActionResult Index(int eventId)
        {
            return View(eventId);
        }

        [AllowAnonymous]
        public JsonResult GetWinner(int eventId, int numberOfWinnersToGet)
        {
            WinnerPicker.WinnerPicker winnerPicker = new WinnerPicker.WinnerPicker();
            return Json(winnerPicker.GetWinners(eventId, numberOfWinnersToGet), JsonRequestBehavior.AllowGet);
        }
    }
}