using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using QuickApp.SQLDAL.Models.Interfaces;

namespace QuickApp.SQLDAL.Models
{
    public class AuditableEntity : IAuditableEntity
    {
        [MaxLength(256)]
        public string CreatedBy { get; set; }
        [MaxLength(256)]
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
