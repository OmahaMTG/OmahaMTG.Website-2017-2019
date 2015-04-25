using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Data
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<Event> Events { get; set; }
        public virtual IList<User> Users { get; set; } 
    }
}
