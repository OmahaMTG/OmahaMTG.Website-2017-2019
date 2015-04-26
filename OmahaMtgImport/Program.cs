using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Html2Markdown;

using OmahaMtgImport.Models;

namespace OmahaMtgImport
{
    public class Program
    {
        static void Main(string[] args)
        {
            TargetGroup.CreateGroups();
            TargetRoles.CreateRoles();
            ImportUsers();
            ImportEvents();
            Console.WriteLine("Import Completed........");
            Console.ReadLine();
        }


        public static void ImportEvents()
        {
            var sourceEvents = SourceEvent.GetEvents();
            Console.WriteLine("Importing {0} Events", sourceEvents.Count);



            foreach (var sourceEvent in sourceEvents)
            {
                var targetEvent = new TargetEvent()
                {
                    EventEndTime = sourceEvent.EventEndDate,
                    EventStartTime = sourceEvent.EventStartDate,
                    Location = sourceEvent.Location,
                        CreatedTime = sourceEvent.PostedOn,
                        ModifiedTime = DateTime.Now,
                        Body = FixupHtmlToMarkdown(sourceEvent.Description),
                        CreatedByUserId = sourceEvent.PostedById.ToString(),
                        Title = sourceEvent.Subject,
                        PublishStartTime = sourceEvent.PostedOn, 
                        IsDeleted = false,
                        PublishEndTime = null

                };
                Console.WriteLine("Imprting Event: {0}", targetEvent.Title);
                int targetEventId =  TargetEvent.AddEvent(targetEvent);

                var sourceEventRsvps = SourceEventRSVP.GetRsvpForEvent(sourceEvent.Id);
                Console.WriteLine("Imprting {0} Event RSVPs: {1}", sourceEventRsvps.Count, targetEvent.Title);

                foreach (var sourceRsvp in sourceEventRsvps)
                {
                    TargetRsvp.AddRsvp(new TargetRsvp()
                    {
                        EventId = targetEventId, RsvpTime = sourceRsvp.DateAdded, UserId = sourceRsvp.UserId
                    });
                }

                
                //TargetEvent.AddEvent(targetEvent);
            }

        }

        public static void ImportUsers()
        {
            var sourceUsers = SourceUser.GetUsers();

            Console.WriteLine("Processing {0} users", sourceUsers.Count);
            foreach (var sourceUser in sourceUsers)
            {

                var targetUser = new TargetUser()
                {
                    Email = sourceUser.Email,
                    EmailConfirmed = true,
                    PasswordHash = sourceUser.Password,
                    SecurityStamp = sourceUser.PasswordSalt,
                    UserName = sourceUser.UserName,
                    Id = sourceUser.UserId.ToString(), 
                    GroupNames = sourceUser.GroupNames, 
                    FirstName = sourceUser.FirstName,
                    LastName = sourceUser.LastName
                };
                Console.WriteLine("Imprting user {0}", targetUser.UserName);
                TargetUser.AddUser(targetUser);

            }
        }

        public static string FixupHtmlToMarkdown(string htmlText)
        {
            var converter = new Converter();
            var markdownText = converter.Convert(htmlText);

            markdownText = markdownText.Replace("<ul>", "");
            markdownText = markdownText.Replace("</ul>", "");
            markdownText = markdownText.Replace("</li>", "");
            markdownText = markdownText.Replace("<li>", "*");
            markdownText = Regex.Replace(markdownText, "<.*?>", string.Empty);
            return markdownText;
        }
    }
}
