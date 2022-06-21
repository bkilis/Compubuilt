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
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual DeliveryStatusType DeliveryStatusType { get; set; } = null!;
        public virtual DeliveryType DeliveryType { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
