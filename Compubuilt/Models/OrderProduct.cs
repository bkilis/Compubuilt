using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class OrderProduct
    {
        public int OrderProductId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountValue { get; set; }
        public int Quantity { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
