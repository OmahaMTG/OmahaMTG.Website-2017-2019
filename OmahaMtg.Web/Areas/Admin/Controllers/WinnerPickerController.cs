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

        private List<Guid> Winners()
        {
            object o = Session["winners"];
            if (o is List<Guid>)
            {
                return (List<Guid>)o;
            }

            return new List<Guid>();
        }

        private void Set (Guid winner)
        {
            var winners = Winners();
            winners.Add(winner);
            Session["winners"] = winners;
        }



        [AllowAnonymous]
        public JsonResult GetWinner(int eventId)
        {
            WinnerPicker.WinnerPicker winnerPicker = new WinnerPicker.WinnerPicker();

            var winner = winnerPicker.GetWinner(eventId, Winners());

            Set(winner.Key);

            return Json(winner.Value, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetRsvps(int eventId)
        {
            WinnerPicker.WinnerPicker winnerPicker = new WinnerPicker.WinnerPicker();

            var winner = winnerPicker.GetAllRsvpUsers(eventId);

            return new JsonpResult(winner);
        }
    }
}