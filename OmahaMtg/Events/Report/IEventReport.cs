using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Events.Report
{
    public interface IEventReport
    {
        byte[] GetEventReport(int eventId);
    }
}
