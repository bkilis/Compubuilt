using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class PromotionalCode
    {
        public PromotionalCode()
        {
            Orders = new HashSet<Order>();
        }

        public int PromotionalCodeId { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public byte UseLimitPerUser { get; set; }
        public byte DiscountValue { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
