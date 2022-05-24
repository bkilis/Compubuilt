using System;
using System.Collections.Generic;

namespace Compubuilt.Models
{
    public partial class PromotionalCode
    {
        public int PromotionalCodeId { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public byte UseLimitPerUser { get; set; }
        public byte DiscountValue { get; set; }
        public int PromotionalCodeTypeId { get; set; }

        public virtual PromotionalCodeType PromotionalCodeType { get; set; } = null!;
    }
}
