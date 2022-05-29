using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; } = null!;
        public DateTime PlacementDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsDraft { get; set; }
        public int? OrderStatusTypeId { get; set; }
        public int? PaymentId { get; set; }
        public int CustomerId { get; set; }
        public int? PromotionalCodeId { get; set; }
        public int? DeliveryId { get; set; }
        public int AddressId { get; set; }

        public virtual CustomerAddress Address { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual Delivery? Delivery { get; set; }
        public virtual OrderStatusType? OrderStatusType { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual PromotionalCodeType? PromotionalCode { get; set; }
    }
}
