using EmailClient.Helpers;
using EmailClient.Models;
using Entities.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Services
{
    public class EmailClientManager 
    {
        public async Task SendHelloWorldEmail(string email, string name)
        {
            string template = "Templates.HelloWorldTemplate";

            RazorParser renderer = new RazorParser(typeof(EmailClient).Assembly);
            var body = renderer.UsingTemplateFromEmbedded(template,
                new HelloWorldViewModel { Name = name });



            EmailModal emailModal = new EmailModal();
            emailModal.FromEmail = "code4lifetime@gmail.com";
            emailModal.ToEmails.Add("useraniket@gmail.com");
            emailModal.Subject = "Welcome to ASP.net Core";
            emailModal.IsHtmlBody = true;
            emailModal.Body = body;


            var emailSendResult = await new EmailClientSender().SendEmailAsync(emailModal);


            if (emailSendResult.success)
            {
                emailModal.StatusId = (int)EmailSendStatus.Sent;
            }
            else
            {
                emailModal.StatusId = (int)EmailSendStatus.Pending;
            }

           // new EmailSender().SaveEmail(emailModal);
        }

       
    }
}
