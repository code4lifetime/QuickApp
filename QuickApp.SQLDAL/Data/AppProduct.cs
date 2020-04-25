using System;
using System.Collections.Generic;

namespace QuickApp.SQLDAL.Models
{
    public partial class AppProduct
    {
        public AppProduct()
        {
            AppOrderDetails = new HashSet<AppOrderDetail>();
            InverseParent = new HashSet<AppProduct>();
        }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int UnitsInStock { get; set; }
        public bool IsActive { get; set; }
        public bool IsDiscontinued { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int? ParentId { get; set; }
        public int ProductCategoryId { get; set; }

        public virtual AppProduct Parent { get; set; }
        public virtual AppProductCategory ProductCategory { get; set; }
        public virtual ICollection<AppOrderDetail> AppOrderDetails { get; set; }
        public virtual ICollection<AppProduct> InverseParent { get; set; }
    }
}