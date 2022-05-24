using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class DeliveryStatusType
    {
        public DeliveryStatusType()
        {
            Deliveries = new HashSet<Delivery>();
        }

        public int DeliverStatusTypeId { get; set; }
        public string DeliveryStatusName { get; set; } = null!;

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
