using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Email
{
    public interface IEmailer
    {
        Task SendEmailAsync(EmailInfo emailInfo);
    }
}
