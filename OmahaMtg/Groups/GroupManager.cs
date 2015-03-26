using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmahaMtg.Data;

namespace OmahaMtg.Groups
{
    public class GroupManager : IGroupManager
    {
        private ApplicationDbContext _context;
        public GroupManager()
        {
            _context = new ApplicationDbContext();
        }
        public List<string> GetUserEmailsInGroup(int groupId)
        {
            return _context.Users.Where(u => u.Groups.Any(g => g.Id == groupId))
                .Select(s => s.Email).ToList();
        }
    }
}
