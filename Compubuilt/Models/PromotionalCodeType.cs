using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class PromotionalCodeType
    {
        public PromotionalCodeType()
        {
            Orders = new HashSet<Order>();
        }

        public int PromotionalCodeTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
    }
}
