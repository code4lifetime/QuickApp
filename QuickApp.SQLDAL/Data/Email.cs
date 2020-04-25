using System;
using System.Collections.Generic;

namespace QuickApp.SQLDAL.Models
{
    public partial class Email
    {
        public long Id { get; set; }
        public int EmailTypeId { get; set; }
        public string FromEmailId { get; set; }
        public string FromName { get; set; }
        public string ToEmailId { get; set; }
        public string CcEmailId { get; set; }
        public string BccEmailId { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        public bool IsHtml { get; set; }
        public bool IsAttachment { get; set; }
        public bool IsActive { get; set; }
        public int StatusId { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}