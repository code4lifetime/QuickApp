using EmailClient.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Services
{
    public interface IEmailClientSender
    {
        Task<(bool success, string errorMsg)> SendEmailAsync(EmailModal emailModal);

        bool SendEmail(EmailModal emailModal);

        bool SaveEmail(EmailModal emailModal);
    }
}
