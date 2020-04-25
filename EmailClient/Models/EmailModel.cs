using System;
using System.Collections.Generic;
using System.Text;

namespace EmailClient.Models
{
    public class EmailModal
    {
        public long Id { get; set; }
        public int EmialType { get; set; }
        public int EmailTypeId { get; set; }
        public string EmailContent { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtmlBody { get; set; }
        public bool IsAttachment { get; set; }
        public List<string> EmailAttachments { get; set; }
        public List<string> ToEmails = new List<string>();
        public List<string> CcEmails = new List<string>();
        public List<string> BccEmails = new List<string>();

        public long CreatedById { get; set; }
        public string FromEmailId { get; set; }
        public bool IsActive { get; set; }
        public int StatusId { get; set; }
        public string SiestaLogoImagePath { get; set; }

        public bool IsLogoRequired { get; set; }
    }
}
