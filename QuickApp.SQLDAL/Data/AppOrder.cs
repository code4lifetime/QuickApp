using System;
using System.Collections.Generic;

namespace QuickApp.SQLDAL.Models
{
    public partial class AppOrder
    {
        public AppOrder()
        {
            AppOrderDetails = new HashSet<AppOrderDetail>();
        }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public string Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string CashierId { get; set; }
        public int CustomerId { get; set; }

        public virtual AspNetUser Cashier { get; set; }
        public virtual AppCustomer Customer { get; set; }
        public virtual ICollection<AppOrderDetail> AppOrderDetails { get; set; }
    }
}