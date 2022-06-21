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
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
