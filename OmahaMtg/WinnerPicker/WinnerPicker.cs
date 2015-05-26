using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Presentation;
using OmahaMtg.Data;
using OmahaMtg.Events.Report;

namespace OmahaMtg.WinnerPicker
{
    public class WinnerPicker
    {
        private ApplicationDbContext _context;

        public WinnerPicker()
        {
            _context = new ApplicationDbContext();

            //_context.Rsvps.Where(w => w.EventId == eventId).Count()

        }

        public KeyValuePair<Guid, string> GetWinner(int eventId, List<Guid> exclusionList)
        {
            var rsvpUsers = _context.Rsvps.Where(w => w.EventId == eventId).Where(w => !exclusionList.Contains(w.UserId)).OrderBy(o => o.RsvpTime)
                .Include(r => r.User);//.Select(s => s.User.FirstName + " " + s.User.LastName);

            int totalRemainingUsers = rsvpUsers.Count();

            var results = new KeyValuePair<Guid, string>();

            if (totalRemainingUsers <= 0)
            {
                return new KeyValuePair<Guid, string>(Guid.Empty, "No More Users"); 
            }
            var index = new Random().Next(rsvpUsers.Count());
            var user = rsvpUsers.Skip(index).FirstOrDefault().User;
            results = new KeyValuePair<Guid, string>(user.Id, user.FirstName + " " + user.LastName); 


            return results;
        } 
    }
}