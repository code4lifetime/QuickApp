using DAL;
using DAL.Models;
using EmailClient.Models;
using Entities;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmailClient.Services
{
    public class EmailClientSender: IEmailClientSender
    {
       
        readonly SmtpConfig _config;
        public EmailClientSender()
        {
           
        }

        public EmailClientSender(IOptions<AppSettings> config)
        {
            _config = config.Value.SmtpConfig;
        }
        

        public async Task<(bool success, string errorMsg)> SendEmailAsync(EmailModal emailModal)
        {
            
            MimeMessage message = new MimeMessage();

            #region TO List
            if (emailModal.ToEmails != null && emailModal.ToEmails.Count > 0)
            {
                InternetAddressList list = new InternetAddressList();
                foreach (var toEmailId in emailModal.ToEmails.Distinct())
                {
                    if (toEmailId != "")
                    {
                        list.Add(new MailboxAddress(toEmailId));
                    }
                }

                message.To.AddRange(list);
            }
            #endregion

            #region Cc List
            if (emailModal.CcEmails != null && emailModal.CcEmails.Count > 0)
            {
                InternetAddressList list = new InternetAddressList();
                foreach (var ccEmailId in emailModal.CcEmails.Distinct())
                {
                    if (ccEmailId != "")
                    {
                        list.Add(new MailboxAddress(ccEmailId));
                    }
                }
                message.Cc.AddRange(list);
            }
            #endregion

            #region Bcc List
            if (emailModal.BccEmails != null && emailModal.BccEmails.Count > 0)
            {
                InternetAddressList list = new InternetAddressList();
                foreach (var bccEmailId in emailModal.BccEmails.Distinct())
                {
                    if (bccEmailId != "")
                    {
                        list.Add(new MailboxAddress(bccEmailId));
                    }
                }
                message.Bcc.AddRange(list);
            }
            #endregion

            message.From.Add(new MailboxAddress(emailModal.FromEmail));
            message.Subject = emailModal.Subject;
            message.Body = emailModal.IsHtmlBody ? new BodyBuilder { HtmlBody = emailModal.Body }.ToMessageBody() : new TextPart("plain") { Text = emailModal.Body };

            try
            {
                SmtpConfig config = _config;

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(config.Host,config.Port, config.UseSSL);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                     client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    await client.AuthenticateAsync(config.Username, config.Password);

                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return (true, null);
            }
            catch (Exception ex)
            {
               // _logger.LogError(LoggingEvents.SEND_EMAIL, ex, "An error occurred whilst sending email");
                return (false, ex.Message);
            }
        }

       

        public bool SendEmail(EmailModal emailModal)
        {
            MailMessage email = new MailMessage();
            bool isEmailSent = false;
            try
            {
                //email.From = new MailAddress("aniket.jadhav@kairee.in", emailModal.FromName);
                email.From = new MailAddress(emailModal.FromEmail, emailModal.FromName);
                email.Subject = emailModal.Subject;
                email.IsBodyHtml = emailModal.IsHtmlBody;
                if (emailModal.EmailAttachments != null && emailModal.EmailAttachments.Count > 0)
                {
                    foreach (var attactment in emailModal.EmailAttachments)
                    {
                        System.Net.Mail.Attachment emailAttachment = new System.Net.Mail.Attachment(attactment);
                        email.Attachments.Add(emailAttachment);
                    }
                }

                email.Body = emailModal.EmailContent;
                // To email List
                //AddToemails(emailModal);
                if (emailModal.ToEmails != null && emailModal.ToEmails.Count > 0)
                {
                    foreach (var toEmailId in emailModal.ToEmails.Distinct())
                    {
                        if (toEmailId != "")
                        {
                            email.To.Add(new MailAddress(toEmailId));
                        }
                        //email.To.Add(new MailAddress("prachi.patil@kairee.in"));
                    }
                }
                // CC email List
                if (emailModal.CcEmails != null && emailModal.CcEmails.Count > 0)
                {
                    foreach (var ccEmailId in emailModal.CcEmails.Distinct())
                    {
                        if (ccEmailId != "")
                        {
                            email.CC.Add(new MailAddress(ccEmailId));
                        }
                        // email.CC.Add(new MailAddress("aniket.jadhav@kairee.in"));
                        //email.CC.Add(new MailAddress("vishal.ugale@kairee.in"));
                    }
                }
                // BCC email List
                if (emailModal.BccEmails != null && emailModal.BccEmails.Count > 0)
                {
                    foreach (var bccEmailId in emailModal.BccEmails.Distinct())
                    {
                        if (bccEmailId != "")
                        {
                            email.Bcc.Add(new MailAddress(bccEmailId));
                        }
                    }
                }

                if (email.To.Count > 0)
                {
                    //log.InfoFormat("Email Sending Start for EmailId : {0}", emailModal.Id);
                    //log.InfoFormat("Email Sending (From) : {0}", string.Join(";", emailModal.FromEmail));
                    //log.InfoFormat("Email Sending (To) : {0}", string.Join(";", emailModal.ToEmails));
                    //log.InfoFormat("Email Sending (CC) : {0}", string.Join(";", emailModal.CcEmails));
                    //log.InfoFormat("Email Sending (BCC) : {0}", string.Join(";", emailModal.BccEmails));
                    //log.InfoFormat("Email Sending Subject : {0}", emailModal.Subject);

                   // System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                   // smtp.Host = ConfigurationManager.AppSettings["APPSETTING_WebMasterSMTPServer"]; //"smtp.gmail.com"; //Or Your SMTP Server Address
                   // smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["APPSETTING_WebMasterMailId"].ToString(), ConfigurationManager.AppSettings["APPSETTING_WebMasterMailPassword"].ToString());
                   // smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["APPSETTING_WebMasterPort"]);
                   // smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["APPSETTING_WebMasterEnableSSL"]);
                   // smtp.Send(email);

                    //log.Info("Email Sent Successfully");
                    isEmailSent = true;
                }
                else
                {
                   // log.Info("Email id not found in To list");
                }
            }
            catch (Exception ex)
            {
                isEmailSent = false;
               // log.Error(ex);
                //throw;
            }
            return isEmailSent;
        }




        #region Save Email in database
        public bool SaveEmail(EmailModal emailModal)
        {
            bool isEmailsave = false;
            try
            {
                Email emailmodel = new Email();
                emailmodel.EmailTypeId = emailModal.EmialType;
                emailmodel.FromEmailId = emailModal.FromEmail;
                emailmodel.FromName = emailModal.FromName;
                emailmodel.ToEmailId = string.Join(";", emailModal.ToEmails);
                emailmodel.CcEmailId = string.Join(";", emailModal.CcEmails);
                emailmodel.BccEmailId = string.Join(";", emailModal.BccEmails);
                emailmodel.Subject = emailModal.Subject;
                emailmodel.EmailContent = emailModal.EmailContent;
                emailmodel.IsHtml = emailModal.IsHtmlBody;
                emailmodel.IsAttachment = emailModal.IsAttachment;
                emailmodel.IsActive = true;
                emailmodel.StatusId = emailModal.StatusId;
                //emailmodel.CreatedBy = emailModal.CreatedById;
                //emailmodel.CreatedOn = new CommonFunctions().ServerDate();

               // context.Emails.Add(emailmodel);
               // context.SaveChanges();
                isEmailsave = true;
            }
            catch (Exception ex)
            {
               // log.Error(ex);

            }

            return isEmailsave;
        }

       

        bool IEmailClientSender.SendEmail(EmailModal emailModal)
        {
            throw new NotImplementedException();
        }

        bool IEmailClientSender.SaveEmail(EmailModal emailModal)
        {
            throw new NotImplementedException();
        }



        //public bool SaveEmail(EmailModal emailModal, EmailAttachementModel emailAttModel)
        //{
        //    bool isEmailsave = false;
        //    try
        //    {
        //        Email emailmodel = new Email();
        //        emailmodel.EmailTypeId = emailModal.EmialType;
        //        emailmodel.FromEmailId = emailModal.FromEmail;
        //        emailmodel.FromName = emailModal.FromName;
        //        emailmodel.ToEmailId = string.Join(";", emailModal.ToEmails);
        //        emailmodel.CcEmailId = string.Join(";", emailModal.CcEmails);
        //        emailmodel.BccEmailId = string.Join(";", emailModal.BccEmails);
        //        emailmodel.Subject = emailModal.Subject;
        //        emailmodel.EmailContent = emailModal.EmailContent;
        //        emailmodel.IsHtml = emailModal.IsHtmlBody;
        //        emailmodel.IsAttachment = emailModal.IsAttachment;
        //        emailmodel.IsActive = true;
        //        emailmodel.StatusId = emailModal.StatusId;
        //        emailmodel.CreatedById = emailModal.CreatedById;
        //        emailmodel.CreatedOn = new CommonFunctions().ServerDate();

        //        context.Emails.Add(emailmodel);
        //        context.SaveChanges();

        //        EmailAttachment emailAttachementModel = new EmailAttachment();
        //        emailAttachementModel.EmailId = emailmodel.Id;
        //        emailAttachementModel.AttachmentName = emailAttModel.AttachmentName;
        //        emailAttachementModel.AttachmentPath = emailAttModel.AttachmentPath;
        //        emailAttachementModel.IsActive = true;
        //        emailAttachementModel.CreateById = emailModal.CreatedById;
        //        emailAttachementModel.CreatedOn = new CommonFunctions().ServerDate();

        //        context.EmailAttachments.Add(emailAttachementModel);
        //        context.SaveChanges();


        //        isEmailsave = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex);

        //    }

        //    return isEmailsave;
        //}

        #endregion
    }
}
