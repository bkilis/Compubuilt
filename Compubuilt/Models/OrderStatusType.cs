using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class OrderStatusType
    {
        public OrderStatusType()
        {
            Orders = new HashSet<Order>();
        }

        public int OrderStatusTypeId { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
