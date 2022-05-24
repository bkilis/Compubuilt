using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class Delivery
    {
        public Delivery()
        {
            Orders = new HashSet<Order>();
        }

        public int DeliveryId { get; set; }
        public int DeliveryStatusTypeId { get; set; }
        public int DeliveryTypeId { get; set; }

        public virtual DeliveryStatusType DeliveryStatusType { get; set; } = null!;
        public virtual DeliveryType DeliveryType { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
