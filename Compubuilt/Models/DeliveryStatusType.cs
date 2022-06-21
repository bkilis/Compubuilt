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
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
