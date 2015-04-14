using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using OmahaMtg.Data;
using OmahaMtg.Posts.Report;

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

        public List<string> GetWinners(int eventId, int numberOfWinnersToPick)
        {
            var rsvpUsers =  _context.Rsvps.Where(w => w.EventId == eventId).OrderBy(o => o.RsvpTime)
                .Include(r => r.User).Select(s => s.User.FirstName + " " + s.User.LastName);

            var results = new List<string>();
            for (int i = 0; i < numberOfWinnersToPick; i++)
            {
                int index = new Random().Next(rsvpUsers.Count());
                results.Add(rsvpUsers.Skip(index).FirstOrDefault()); 
            }

            return results;
        } 
    }
}