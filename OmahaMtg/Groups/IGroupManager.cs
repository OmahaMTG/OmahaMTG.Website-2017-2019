using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Groups
{
    public interface IGroupManager
    {
        List<string> GetUserEmailsInGroup(int groupId);
    }
}
