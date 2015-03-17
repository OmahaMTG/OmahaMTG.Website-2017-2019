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
                    
                    TargetPost = new TargetPost()
                    {
                        CreatedTime = sourceEvent.PostedOn,
                        ModifiedTime = DateTime.Now,
                        Body = FixupHtmlToMarkdown(sourceEvent.Description),
                        CreatedByUserId = sourceEvent.PostedById.ToString(),
                        Title = sourceEvent.Subject,
                        PublishStartTime = sourceEvent.PostedOn, 
                        IsDeleted = false,
                        PublishEndTime = null
                    }
                };
                Console.WriteLine("Imprting Event: {0}", targetEvent.TargetPost.Title);
                TargetEvent.AddEvent(targetEvent);
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
                    GroupNames = sourceUser.GroupNames
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
