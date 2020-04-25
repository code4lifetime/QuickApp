using System;
using System.Collections.Generic;

namespace QuickApp.SQLDAL.Models
{
    public partial class AppCustomer
    {
        public AppCustomer()
        {
            AppOrders = new HashSet<AppOrder>();
        }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual ICollection<AppOrder> AppOrders { get; set; }
    }
}