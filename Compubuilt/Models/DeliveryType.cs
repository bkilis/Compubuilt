using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class DeliveryType
    {
        public DeliveryType()
        {
            Deliveries = new HashSet<Delivery>();
        }

        public int DeliveryTypeId { get; set; }
        public string DeliveryTypeName { get; set; } = null!;
        public decimal? Price { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
