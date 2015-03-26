using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OmahaMtg.Email
{
    public interface IEmailer
    {
        void SendEmail(EmailInfo emailInfo);
    }
}
