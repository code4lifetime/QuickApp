using System;
using System.Collections.Generic;

namespace QuickApp.SQLDAL.Models
{
    public partial class EmailAttachment
    {
        public long Id { get; set; }
        public long EmailId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentPath { get; set; }
        public bool? IsActive { get; set; }
        public long CreateById { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedById { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}