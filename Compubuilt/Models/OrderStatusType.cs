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

        public virtual ICollection<Order> Orders { get; set; }
    }
}
