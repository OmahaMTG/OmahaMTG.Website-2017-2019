using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using OmahaMtg.Data;

namespace OmahaMtg.Events.Report
{


    public class EventReport : IEventReport
    {
        private ApplicationDbContext _context;

        public EventReport()
        {
            _context = new ApplicationDbContext();

            //_context.Rsvps.Where(w => w.EventId == eventId).Count()

        }
        public byte[] GetEventReport(int eventId)
        {
            var users = GetEventUsers(eventId);
            return GenerateReport(users);
        }

        private List<RsvpReportRow> GetEventUsers(int eventId)
        {
            return _context.Rsvps.Where(w => w.EventId == eventId)
                .Include(r => r.User).Select(s => new RsvpReportRow()
                {
                    EmailAddress = s.User.Email,
                    FirstName = s.User.FirstName,
                    LastName = s.User.LastName,
                    RsvpDate = s.RsvpTime
                }).ToList();
        }


        private byte[] GenerateReport(List<RsvpReportRow> users)
        {

                

                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("RSVP Users");
                worksheet.Cell("A1").Value = "First Name";
                worksheet.Cell("B1").Value = "Last Name";
                worksheet.Cell("C1").Value = "Email";
                worksheet.Cell("D1").Value = "Date RSVPed";

                int rowCount = 1;
                foreach (var row in users)
                {
                    rowCount++;

                    worksheet.Cell("A" + rowCount).Value = row.FirstName;
                    worksheet.Cell("B" + rowCount).Value = row.LastName;
                    worksheet.Cell("C" + rowCount).Value = row.EmailAddress;
                    worksheet.Cell("D" + rowCount).Value = row.RsvpDate.ToLongTimeString();
                }
                       
            byte[] results;
            using( MemoryStream memory = new MemoryStream())
            {
                workbook.SaveAs(memory);

                results = memory.ToArray();
            };

            return results;
            
        }


    }
}