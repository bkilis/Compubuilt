using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public int OrderId { get; set; }
        public string? OrderNumber { get; set; }
        public DateTime PlacementDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int? OrderStatusTypeId { get; set; }
        public int? PaymentId { get; set; }
        public int CustomerId { get; set; }
        public int? DeliveryId { get; set; }
        public int AddressId { get; set; }
        public int? PromotionalCodeId { get; set; }
        public decimal TotalValue { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual CustomerAddress Address { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual Delivery? Delivery { get; set; }
        public virtual OrderStatusType? OrderStatusType { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual PromotionalCode? PromotionalCode { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
